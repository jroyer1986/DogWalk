using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DogWalk.Helpers
{
    public static class HtmlHelpers
    {

        public static MvcHtmlString EnumDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, object htmlAttributes = null)
        {
            //get metadata from the calling model
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            //get selectedVAlue, if it exists
            string selectedValue = metadata.Model == null ? "" : metadata.Model.ToString();
            //create the select list, using the enum and the selected value
            SelectList selectList = ToSelectList(enumType, selectedValue);
            //return the drop down list
            return htmlHelper.DropDownListFor(expression, selectList, htmlAttributes);
        }

        public static SelectList ToSelectList(Type enumType, string selectedValue)
        {
            //create contianer to hold the output
            var items = new List<SelectListItem>();
            //iterate through the enum
            foreach (var item in Enum.GetValues(enumType))
            {
                //get field type
                FieldInfo fi = enumType.GetField(item.ToString());
                //and any desc attributes
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                //set title
                var title = "";
                //if there are attribtues, use as the title
                if (attributes != null && attributes.Length > 0)
                {
                    //set title
                    title = attributes[0].Description;
                }
                else
                {
                    //otherwise, use the string value
                    title = item.ToString();
                }
                //create new list item
                var listItem = new SelectListItem
                {
                    //value is the title
                    Value = title,
                    //as is text
                    Text = title,
                    //and if it equals the selected val, mark it selected
                    Selected = selectedValue == title.ToString(),
                };
                //add to the collection
                items.Add(listItem);
            }
            //return the list
            return new SelectList(items, "Value", "Text", 1);
        }
         public static IEnumerable<SelectListItem> ToSelectListEnumerable(Type enumType, string selectedValue)
        {
            //create contianer to hold the output
            var items = new List<SelectListItem>();
            //iterate through the enum
            foreach (var item in Enum.GetValues(enumType))
            {
                //get field type
                FieldInfo fi = enumType.GetField(item.ToString());
                //and any desc attributes
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                //set title
                var title = "";
                //if there are attribtues, use as the title
                if (attributes != null && attributes.Length > 0)
                {
                    //set title
                    title = attributes[0].Description;
                }
                else
                {
                    //otherwise, use the string value
                    title = item.ToString();
                }
                //create new list item
                var listItem = new SelectListItem
                {
                    //value is the title
                    Value = title,
                    //as is text
                    Text = title,
                    //and if it equals the selected val, mark it selected
                    Selected = selectedValue == title.ToString(),
                };
                //add to the collection
                items.Add(listItem);
            }
            //return the list
            return items;
        }
    }
}