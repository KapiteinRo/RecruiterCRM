using System;
using RecruiterCRM.classes;
using RecruiterCRM.classes.Models;
using ConsoleFramework;
using ConsoleFramework.Controls;

namespace RecruiterCRM.forms
{
    /// <summary>
    /// Companies window
    /// </summary>
    public partial class CompaniesList : RecruiterCRM.classes.Views.CRMForm
    {
        /// <summary>
        /// Init form, loads stuffs, binds events..
        /// </summary>
        public override void InitForm()
        {
            CRM.Refresh();
            
            // fill companies list
            lstCompanies.Items.Clear();
            foreach (Company comp in CRM.Companies)
                lstCompanies.Items.Add(comp.ToString());
            
            // save edit
            btnSave.OnClick += (snr, eventrgs) =>
            {
                int iSel = lstCompanies.SelectedItemIndex;
                CRM.Companies[iSel].Name = txtCompName.Text;
                CRM.Companies[iSel].Website = txtCompWebsite.Text;
                CRM.Companies[iSel].Notes = txtCompNotes.Text;
                CRM.Companies[iSel].Update();
                this.InitForm();
            };
            // create
            btnCreate.OnClick += (snr, eventrgs) =>
            {
                CRM.Companies.Create(txtCompName.Text, txtCompWebsite.Text, txtCompNotes.Text);
                this.InitForm();
            };
            // list selectedchange
            lstCompanies.SelectedItemIndexChanged += (sndr, eventrgs) =>
            {
                int iSel = lstCompanies.SelectedItemIndex;
                txtCompName.Text = CRM.Companies[iSel].Name;
                txtCompWebsite.Text = CRM.Companies[iSel].Website;
                txtCompNotes.Text = CRM.Companies[iSel].Notes;
                lstCompRecruiters.Items.Clear();
                foreach (Recruiter recr in CRM.Companies[iSel].Recruiters)
                    lstCompRecruiters.Items.Add(recr.Name);
            };
        }

    }
}
