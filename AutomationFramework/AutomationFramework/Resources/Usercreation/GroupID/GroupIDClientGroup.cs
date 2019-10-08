
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;


namespace UserCreation.GroupId
{
    /// <summary>
    /// Description of GroupIDClientGroup.
    /// </summary>
    
     
    public class GroupIDClientGroup
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
       
		public string ParentGroupID { get; set; }
		public string GroupID { get; set; }
		public string GroupName { get; set; }
		public string GroupAddress1 { get; set; }
		public string GroupCity { get; set; }
		public string GroupState { get; set; }
		public string GroupPhoneNo { get; set; }

        public GroupIDClientGroup()
        {
            // Do not delete - a parameterless constructor is required!
        }

    }
}
