using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using TestApp.Models;

namespace TestApp.Components
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString TagsFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var text = new StringWriter();
            var div = new HtmlTextWriter(text);
            div.AddAttribute(HtmlTextWriterAttribute.Id, "Tags");

            div.RenderBeginTag(HtmlTextWriterTag.Ul);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var tagsData = metadata.Model as ICollection<Tag> ?? new List<Tag>();

            foreach (var item in tagsData)
            {
                div.RenderBeginTag(HtmlTextWriterTag.Li);
                div.Write(item.Name);
                div.RenderEndTag();
            }
            div.RenderEndTag();
            return MvcHtmlString.Create(text.ToString());
        }
    }
}