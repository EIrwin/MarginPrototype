using System;
using System.Collections.Generic;

namespace MarginPrototype.ViewModels
{
    public class ReportViewModel
    {
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public List<PageViewModel> Pages { get; set; }

        public ReportViewModel()
        {
            Name = "My Report - " + DateTime.Now.ToString();
            Created = DateTime.Now;
            Pages = new List<PageViewModel>();
        }
    }
}