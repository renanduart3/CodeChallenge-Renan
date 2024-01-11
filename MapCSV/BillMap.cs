using CsvHelper.Configuration;
using Models;

public class BillMap: ClassMap<Bill>{
    public BillMap()
    {
        Map(m => m.Id).Index(0).Name("id");
        Map(m => m.Title).Index(1).Name("title");
        Map(m => m.PrimarySponsor).Index(2).Name("sponsor_id");
    }
}