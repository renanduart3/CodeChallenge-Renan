using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models;
public class VoteResult
{
    public int Id { get; set; }
    public int LegislatorId { get; set; }
    public int VoteId { get; set; }
    public int VoteType { get; set; }

}