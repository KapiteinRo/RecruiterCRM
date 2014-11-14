using System;
using System.Collections.Generic;
using System.Text;
using OXStack.Config;

namespace RecruiterCRM.classes.Config
{
    /// <summary>
    /// Config to SQLLite Database
    /// </summary>
    public class DataConfig : ConfigHelper, IDataConnectorConfig
    {
        /// <summary>
        /// Path to database
        /// </summary>
        public string FileName { get { return Get("FileName", string.Empty); } }
        /// <summary>
        /// Path to password
        /// </summary>
        public string PassWord { get { return Get("PassWord", string.Empty); } }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString
        {
            get { return "Data Source=" + FileName + ";Version=3;Password=" + PassWord + ";"; }
        }
    }
}
