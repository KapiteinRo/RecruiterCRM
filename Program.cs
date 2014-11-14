using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;
using ConsoleFramework;
using ConsoleFramework.Controls;
using ConsoleFramework.Core;
using ConsoleFramework.Xaml;
using Xaml;
using System.Text;
using OXStack.Helpers;
using RecruiterCRM.forms;
using RecruiterCRM.classes;
using RecruiterCRM.classes.Views;
using RecruiterCRM.classes.Models;

namespace RecruiterCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            // load teh database
            CRM crm = new CRM();

            // language selector here?
            // nahh..not now
            CRMFormLanguage lang = CRMFormLanguage.EN;
            // load main window parent
            WindowsHost wndMain = ConsoleApplication.LoadFromXaml("RecruiterCRM.forms." + lang.ToString().ToLower() + ".windows-host.xml", null) as WindowsHost;

            // forms
            About frmAbout = new About(crm, lang);
            CompaniesList frmCompanies = new CompaniesList(crm, lang);
            RecruitersList frmRecruiters = new RecruitersList(crm, lang);
            RecruitersSearch frmRecruitersSearch = new RecruitersSearch(crm, lang);
            LeadsList frmLeads = new LeadsList(crm, lang);

            // list first
            wndMain.Show(frmRecruiters.Show());
            
            // bind public events from forms

            // view companies
            frmRecruiters.btnCompanies.OnClick+= (s, e) => wndMain.Show(frmCompanies.Show());
            // search recruiters
            frmRecruitersSearch.txtSearch.KeyUp += (s, e) =>
            {
                if (frmRecruitersSearch.txtSearch.Text.Trim().Length > 2)
                {
                    frmRecruitersSearch.lstResults.Items.Clear();
                    foreach (Recruiter rec in crm.Recruiters.Find(frmRecruitersSearch.txtSearch.Text.Trim()))
                        frmRecruitersSearch.lstResults.Items.Add(rec.ToString());
                }
            };
            // go to search result
            frmRecruitersSearch.btnGoto.OnClick += (s, e) =>
            {
                if (frmRecruitersSearch.lstResults.SelectedItemIndex >= 0)
                {
                    // refresh recruiters window
                    try
                    {
                        frmRecruiters.Close();
                    }
                    catch { }
                    finally { wndMain.Show(frmRecruiters.Show()); }
                    // go to selected recruiters
                    frmRecruiters.SelectRecruiter(crm.Recruiters.Fetch(frmRecruitersSearch.lstResults.Items[frmRecruitersSearch.lstResults.SelectedItemIndex]).ID);
                    // close search
                    frmRecruitersSearch.Close();
                }
            };
            
            

            // handle menu bindings
            List<Control> menuItems = VisualTreeHelper.FindAllChilds(wndMain.MainMenu, control => control is MenuItem);
            foreach (Control menuItem in menuItems)
            {
                MenuItem item = (MenuItem)menuItem;
                foreach (Control subMenuItem in item.Items)
                {
                    if (subMenuItem is MenuItem)
                    {
                        MenuItem mi = subMenuItem as MenuItem;
                        switch (mi.Name)
                        {
                            case "mnuAbout":
                                mi.Click += (s, e) => wndMain.Show(frmAbout.Show());
                                break;
                            case "mnuExit":
                                mi.Click += (s, e) => Environment.Exit(0);
                                break;
                            case "lstRecruiters":
                                mi.Click += (s, e) => wndMain.Show(frmRecruiters.Show());
                                break;
                            case "lstCompanies":
                                mi.Click += (s, e) => wndMain.Show(frmCompanies.Show());
                                break;
                            case "searchRecruiters":
                                mi.Click += (s, e) => wndMain.Show(frmRecruitersSearch.Show());
                                break;
                            case "lstLeads":
                                mi.Click += (s, e) => wndMain.Show(frmLeads.Show());
                                break;
                        }
                    }
                }
            }
            // run
            ConsoleApplication.Instance.Run(wndMain);
        }
    }
}
