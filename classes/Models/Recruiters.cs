using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OXStack;
using OXStack.Helpers;
using OXStack.Data;
using OXStack.RT.Mappers;
using OXStack.RT;

namespace RecruiterCRM.classes.Models
{
    /// <summary>
    /// Recruiter
    /// </summary>
    public class Recruiter : BaseRTEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of recruiter
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// E-mail address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Telephone
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Company ID of the company where the recruiter belongs to
        /// </summary>
        [RTDataCol("company_id")]
        public int CompanyID { get; set; }
        /// <summary>
        /// First date of contact
        /// </summary>
        [RTDataCol("first_contact")]
        public DateTime FirstContact { get; set; }
        /// <summary>
        /// Notes & comments
        /// </summary>
        public string Notes { get; set; }

        private Company _comp = null;
        /// <summary>
        /// Company the recruiter belongs to
        /// </summary>
        public Company Company { get { return _comp ?? (_comp = new Company(CompanyID, DataConnector)); } }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dc"></param>
        public Recruiter(DataRow dr, DataConnector dc) : base(dr, dc) { }
        /// <summary>
        /// Updates recruiter
        /// </summary>
        public void Update()
        {
            this.DataConnector.NonQuery(string.Format(@"
UPDATE recruiters SET
name = '{0}',
email = '{1}',
telephone = '{2}',
company_id = {3},
first_contact = '{4}',
notes = '{5}'
WHERE id = {6}
", Name, Email, Telephone, CompanyID, FirstContact.ToString("yyyy-MM-dd"), Notes, ID));
            _comp = null;
            this.DataConnector.Dispose();
        }
        /// <summary>
        /// To string
        /// </summary>
        /// <returns>Name (Company Name)</returns>
        public override string ToString()
        {
            return this.Name + " (" + this.Company.Name + ")";
        }
    }
    /// <summary>
    /// List of recruiters
    /// </summary>
    public class Recruiters : BaseRT<Recruiter>
    {
        /// <summary>
        /// ctor, loads all recruiters
        /// </summary>
        /// <param name="dc"></param>
        public Recruiters(DataConnector dc) : base(dc)
        {
            this.AddRange(Fill<Recruiter>("SELECT * FROM recruiters ORDER BY name;"));
        }
        /// <summary>
        /// Loads recruiters of a certain company
        /// </summary>
        /// <param name="iCompanyId"></param>
        /// <param name="dc"></param>
        public Recruiters(int iCompanyId, DataConnector dc) : base(dc)
        {
            this.AddRange(Fill<Recruiter>("SELECT * FROM recruiters WHERE company_id = " + iCompanyId + " ORDER BY name;"));
        }
        /// <summary>
        /// To string list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ToItemList()
        {
            foreach (Recruiter rec in this)
                yield return rec.ToString();
        }
        /// <summary>
        /// Creates recruiter
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sEmail"></param>
        /// <param name="sTelephone"></param>
        /// <param name="iCompanyId"></param>
        /// <param name="dtFirstContact"></param>
        /// <param name="sNotes"></param>
        public void Create(string sName, string sEmail, string sTelephone, int iCompanyId, DateTime dtFirstContact, string sNotes)
        {
            this.DataConnector.NonQuery(string.Format(@"
INSERT INTO recruiters (name, email, telephone, company_id, first_contact, notes)
VALUES ('{0}', '{1}', '{2}', {3}, '{4}', '{5}');", sName, sEmail, sTelephone, iCompanyId, dtFirstContact.ToString("yyyy-MM-dd"), sNotes));
            this.DataConnector.Dispose();
        }
        /// <summary>
        /// Find recruiters
        /// </summary>
        /// <param name="sSearchTerm"></param>
        /// <returns></returns>
        public List<Recruiter> Find(string sSearchTerm)
        {
            sSearchTerm = sSearchTerm.ToLower();
            return this.FindAll(x => x.Name.Contains(sSearchTerm) ||
                x.Email.ToLower().Contains(sSearchTerm) ||
                x.Notes.ToLower().Contains(sSearchTerm) ||
                x.Company.Name.ToLower().Contains(sSearchTerm));
        }
        /// <summary>
        /// Get index by recruiter ID
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public int FindIndex(int iID)
        {
            return this.FindIndex(x => x.ID == iID);
        }
        /// <summary>
        /// Fetch by name
        /// </summary>
        /// <param name="sNameAndCompany"></param>
        /// <returns></returns>
        public Recruiter Fetch(string sNameAndCompany)
        {
            return this[this.FindIndex(x => x.ToString() == sNameAndCompany)];
        }
    }
}
