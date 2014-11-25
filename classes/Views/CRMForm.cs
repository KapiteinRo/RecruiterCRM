using System;
using System.Reflection;
using OXStack.Helpers;
using RecruiterCRM.classes;
using RecruiterCRM.classes.Models;
using RecruiterCRM.classes.Views.Helpers;
using ConsoleFramework;
using ConsoleFramework.Controls;

namespace RecruiterCRM.classes.Views
{
    /// <summary>
    /// Baseform class
    /// </summary>
    public class CRMForm : ICRMForm
    {
        /// <summary>
        /// delegate to refresh parent event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void RefreshParentHandler(object sender, RefreshParentEventArgs e);
        /// <summary>
        /// Call for a refresh of the parent window.
        /// </summary>
        public event RefreshParentHandler OnRefreshParent;

        /// <summary>
        /// Main logic class
        /// </summary>
        public CRM CRM { get; set; }

        private Window _hwnd = null;
        /// <summary>
        /// Window handle
        /// </summary>
        public Window HWND
        {
            get { return _hwnd ?? (_hwnd = ConsoleApplication.LoadFromXaml("RecruiterCRM.forms." + CurrentLanguage.ToString().ToLower() + "." + Template + ".xml", null) as Window); }
            set { _hwnd = value; }
        }
        /// <summary>
        /// Current form language
        /// </summary>
        public CRMFormLanguage CurrentLanguage { get; set; }
        private string _sTemplate = string.Empty;
        /// <summary>
        /// Template name
        /// </summary>
        public string Template
        {
            get
            {
                // fetch template from attribute
                if (string.IsNullOrEmpty(_sTemplate))
                    foreach (object oTmp in this.GetType().GetCustomAttributes(true))
                        if (oTmp is CRMFormTemplateAttribute)
                            _sTemplate = (oTmp as CRMFormTemplateAttribute).Template;
                return _sTemplate;
            }
            set { _sTemplate = value; }
        }

        /// <summary>
        /// Init form
        /// </summary>
        public virtual void InitForm() { }

        /// <summary>
        /// Close, check if it is closeable
        /// </summary>
        public void Close()
        {
            if (HWND.Parent != null)
                HWND.Close();
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="crm"></param>
        /// <param name="curLang"></param>
        public CRMForm(CRM crm, CRMFormLanguage curLang)
        {
            CRM = crm;
            CurrentLanguage = curLang;
        }

        /// <summary>
        /// Show window, inits form
        /// </summary>
        /// <returns></returns>
        public Window Show()
        {
            InitForm();

            // handle several keys:
            // F5 = refresh
            // Escape = close
            HWND.KeyUp += (s, e) =>
            {
                switch (e.wVirtualKeyCode)
                {
                    case ConsoleFramework.Native.VirtualKeys.F5: InitForm(); break;
                    case ConsoleFramework.Native.VirtualKeys.Escape: Close(); break;
                }
            };

            return HWND;
        }

        /// <summary>
        /// Refresh parent window
        /// </summary>
        /// <param name="sParent"></param>
        protected void CallForRefreshParent(string sParent)
        {
            if (OnRefreshParent == null) return;
            OnRefreshParent(this, new RefreshParentEventArgs(sParent));
        }
    }

    /// <summary>
    /// Event args for refreshing a parent
    /// </summary>
    public class RefreshParentEventArgs : EventArgs
    {
        public string ParentID { get; set; }
        public RefreshParentEventArgs(string sParent)
        {
            ParentID = sParent;
        }
    }
}
