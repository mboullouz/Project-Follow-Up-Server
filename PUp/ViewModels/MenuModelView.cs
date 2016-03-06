using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels
{
    public class MenuModelView
    {
        public static List<Models.Entity.ProjectEntity> ActiveProject()
        {
            Models.DatabaseContext dbc = new Models.DatabaseContext();
            return dbc.ProjectSet.Where(p => p.EndAt > DateTime.Now && p.Deleted == false).ToList();
        }
    }
}