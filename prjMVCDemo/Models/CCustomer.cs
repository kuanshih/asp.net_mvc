using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CCustomer
    {
        public int fId { get; set; }
        [DisplayName("品名")]
        public string fName { get; set; }
        public string fPhone { get; set; }
        public string fEmail { get; set; }
        public string fAddress { get; set; }
        public string fPassword { get; set; }
    }
}