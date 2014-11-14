using System;
using OXStack.Helpers;
using RecruiterCRM.classes;
using RecruiterCRM.classes.Views;
using RecruiterCRM.classes.Models;
using ConsoleFramework;
using ConsoleFramework.Controls;

namespace RecruiterCRM.forms
{
    public partial class RecruitersSearch : RecruiterCRM.classes.Views.CRMForm
    {
        public override void InitForm()
        {
            CRM.Refresh();
            // position
            HWND.X = 5;
            HWND.Y = 5;
            // empty
            lstResults.Items.Clear();
            switch (CurrentLanguage)
            {
                case CRMFormLanguage.EN: lstResults.Items.Add("- enter a keyword -"); break;
                case CRMFormLanguage.NL: lstResults.Items.Add("- voer een zoekterm in -"); break;
            }

            // bind events
            btnClose.OnClick += (sndr, eventrgs) => this.Close();
        }

    }
}
