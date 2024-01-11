using CsvHelper.Configuration;
using Models;

public class VoteResultMap: ClassMap<VoteResult>{
    public VoteResultMap()
    {
        Map(m => m.Id).Index(0).Name("id");
        Map(m => m.LegislatorId).Index(1).Name("legislator_id");
        Map(m => m.VoteId).Index(2).Name("vote_id");
        Map(m => m.VoteType).Index(3).Name("vote_type");
    }
}