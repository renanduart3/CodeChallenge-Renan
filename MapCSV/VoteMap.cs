using CsvHelper.Configuration;
using Models;

public class VoteMap: ClassMap<Vote>{
    public VoteMap()
    {
        Map(m => m.Id).Index(0).Name("id");
        Map(m => m.Bill_Id).Index(1).Name("bill_id");
    }
}