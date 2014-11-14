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
    public class Lead : BaseRTEntity
    {
        public Lead(DataRow dr, DataConnector dc) : base(dr, dc) { }
    }
    public class Leads : BaseRT<Lead>
    {
        public Leads(DataConnector dc)
            : base(dc)
        {
        }
    }
}
