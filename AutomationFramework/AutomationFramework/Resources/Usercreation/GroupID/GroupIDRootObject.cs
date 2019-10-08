/*
 * Created by Ranorex
 * User: Goyal
 * Date: 4/21/2017
 * Time: 3:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;


namespace UserCreation.GroupId
{
    /// <summary>
    /// Description of GroupIDRootObject.
    /// </summary>
    
    public class GroupIDRootObject
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public List<GroupIDClientGroup> Groups { get; set; }
     	public int totalCount { get; set; }
     	
        public GroupIDRootObject()
        {
            // Do not delete - a parameterless constructor is required!
        }

    }
}
