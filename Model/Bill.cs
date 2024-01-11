using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Bill
    {
        public int Id{get;set;}
        public string Title { get; set; } = string.Empty;
        public int PrimarySponsor { get; set; }
    }
}