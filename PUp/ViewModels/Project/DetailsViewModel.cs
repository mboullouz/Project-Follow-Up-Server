using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class DetailsViewModel
    {
        public MatrixViewModel MatrixVM { get; set; }
        public ProjectEntity Project { get; set; }
        public ProjectTimelineViewModel Timeline { get; set; }
    }
}