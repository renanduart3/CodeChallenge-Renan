// See https://aka.ms/new-console-template for more information
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Models;

Console.WriteLine("Hello, World!");

#region [Input]
string inputBills = @".\bills.csv";
string inputLegislator = @".\legislator.csv";
string inputVote = @".\vote.csv";
string inputVoteResult = @".\vote_results.csv";

#endregion

#region [OutPut]
string outPutOne = @".\out\legislators-support-oppose-count.csv";
string outPutTwo = @".\out\bills.csv";

List<string> legislatorsSupportResult = new();
List<string> billsResult = new();
#endregion

//config to headers ignore case
//https://joshclose.github.io/CsvHelper/getting-started/#reading-a-csv-file
var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    PrepareHeaderForMatch = args => args.Header.ToLower(),
};

using (var reader = new StreamReader(inputBills))
using (var csv = new CsvReader(reader, config))
{
    var records = csv.GetRecords<Bill>();

    foreach(var item in records)
    {
        Console.WriteLine(item.Title);
    }
}

using (var writer = new StreamWriter(outPutOne))
using (var csv = new CsvWriter(writer, config))
{
    csv.WriteRecords(legislatorsSupportResult);
}

using (var writer = new StreamWriter(outPutTwo))
using (var csv = new CsvWriter(writer, config))
{
    csv.WriteRecords(billsResult);
}