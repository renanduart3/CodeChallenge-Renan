using CsvHelper.Configuration;
using Models;

public class PersonMap: ClassMap<Person>{
    public PersonMap()
    {
        Map(m => m.Id).Index(0).Name("id");
        Map(m => m.Name).Index(1).Name("name");
    }
}