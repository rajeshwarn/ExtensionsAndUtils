using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Framework
{
    public static class UtilExtensions
    {

        /// <summary>
        /// Para fazer a verificação 'LIKE' no acesso ao dataModel
        /// </summary>
        /// <param name="source">String a analisar</param>
        /// <param name="searchText">Texto a procurar no objeto do tipo string</param>
        /// <returns>Confirmação se existe ou não no objeto a string pretendida</returns>
        public static bool SqlContains(this string source, string searchText)
        {
            //Para functionar, ao objeto do dataModel tem que se fazer .ToList(), .ToArray() ou .Take(), etc 

            return source.SqlContains(searchText, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Para fazer a verificação 'LIKE' no acesso ao dataModel
        /// </summary>
        /// <param name="source">String a analisar</param>
        /// <param name="searchText">Texto a procurar no objeto do tipo string</param>
        /// <param name="comparisonType">Tipo de comparação</param>
        /// <returns>Confirmação se existe ou não no objeto a string pretendida</returns>
        public static bool SqlContains(this string source, string searchText, StringComparison comparisonType)
        {
            if (String.IsNullOrEmpty(source)) source = "";
            if (String.IsNullOrEmpty(searchText)) searchText = "";

            //tira os acentos das palavras
            var nonSpacingMarkRegex = new Regex(@"\p{Mn}", RegexOptions.Compiled);

            var normalizedSearch = searchText.Normalize(NormalizationForm.FormD);
            searchText = nonSpacingMarkRegex.Replace(normalizedSearch, string.Empty);

            var normalizedSource = source.Normalize(NormalizationForm.FormD);
            source = nonSpacingMarkRegex.Replace(normalizedSource, string.Empty);
                

            //Para functionar, ao objeto do dataModel tem que se fazer .ToList(), .ToArray() ou .Take(), etc 
            source = source.ToLower();
            searchText = searchText.ToLower();

            char[] delimiterChars = { '%' };

            searchText = String.IsNullOrWhiteSpace(searchText) || String.IsNullOrEmpty(searchText) ? "%" : searchText;

            if (searchText == "%")
                return true;

            string[] words = searchText.Split(delimiterChars);

            foreach (string word in words)
                if (source.IndexOf(word, comparisonType) < 0)
                    return false;

            return true;
        }



        public static string LoadUserControl<T>(Page page, T uc) where T : UserControl
        {
            uc.ID = Guid.NewGuid().ToString();
            var form = new HtmlForm();
            form.Controls.Add((T)uc);
            page.Controls.Add(form);
            var sw = new StringWriter();
            HttpContext.Current.Server.Execute(page, sw, false);
            string toReturn = sw.ToString();
            sw.Close();
            return CleanHtml(toReturn);
        }


        public static string LoadUserControlToEmail<T>(Page page, T uc) where T : UserControl
        {
            uc.ID = Guid.NewGuid().ToString();
            var form = new HtmlForm();
            form.Controls.Add((T)uc);
            page.Controls.Add(form);
            var sw = new StringWriter();
            HttpContext.Current.Server.Execute(page, sw, false);
            string returned = sw.ToString();
            sw.Close();
            var toReturn = CleanHtml(returned);

            toReturn = toReturn.Replace("\r", String.Empty)
                            .Replace("\n", String.Empty)
                            .Replace("\t", String.Empty)
                            .Replace("\"", "'");

            return toReturn;
        }


        public static string CleanHtml(string html)
        {
            html = html.Replace("theForm", "prego2");
            html = html.Replace("__doPostBack", "__prego");
            html = Regex.Replace(html, "<input[^>]*id=\"(__VIEWSTATE)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<input[^>]*id=\"(__EVENTVALIDATION)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);

            html = Regex.Replace(html, "<input[^>]*id=\"(__EVENTTARGET)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<input[^>]*id=\"(__EVENTARGUMENT)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, "<input[^>]*id=\"(__VIEWSTATEGENERATOR)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase); //isto não pode ter senão o JS passa-se
                
            html = Regex.Replace(html, @"<[/]?(form|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);

            html = html.Replace("\r", String.Empty);
            html = html.Replace("\n", String.Empty);
            html = html.Replace("\t", String.Empty);
            //html = html.Replace("\"", "'"); //isto não pode ter senão o JS passa-se
           

            return html;
        }

        public static string DisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            var member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
                outString = ((DisplayAttribute)attrs[0]).GetName();

            return outString;
        }

        public static bool IsValidUrl(this string urlString)
        {
            Uri uri;
            if (!Uri.TryCreate(urlString, UriKind.Absolute, out uri))
                    return false;

            if (uri.Scheme != Uri.UriSchemeHttp
               && uri.Scheme != Uri.UriSchemeHttps
               && uri.Scheme != Uri.UriSchemeFtp
               && uri.Scheme != Uri.UriSchemeMailto)
                return false;

            return true;
        }

        public static string GetUrlToValidate(string urlString)
        {
            if (String.IsNullOrEmpty(urlString.Trim()))
                return "";

            Uri uri;
            try
            {
                if (urlString.Contains(' ') && Uri.TryCreate(urlString.Substring(0, urlString.IndexOf(' ')), UriKind.Absolute, out uri))
                    return urlString.Substring(0, urlString.IndexOf(' '));
                else
                    return urlString;
            }
            catch { return urlString; }
        }



        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static T FindByIndex<T>(this IEnumerable<T> collection, int index)
        {
            return collection.Skip(index).Take(1).FirstOrDefault();
        }

        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var i = 0;
            foreach (var item in source)
            {
                if (predicate(item)) return i;
                i++;
            }

            return -1;
        }

        public static bool Non<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool SafeAny<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        public static bool SafeAny<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return list != null && list.Any(predicate);
        }

        public static IEnumerable<T> GetValues<T>(this T enumList)
        {
            Type enumType = enumList.GetType();
            return Enum.GetValues(enumType).Cast<T>();
        }
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static bool IsAllEmptyOrNull(this string text)
        {
            return String.IsNullOrEmpty(text) || String.IsNullOrEmpty(text.Trim());
        }


        #region OrderBy
        
        public static IOrderedEnumerable<TSource> ToOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (!source.SafeAny())
                return source.OrderBy(keySelector);

            if(keySelector(source.First()).GetType() == typeof(string))
                return source.OrderBy(element => Utils.RemoveDiacritics(keySelector(element).ToString()));
            else
                return source.OrderBy(keySelector);
        }

        public static IOrderedEnumerable<TSource> ToOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.OrderBy(keySelector, comparer);
        }

        public static IOrderedQueryable<TSource> ToOrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            try
            {
                var culture = new CultureInfo("pt-PT");

                return source.OrderBy(element => element.ToString(), StringComparer.Create(culture, false));
            }
            catch
            {
                return source.OrderBy(keySelector);
            }
        }

        public static IOrderedQueryable<TSource> ToOrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.OrderBy(keySelector, comparer);
        }


        public static IOrderedEnumerable<TSource> ToOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (!source.SafeAny())
                return source.OrderByDescending(keySelector);

            return source.OrderByDescending(element => Utils.RemoveDiacritics(keySelector(element).ToString()));
        }

        public static IOrderedEnumerable<TSource> ToOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.OrderByDescending(keySelector, comparer);
        }

        public static IOrderedQueryable<TSource> ToOrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (!source.SafeAny())
                return source.OrderByDescending(keySelector);

            return source.OrderByDescending(element => Utils.RemoveDiacritics(element.ToString()));
        }

        public static IOrderedQueryable<TSource> ToOrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.OrderByDescending(keySelector, comparer);
        }



        //public static IOrderedEnumerable<TSource> ToThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //{
        //    if (!source.SafeAny())
        //        return null;

        //    if (keySelector(source.First()).GetType() == typeof(string))
        //        return source.ThenBy(element => Utils.RemoveDiacritics(keySelector(element).ToString()));
        //    else
        //        return source.ThenBy(keySelector);
        //}

        //public static IOrderedEnumerable<TSource> ToThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        //{
        //    if (!source.SafeAny())
        //        return null;

        //    return source.ThenBy(keySelector, comparer);
        //}

        public static IOrderedEnumerable<TSource> ToThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            var expression = keySelector.Compile();

            if (!source.SafeAny())
                return source.ThenBy(expression);

            if (expression(source.First()).GetType() == typeof(string))
                return source.ThenBy(element => Utils.RemoveDiacritics(expression(element).ToString()));
            else
                return source.ThenBy(expression);
        }

        public static IOrderedEnumerable<TSource> ToThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.ThenBy(keySelector.Compile(), comparer);
        }
        
        public static IOrderedQueryable<TSource> ToThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            try
            {
                var culture = new CultureInfo("pt-PT");
                return source.ThenBy(element => element.ToString(), StringComparer.Create(culture, false));
            }
            catch
            {
                return source.ThenBy(keySelector);
            }
        }

        public static IOrderedQueryable<TSource> ToThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.ThenBy(keySelector, comparer);
        }

        

        //public static IOrderedEnumerable<TSource> ToThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //{
        //    if (!source.SafeAny())
        //        return null;

        //    if (keySelector(source.First()).GetType() == typeof(string))
        //        return source.ThenByDescending(element => Utils.RemoveDiacritics(keySelector(element).ToString()));
        //    else
        //        return source.ThenByDescending(keySelector);
        //}

        //public static IOrderedEnumerable<TSource> ToThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        //{
        //    if (!source.SafeAny())
        //        return null;

        //    return source.ThenByDescending(keySelector, comparer);
        //}

        public static IOrderedEnumerable<TSource> ToThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            var expression = keySelector.Compile();

            if (!source.SafeAny())
                return source.ThenByDescending(expression);


            if (expression(source.First()).GetType() == typeof(string))
                return source.ThenByDescending(element => Utils.RemoveDiacritics(expression(element).ToString()));
            else
                return source.ThenByDescending(expression);
        }

        public static IOrderedEnumerable<TSource> ToThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.ThenByDescending(keySelector.Compile(), comparer);
        }

        public static IOrderedQueryable<TSource> ToThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (!source.SafeAny())
                return source.ThenByDescending(keySelector);

            return source.ThenByDescending(element => Utils.RemoveDiacritics(element.ToString()));
        }

        public static IOrderedQueryable<TSource> ToThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return source.ThenByDescending(keySelector, comparer);
        }
        

        #endregion
    
	
        /// <summary>
        /// Trunca uma string caso o seu tamanho seja superior ao número de caracteres passados por parametro
        /// </summary>
        /// <param name="texto">Texto a truncar</param>
        /// <param name="numeroCaracteres">Número máximo de caracteres</param>
        /// <returns>String com o texto truncado</returns>
        public static string Shorten(this string texto, int numeroCaracteres)
        {
            if (texto.Count() > (numeroCaracteres + 3))
                return texto.Substring(0, numeroCaracteres) + "...";
            return texto;
        }
	}
}
