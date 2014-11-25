using System;
using System.Collections.Generic;
using System.Linq;
using System;
using OXStack.Helpers;
using RecruiterCRM.classes;
using RecruiterCRM.classes.Models;
using ConsoleFramework;
using ConsoleFramework.Controls;

namespace RecruiterCRM.forms
{
    public partial class RecruiterCreate : RecruiterCRM.classes.Views.CRMForm
    {
        public override void InitForm()
        {
            CRM.Refresh();

            // fill combobox with companies
            cmbCompanies.Items.Clear();
            cmbCompanies.Items.AddRange(CRM.Companies.ToItemList());

            // save new recruiter
            btnSave.OnClick += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    CRM.Recruiters.Create(txtName.Text,
                        txtEmail.Text,
                        txtTelephone.Text,
                        CRM.Companies[cmbCompanies.SelectedItemIndex].ID,
                        DateHelper.FromDutchDateString(txtFirstContact.Text),
                        txtNotes.Text);
                    MessageBox.Show("Add Recruiter", txtName.Text + " has been saved.", (mbe) =>
                    {
                    });
                    CallForRefreshParent(string.Empty);
                    Close();
                }
            };
        }
    }
}
