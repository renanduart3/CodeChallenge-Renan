using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model;
public class AnswerSummaryLegislator
{
    public int Bill_Id {get;set;}
    public string? BillName {get;set;}
    public int TotalSupport { get; set; }
    public int TotalOppose{ get; set; }
    public string? PrimarySponsor { get; set; }
}