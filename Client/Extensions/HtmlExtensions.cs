using System;
using System.Web.Mvc;

namespace Client.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper html, byte[] image)
        {
            if (image != null)
            {
                var img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
                return new MvcHtmlString("<img src='" + img + "' height=\"150\" width=\"150\" />");
            }
            return new MvcHtmlString(" <img src=\"/Content/Images/noimage.png\"width=\"150\" height=\"150\" />");
        }

        public static MvcHtmlString DayGreeting(this HtmlHelper html)
        {
            string msg;
            var hour = DateTime.Now.Hour;
            if (hour < 12 && hour > 5)
                msg = "Good morning";
            else if (hour > 11 && hour < 19)
                msg = "Good Afternoon";
            else if (hour > 18 && hour < 23)
                msg = "Good Evning";
            else
                msg = "Good night";
            return MvcHtmlString.Create(msg);
        }
    }
}