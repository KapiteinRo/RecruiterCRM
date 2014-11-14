using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OXStack;
using OXStack.Config;
using OXStack.Data;
using RecruiterCRM.classes.Models;
using RecruiterCRM.classes.Config;

namespace RecruiterCRM.classes
{
    /// <summary>
    /// Main logic class
    /// </summary>
    public class CRM : Base
    {
        /*
         *  DATA LOGIC
         */
        private Recruiters _recrs = null;
        /// <summary>
        /// All the recruiters
        /// </summary>
        public Recruiters Recruiters { get { return _recrs ?? (_recrs = new Recruiters(this.DataConnector)); } }

        private Companies _comps = null;
        /// <summary>
        /// All the companies
        /// </summary>
        public Companies Companies { get { return _comps ?? (_comps = new Companies(this.DataConnector)); } }

        /*
         *  CONFIGURATIONS
         */
        private DataConfig _dataConfig = null;
        /// <summary>
        /// Configuration to database
        /// </summary>
        public DataConfig DataConfig { get { return Config.Load<DataConfig>(ref _dataConfig); } }

        /// <summary>
        /// ctor
        /// </summary>
        public CRM()
        {
            // load from conifg
            this.DataConnector = new DataConnector(DataConfig.ConnectionString);
        }

        /// <summary>
        /// Empty everything
        /// </summary>
        public void Refresh()
        {
            if (_recrs != null)
                _recrs = null;
            if (_comps != null)
                _comps = null;
        }
    }
}
