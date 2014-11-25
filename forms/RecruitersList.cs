using System;
using OXStack.Helpers;
using RecruiterCRM.classes;
using RecruiterCRM.classes.Models;
using ConsoleFramework;
using ConsoleFramework.Controls;

namespace RecruiterCRM.forms
{
    /// <summary>
    /// Recruiters window
    /// </summary>
    public partial class RecruitersList : RecruiterCRM.classes.Views.CRMForm
    {
        /// <summary>
        /// Init form, loads stuffs, binds events..
        /// </summary>
        public override void InitForm()
        {
            CRM.Refresh();

            // fill recruiters
            lstRecruiters.Items.Clear();
            foreach (Recruiter rec in CRM.Recruiters)
                lstRecruiters.Items.Add(rec.Name + " (" + rec.Company.Name + ")");
            // fill combobox with companies
            cmbCompanies.Items.Clear();
            cmbCompanies.Items.AddRange(CRM.Companies.ToItemList());

            // Select recruiter
            lstRecruiters.SelectedItemIndexChanged += (sender, eventArgs) =>
            {
                int iSel = lstRecruiters.SelectedItemIndex;
                SelectRecruiter(CRM.Recruiters[iSel].ID);
            };
            lstRecruiters.SelectedItemIndex = 0;
            // delete recruiter
            lstRecruiters.KeyUp += (s, e) =>
            {
                if (e.wVirtualKeyCode == ConsoleFramework.Native.VirtualKeys.Delete)
                {
                    CRM.Recruiters[lstRecruiters.SelectedItemIndex].Delete();
                    InitForm();
                }
            };
            // save recruiter
            btnSave.OnClick += (s, e) =>
            {
                int iSel = lstRecruiters.SelectedItemIndex;
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    CRM.Recruiters[iSel].Name = txtName.Text;
                    CRM.Recruiters[iSel].Email = txtEmail.Text;
                    CRM.Recruiters[iSel].Telephone = txtTelephone.Text;
                    CRM.Recruiters[iSel].FirstContact = DateHelper.FromDutchDateString(txtFirstContact.Text);
                    CRM.Recruiters[iSel].Notes = txtNotes.Text;
                    CRM.Recruiters[iSel].CompanyID = CRM.Companies[cmbCompanies.SelectedItemIndex].ID;
                    CRM.Recruiters[iSel].Update();
                    InitForm();
                }
            };
        }

        /// <summary>
        /// View recruiter
        /// </summary>
        /// <param name="iRecruiterId"></param>
        public void SelectRecruiter(int iRecruiterId)
        {
            int iSel = CRM.Recruiters.FindIndex(iRecruiterId);
            txtName.Text = CRM.Recruiters[iSel].Name;
            txtEmail.Text = CRM.Recruiters[iSel].Email;
            txtTelephone.Text = CRM.Recruiters[iSel].Telephone;
            txtFirstContact.Text = CRM.Recruiters[iSel].FirstContact.ToString("dd-MM-yyyy");
            txtNotes.Text = CRM.Recruiters[iSel].Notes;
            cmbCompanies.SelectedItemIndex = CRM.Companies.FindIndexById(CRM.Recruiters[iSel].CompanyID);
        }

    }
}
