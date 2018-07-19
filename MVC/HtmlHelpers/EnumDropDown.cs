using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Shared.Extensions;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     EnumDropDown custom HTML helper.
    /// </summary>
    public static class EnumDropDownExtensions
    {
        /// <summary>
        /// Returns the enum description of a model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString DisplayEnumFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return MvcHtmlString.Create((metadata.Model as Enum).GetDescription());
        }


        /// <summary>
        /// Returns a dropdownlist from a enum type
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The property name which the value must be set</param>
        /// <param name="selectedValue">Define the enum value for a model property</param>
        public static MvcHtmlString EnumDropDownList<TEnum>(this System.Web.Mvc.HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            var items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.ToString(),
                                                    Selected = (value.Equals(selectedValue))
                                                };

            return htmlHelper.DropDownList(name, items);
        }


        #region EnumDropDownListFor

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="optionLabel">The text for a default empty item. This parameter can be null</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumItems(htmlHelper, expression), optionLabel, htmlAttributes);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="optionLabel">The text for a default empty item. This parameter can be null</param>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumItems(htmlHelper, expression), optionLabel);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumItems(htmlHelper, expression), htmlAttributes);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumItems(htmlHelper, expression));
        }
        #endregion

        //Adding flexibility:
        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString EnumerableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var converter = TypeDescriptor.GetConverter(enumType);

            var items =
                from value in values
                select new SelectListItem
                {
                    Text = converter.ConvertToString(value),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                };

            if (metadata.IsNullableValueType)
                items = SingleEmptyItem.Concat(items);

            return htmlHelper.DropDownListFor(expression,items);
        }


        #region EnumNullableDropDownListFor

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="optionLabel">The text for a default empty item. This parameter can be null</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, null), optionLabel, htmlAttributes);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, null), htmlAttributes);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="optionLabel">The text for a default empty item. This parameter can be null</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, null), optionLabel);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, null));
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="listToExclude">Define the list of enum values not to present</param>
        /// <param name="optionLabel">The text for a default empty item. This parameter can be null</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, List<Enum> listToExclude, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, listToExclude), optionLabel, htmlAttributes);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="listToExclude">Define the list of enum values not to present</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, List<Enum> listToExclude, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, listToExclude), htmlAttributes);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="listToExclude">Define the list of enum values not to present</param>
        /// <param name="optionLabel">The text for a default empty item. This parameter can be null</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, List<Enum> listToExclude)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, listToExclude), optionLabel);
        }

        /// <summary>
        /// Returns a dropdownlist for a enum type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="listToExclude">Define the list of enum values not to present</param>
        public static MvcHtmlString EnumNullableDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, List<Enum> listToExclude)
        {
            return htmlHelper.DropDownListFor(expression, SetEnumNullableItems(htmlHelper, expression, listToExclude));
        }
        #endregion



        #region [privates]
        //lista os valores do enumerador nullable para disponibilizar na dropdownlist:
        private static IEnumerable<SelectListItem> SetEnumNullableItems<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, List<Enum> listToExclude)
        {
            //obter os valores:
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var values = Enum.GetValues(GetNonNullableModelType(metadata)).Cast<TEnum>();

            var dicObjs = new Dictionary<object, string>();
            var selected = "";

            //para cada valor do enumerador que não pertença à lista de exclusão, adiciona à lista a apresentar e verifica se é o valor selecionado ou não:
            foreach (var value in values.Where(value => listToExclude == null || (listToExclude != null && !listToExclude.Exists(i => i.Equals(value as Enum)))))
            {
                dicObjs.Add(value, (value as Enum).GetDescription());

                if (metadata.Model != null && value.ToString() == metadata.Model.ToString())
                    selected = metadata.Model.ToString();
            }

            //forma a selectList com os dados obtidos em cima:
            IEnumerable<SelectListItem> itens = new SelectList(dicObjs, "Key", "Value", selected);

            if (metadata.IsNullableValueType)
                itens = SingleEmptyItem.Concat(itens);

            return itens;
        }

        //lista os valores do enumerador para disponibilizar na dropdownlist:
        private static IEnumerable<SelectListItem> SetEnumItems<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            //obter os valores:
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var source = Enum.GetValues(typeof(TEnum));
            var items = new Dictionary<object, string>();
            var selected = metadata.Model.ToString();

            //para cada valor do enumerador, adiciona à lista a apresentar e verifica se é o valor selecionado ou não:
            foreach (var value in source)
            {
                items.Add(value, (value as Enum).GetDescription());

                if (value.ToString() == metadata.Model.ToString())
                    selected = (value as Enum).GetDescription();
            }

            return new SelectList(items, "Key", "Value", selected);
        }


        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        //para obter o type real de dados do enumerador:
        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            var realModelType = modelMetadata.ModelType;

            var underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
                realModelType = underlyingType;

            return realModelType;
        }
        #endregion

    }
}