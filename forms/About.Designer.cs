﻿using RecruiterCRM.classes.Views.Helpers;
using RecruiterCRM.classes.Views;
using RecruiterCRM.classes;
using ConsoleFramework;
using ConsoleFramework.Controls;

namespace RecruiterCRM.forms
{
    /*
     *  GENERATED BY A YET-TO-BE-RELEASED CONSOLE EDITOR
     */
    [CRMFormTemplate("about")]
    public partial class About
    {
        public About(CRM crm, CRMFormLanguage lang) : base(crm, lang) { }
        /*
         *  CONTROLS
         */
        private Panel _pnlAbout = null;
        public Panel pnlAbout
        {
            get { return _pnlAbout ?? (_pnlAbout = HWND.FindChildByName<Panel>("pnlAbout")); }
            set { _pnlAbout = value; }
        }
        private Button _btnOK = null;
        public Button btnOK
        {
            get { return _btnOK ?? (_btnOK = pnlAbout.FindChildByName<Button>("btnOK")); }
            set { _btnOK = value; }
        }
    }
}