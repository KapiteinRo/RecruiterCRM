using System;
using RecruiterCRM.classes;
using ConsoleFramework;
using ConsoleFramework.Controls;

namespace RecruiterCRM.forms
{
    /// <summary>
    /// About-window featuring copyright and such
    /// </summary>
    public partial class About : RecruiterCRM.classes.Views.CRMForm
    {
        /// <summary>
        /// Init form, loads stuffs, binds events..
        /// </summary>
        public override void InitForm()
        {
            CRM.Refresh();
            btnOK.OnClick += (sndr, eventrgs) => this.Close();
        }

    }
}
