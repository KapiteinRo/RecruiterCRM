using RecruiterCRM.classes;
using ConsoleFramework.Controls;

namespace RecruiterCRM.classes.Views
{
    /// <summary>
    /// Selected language
    /// </summary>
    public enum CRMFormLanguage
    {
        EN,
        NL
    };
    /// <summary>
    /// Interface to form
    /// </summary>
    public interface ICRMForm
    {
        Window HWND { get; set; }
        CRM CRM { get; set; }
        CRMFormLanguage CurrentLanguage { get; set; }
        string Template { get; set; }

        void InitForm();
    }
}
