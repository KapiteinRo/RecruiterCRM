using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OXStack;
using OXStack.Helpers;
using OXStack.Data;
using OXStack.RT;

namespace RecruiterCRM.classes.Models
{
    /// <summary>
    /// Recruiting company
    /// </summary>
    public class Company : BaseRTEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Company name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Corporate website
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Notes & comments
        /// </summary>
        public string Notes { get; set; }

        private Recruiters _recrs = null;
        /// <summary>
        /// Recruiters belonging to this company
        /// </summary>
        public Recruiters Recruiters
        {
            get { return _recrs ?? (_recrs = new Recruiters(ID, DataConnector)); }
            set { _recrs = value; }
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dc"></param>
        public Company(DataRow dr, DataConnector dc) : base(dr, dc) { }

        /// <summary>
        /// Fetch a single company
        /// </summary>
        /// <param name="iID"></param>
        public Company(int iID, DataConnector dc) : base(null, dc) {
            DataRow dr = null;
            // use cache
            string[] saCacheParams = new[] { "Company", iID.ToString() };
            if (!Cache.Contains(saCacheParams))
            {
                DataTable dt = DataConnector.SelectDataTable("SELECT * FROM companies WHERE id = " + iID, true);
                if (Parser.IsDataTableNotEmpty(dt))
                {
                    dr = dt.Rows[0];
                    Cache.SetCache(saCacheParams, dr);
                }
            }
            else
                dr = Cache.GetCache(saCacheParams) as DataRow;
            _FillObject(dr);
        }
        /// <summary>
        /// Update company
        /// </summary>
        public void Update()
        {
            DataConnector.NonQuery(string.Format(@"
UPDATE companies SET
name = '{0}',
website = '{1}',
notes = '{2}'
WHERE id = {3}
", Name, Website, Notes, ID));
            _recrs = null;
            DataConnector.Dispose();
        }

        /// <summary>
        /// To string for list
        /// </summary>
        /// <returns>Company Name (number of recruiters)</returns>
        public override string ToString()
        {
            return Name + " (" + Recruiters.Count + ")";
        }
    }

    /// <summary>
    /// List of companies
    /// </summary>
    public class Companies : BaseRT<Company>
    {
        /// <summary>
        /// ctor, loads all the companies
        /// </summary>
        /// <param name="dc"></param>
        public Companies(DataConnector dc)
            : base(dc)
        {
            AddRange(Fill<Company>("SELECT * FROM companies ORDER BY name;"));
        }

        /// <summary>
        /// ctor, loads all the companies and fills recruiters too
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="recrs"></param>
        public Companies(DataConnector dc, Recruiters recrs)
            : base(dc)
        {
            AddRange(Fill<Company>("SELECT * FROM companies ORDER BY name;"));
            this.ForEach(c => c.Recruiters = new Recruiters(recrs.FindAll(r => r.CompanyID == c.ID), dc));
        }

        /// <summary>
        /// Creates new company
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sWebsite"></param>
        /// <param name="sNotes"></param>
        public void Create(string sName, string sWebsite, string sNotes)
        {
            DataConnector.NonQuery(string.Format(@"
INSERT INTO companies (name, website, notes)
VALUES ('{0}', '{1}', '{2}');", sName, sWebsite, sNotes));
            DataConnector.Dispose();
        }

        /// <summary>
        /// Index by ID
        /// </summary>
        /// <param name="iId"></param>
        /// <returns></returns>
        public int FindIndexById(int iId)
        {
            return this.FindIndex(x => x.ID == iId);
        }
        /// <summary>
        /// List of company names
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ToItemList()
        {
            foreach (Company rec in this)
                yield return rec.Name;
        }
    }
}
