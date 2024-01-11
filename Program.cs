// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.VisualBasic;
using Model;
using Models;

Console.WriteLine("Hello, World!");

#region [Input]
string inputBills = @".\inputData\bills.csv";
string inputLegislator = @".\inputData\legislators.csv";
string inputVote = @".\inputData\votes.csv";
string inputVoteResult = @".\inputData\vote_results.csv";

#endregion

#region [OutPut]
string outPutFolder = @".\out\";
string fileNameOne = "legislators-support-oppose-count.csv";
string fileNameTwo = "bills.csv";

DirectoryInfo di = Directory.CreateDirectory(outPutFolder);
Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(outPutFolder));

var fileOne = string.Concat(outPutFolder,fileNameOne);
var fileTwo = string.Concat(outPutFolder,fileNameTwo);

#endregion

//config to headers ignore case
//https://joshclose.github.io/CsvHelper/getting-started/#reading-a-csv-file
var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    PrepareHeaderForMatch = args => args.Header.ToLower(),
};

List<Bill> billsList = new();
List<Vote> votesList = new();
List<Person> personsList = new();
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

var billsSupported = votesResultList.GroupBy(x => new { x.LegislatorId })
    .Select(b => new AnswerBillsLegislator
{
    Person = personsList.Where(c => c.Id == b.Key.LegislatorId).FirstOrDefault(),
    VotesOpposed = b.Count(c => c.VoteType == 2),
    VotesSupported = b.Count(c => c.VoteType == 1)
}).ToList();

var groupedVotes = votesResultList.GroupBy(y => y.VoteId)
    .Select(group => new
    {
        VoteId = group.Key,
        TotalSupport = group.Count(y => y.VoteType == 1),
        TotalOppose = group.Count(y => y.VoteType == 2)
    })
    .ToList();

var billPrimarySponsor = billsList.Select(b => new{
    Bill_Id = b.Id,
    PrimarySponsor = personsList.Where(c => c.Id == b.PrimarySponsor).FirstOrDefault()
}).ToList();

List<AnswerSummaryLegislator> summaryLegislatorList = new();

foreach(var item in groupedVotes){

    Vote? matchingVote = votesList.FirstOrDefault(x => x.Id == item.VoteId);
    Bill? matchingBill = billsList.FirstOrDefault(x => x.Id == matchingVote.Bill_Id);
    Person? matchingPerson = personsList.Where(c => c.Id == matchingBill.PrimarySponsor).FirstOrDefault();

    string? primarySponsor = matchingPerson == null ? "Not found" : matchingPerson.Name;

    summaryLegislatorList.Add(new AnswerSummaryLegislator{
        Bill_Id = item.VoteId,
        BillName = matchingBill.Title,
        TotalOppose = item.TotalOppose,
        TotalSupport = item.TotalSupport,
        PrimarySponsor = primarySponsor
    });
}


using (var writer = new StreamWriter(fileOne))
using (var csv = new CsvWriter(writer, config))
{
    csv.WriteRecords(billsSupported);
}

using (var writer = new StreamWriter(fileTwo))
using (var csv = new CsvWriter(writer, config))
{
    csv.WriteRecords(summaryLegislatorList);
}

System.Console.WriteLine("Program ended.");