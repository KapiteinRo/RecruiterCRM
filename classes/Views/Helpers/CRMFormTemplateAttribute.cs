using System;

namespace RecruiterCRM.classes.Views.Helpers
{
    public class CRMFormTemplateAttribute : Attribute
    {
        /// <summary>
        /// Desired template..
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// Map this property with this template.
        /// </summary>
        /// <param name="sTemplate"></param>
        public CRMFormTemplateAttribute(string sTemplate)
        {
            Template = sTemplate;
        }

    }
}
