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
            // save recruiter
            btnSave.OnClick += (sender, eventArgs) =>
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
                    this.InitForm();
                }
            };
            // create recruiter
            btnCreate.OnClick += (sender, eventArgs) =>
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    CRM.Recruiters.Create(txtName.Text,
                        txtEmail.Text,
                        txtTelephone.Text,
                        CRM.Companies[cmbCompanies.SelectedItemIndex].ID,
                        DateHelper.FromDutchDateString(txtFirstContact.Text),
                        txtNotes.Text);
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
