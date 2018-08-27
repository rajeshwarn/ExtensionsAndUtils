using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Shared.HtmlHelper.Resources;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     CheckBoxTable custom HTML helper.
    /// </summary>
    public static class TableExtensions
    {
        #region html called methods

        /// <summary>
        /// Returns a table with checkbox options for a model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="selectList">A collection of SelectListItem objects that are used to populate the drop-down list</param>
        /// <param name="attributes">Personalized attributes to set on table</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString CheckBoxTableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, AttributesTable attributes, object htmlAttributes)
        {
            var tableBody = new TagBuilder("tbody");
            attributes = CreateTableContent(ref tableBody, selectList, attributes);

            return MvcHtmlString.Create(CreateBaseTable(tableBody, attributes, htmlAttributes).ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Returns a table with checkbox options for a model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="selectList">A collection of SelectListItem objects that are used to populate the drop-down list</param>
        /// <param name="attributes">Personalized attributes to set on table</param>
        public static MvcHtmlString CheckBoxTableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, AttributesTable attributes)
        {
            var tableBody = new TagBuilder("tbody");
            attributes = CreateTableContent(ref tableBody, selectList, attributes);

            return MvcHtmlString.Create(CreateBaseTable(tableBody, attributes).ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Returns a table with checkbox options for a model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="selectList">A collection of SelectListItem objects that are used to populate the drop-down list</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString CheckBoxTableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            var tableBody = new TagBuilder("tbody");
            var attributes = CreateTableContent(ref tableBody, selectList, new AttributesTable { HasFirstChecksAll = true, HeaderTitle = "Descrição" });

            return MvcHtmlString.Create(CreateBaseTable(tableBody, attributes, htmlAttributes).ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Returns a table with checkbox options for a model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="selectList">A collection of SelectListItem objects that are used to populate the drop-down list</param>
        public static MvcHtmlString CheckBoxTableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            var tableBody = new TagBuilder("tbody");
            var attributes = CreateTableContent(ref tableBody, selectList, new AttributesTable { HasFirstChecksAll = true, HeaderTitle = "Descrição" });

            return MvcHtmlString.Create(CreateBaseTable(tableBody, attributes).ToString(TagRenderMode.Normal));
        }
        #endregion


        #region privates to table creation

        //para criar a estrutura principal da tabela (cria theader e junta tbody):
        private static TagBuilder CreateBaseTable(TagBuilder tableBody, AttributesTable attributes)
        {
            return CreateBaseTable(tableBody, attributes, null);
        }

        //para criar a estrutura principal da tabela (cria theader e junta tbody):
        private static TagBuilder CreateBaseTable(TagBuilder tableBody, AttributesTable attributes, object htmlAttributes)
        {
            //cria estrutura HTML da tabela:
            var table = new TagBuilder("table");

            if (htmlAttributes != null)
                table.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            table.AddCssClass("iCustomTable");
            table.AddCssClass("table-custom");

            //define o nome da coleção dos objectos a serem apresentados:
            if (!String.IsNullOrEmpty(attributes.FieldName))
            {
                table.MergeAttribute("data-fieldname", attributes.FieldName);
                table.MergeAttribute("id", attributes.FieldName);
            }

            //define o texto a apresentar no header da tabela:
            if (String.IsNullOrEmpty(attributes.HeaderTitle))
                attributes.HeaderTitle = "Designation";

            //criação do header da tabela:
            var tableHeader = new TagBuilder("thead");

            //criação de TD com checkbox para funcionalidade de checkar todos os elementos de uma só vez:
            if (attributes.HasFirstChecksAll)
            {
                var checkbox = new TagBuilder("input");
                checkbox.MergeAttribute("type", "checkbox");
                checkbox.AddCssClass("isCheckAll");
                if (attributes.AllChecked)
                {
                    checkbox.MergeAttribute("value", "true");
                    checkbox.MergeAttribute("checked", "true");
                }
                else
                {
                    checkbox.MergeAttribute("value", "false");
                    checkbox.MergeAttribute("checked", "false");
                }

                tableHeader.InnerHtml += "<tr><th>" + checkbox + "</th><th>" + attributes.HeaderTitle + "</th></tr>";
            }
            else
                tableHeader.InnerHtml += "<tr><th colspan='2'>" + attributes.HeaderTitle + "</th></tr>";

            //junção do theader e do tbody à tabela:
            table.InnerHtml += tableHeader;
            table.InnerHtml += tableBody;

            return table;
        }

        //para criar o conteudo da tabela (tbody):
        private static AttributesTable CreateTableContent(ref TagBuilder tableBody, IEnumerable<SelectListItem> selectList, AttributesTable attributes)
        {
            //criação de selectList com os dados a expor na tabela:
            var newList = new SelectList(selectList, "Id", (selectList as MultiSelectList).DataTextField);
            var selectedValues = (selectList as SelectList).SelectedValue as ICollection;
            if(selectedValues!=null)
                newList = new SelectList(selectedValues, "Id", (selectList as MultiSelectList).DataTextField);

            if (selectList != null)
            {
                int index = 0, countSelected = 0;

                foreach (var item in selectList)
                {
                    try
                    {
                        //verifica se o item conincide com os itens selecionados no model:
                        foreach (var selectedItem in from object selectedItem in selectedValues let valor = selectedItem.getPropertyValue((selectList as MultiSelectList).DataTextField) where selectedItem.getPropertyValue((selectList as MultiSelectList).DataTextField).ToString() == item.Text select selectedItem)
                        {
                            item.Selected = true;
                            if (attributes.isBackoffice)
                                item.Selected = bool.Parse(selectedItem.getPropertyValue("Selected").ToString().ToLower());
                            break;
                        }
                    }
                    catch(Exception) {}


                    //criação de nova linha da tabela:
                    var newTR = new TagBuilder("tr");
                    newTR.MergeAttribute("id", index.ToString());
                    newTR.AddCssClass(index%2 == 0 ? "odd" : "even");


                    //criação de checkbox a apresentar na tabela:
                    var checkbox = new TagBuilder("input");
                    checkbox.MergeAttribute("type", "checkbox");
                    if (attributes.isBackoffice)
                        checkbox.MergeAttribute("data-fieldname", "Selected");
                    if (attributes.AllChecked)
                    {
                        checkbox.MergeAttribute("value", "true");
                        checkbox.MergeAttribute("checked", "true");
                    }
                    else
                    {
                        checkbox.MergeAttribute("value", item.Selected.ToString().ToLower());
                        checkbox.MergeAttribute("checked", item.Selected.ToString().ToLower());
                    }


                    //à tabela são também adicionados campos (hidden) referentes ao Id da propriedade e à sua descrição:
                    var hiddenId = new TagBuilder("input");
                    hiddenId.MergeAttribute("type", "hidden");
                    hiddenId.MergeAttribute("value", item.Value);
                    hiddenId.MergeAttribute("id", "isId");
                    hiddenId.MergeAttribute("data-fieldname", String.IsNullOrEmpty(attributes.IdName) ? (selectList as MultiSelectList).DataValueField : attributes.IdName);

                    var hiddenDesc = new TagBuilder("input");
                    hiddenDesc.MergeAttribute("type", "hidden");
                    hiddenDesc.MergeAttribute("value", item.Text);
                    hiddenDesc.MergeAttribute("id", "isName");
                    hiddenDesc.MergeAttribute("data-fieldname", (selectList as MultiSelectList).DataTextField);


                    //atualização dos campos a inserir na tabela, ou seja, caso no model esses valores estejam selecionados, esse facto deve-se verifica visualmente e no momento de submissão dos dados:
                    if (selectedValues != null && selectedValues.Count != 0)
                        if (newList.Any(selectedItem => (String.IsNullOrEmpty(selectedItem.Text) && selectedItem.Value == item.Value) || (!String.IsNullOrEmpty(selectedItem.Text) && selectedItem.Text == item.Text && item.Selected)))
                        {
                            checkbox.MergeAttribute("value", "true");
                            checkbox.MergeAttribute("checked", "true");

                            if (!String.IsNullOrEmpty(attributes.FieldName))
                            {
                                //para os valores chegarem realmente selecionados ao controller deve-se garantir isso definindo o nome da coleção, o indice do objeto e o nome e valor de cada propriedade: 
                                if(attributes.isBackoffice)
                                    checkbox.MergeAttribute("name", attributes.FieldName + "[" + countSelected + "].Selected"); // para desaparecer no futuro
                                hiddenId.MergeAttribute("name", attributes.FieldName + "[" + countSelected + "]." + (String.IsNullOrEmpty(attributes.IdName) ? (selectList as MultiSelectList).DataValueField : attributes.IdName));
                                hiddenDesc.MergeAttribute("name", attributes.FieldName + "[" + countSelected + "]." + (selectList as MultiSelectList).DataTextField);
                            }
                            countSelected++;
                        }

                    //adiciona-se todos os campos aos respetivos TDs, adicionam-se os TDs à nova linha criada, e adiciona-se a linha ao tbody da tabela:
                    newTR.InnerHtml += "<td>" + hiddenId + hiddenDesc + checkbox + "</td><td>" + item.Text + "</td>";
                    tableBody.InnerHtml += newTR;
                    index++;
                }

                if (index == countSelected && countSelected != 0)
                    attributes.AllChecked = true;
            }

            return attributes;
        }
        #endregion


        #region aux privates
        private static bool HasMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }

        private static PropertyInfo getProperty(this object objectToCheck, string propertyName)
        {
            var type = objectToCheck.GetType();
            return type.GetProperty(propertyName);
        }
        
        private static object getPropertyValue(this object objectToCheck, string propertyName)
        {
            return objectToCheck.GetType().GetProperty(propertyName).GetValue(objectToCheck, null);
        }
        #endregion
    }



    /// <summary>
    ///     Data struct for CheckBoxTable custom HTML helper.
    /// </summary>
    public struct AttributesTable //estrutura própria e necessária para criar custom HTML tables personalizadas
    {
        /// <summary>
        /// Defines if table must have first check to check all the others
        /// </summary> 
        public bool HasFirstChecksAll;

        /// <summary>
        /// Defines if are all checkboxes checked
        /// </summary> 
        public bool AllChecked;

        /// <summary>
        /// Defines if is a backoffice model property
        /// </summary>
        public bool isBackoffice; //Backoffice entende-se como configuração (momento em que se define se é possível usar na app ou não)

        /// <summary>
        /// Defines model property name
        /// </summary>
        public string FieldName;

        /// <summary>
        /// Defines table header text
        /// </summary>
        public string HeaderTitle;

        /// <summary>
        /// Defines model ID property name
        /// </summary>
        public string IdName;
    }

}