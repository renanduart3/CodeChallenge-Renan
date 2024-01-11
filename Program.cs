// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using Model;
using Models;

Console.WriteLine("Hello, World!");

#region [Input]
string inputBills = @".\bills.csv";
string inputLegislator = @".\legislators.csv";
string inputVote = @".\votes.csv";
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

List<Bill> billsList = new();
List<Vote> votesList = new();
List<Person>personsList = new();
List<VoteResult> votesResultList = new();

using (var reader = new StreamReader(inputBills))
using (var csv = new CsvReader(reader, config))
{
    csv.Context.RegisterClassMap<BillMap>();
    var records = csv.GetRecords<Bill>();

    foreach (var item in records)
    {
        billsList.Add(item);
    }
}

using (var reader = new StreamReader(inputLegislator))
using (var csv = new CsvReader(reader, config))
{
    csv.Context.RegisterClassMap<PersonMap>();
    var records = csv.GetRecords<Person>();

    foreach (var item in records)
    {
        personsList.Add(item);
    }
}

using (var reader = new StreamReader(inputVote))
using (var csv = new CsvReader(reader, config))
{
    csv.Context.RegisterClassMap<VoteMap>();
    var records = csv.GetRecords<Vote>();

    foreach (var item in records)
    {
        votesList.Add(item);
    }
}

using (var reader = new StreamReader(inputVoteResult))
using (var csv = new CsvReader(reader, config))
{
    csv.Context.RegisterClassMap<VoteResultMap>();
    var records = csv.GetRecords<VoteResult>();

    foreach (var item in records)
    {
        votesResultList.Add(item);
    }
}

var billsSupported = votesResultList.GroupBy( x => new {x.LegislatorId })
.Select(b => new AnswerBillsLegislator{
    Person = personsList.Where(c => c.Id == b.Key.LegislatorId).FirstOrDefault(),
    VotesOpposed = b.Count(c => c.VoteType == 2),
    VotesSupported = b.Count(c => c.VoteType == 1)
});

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