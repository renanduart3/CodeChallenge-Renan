# CodeChallenge-Renan



## Write up
1. Discuss your solution’s time complexity. What tradeoffs did you make?
   > I did the most part of serches and index itens trying to follow O(n) and made the code linear without breaking up in methods to be more excplicity, but in final foreach i have some questions inside the filters with linq inside a foreach, but in that moment that was my solution. I'm using a lib that i've met in this challegen called CsvHelper to make the manipulations for the files.
2. How would you change your solution to account forfuture columns that might be requested, such as “Bill Voted On Date” or“Co-Sponsors”?
   >For that solution (and i will not change after reading this, to be more assertive for you), i would change the models to identify the new fields, the query to filter grouped votes and the loop to insert the new colum in the result.
3. How would you change your solution if instead ofreceiving CSVs of data, you were given a
list of legislators or bills that you should generate a CSV for?
    > In my solution it will be more easy, because i'm already filtering data through a list that was trasnformed.
4. How long did you spend working on the assignment?
> according to table above, 5 hours, because in the first try i spend much time trying to make the filter entirely with linq expression, but, i'm not expert yet in linq expressions and i got some problems with the projections that i was trying to do.

| Data       | Start hour        | End hour        | Woking Our          |
|------------|-------------------|-----------------|---------------------|
| 2024-01-10 | 10:00 PM          | 23:00 PM        | 1h                  |
| 2024-01-11 | 10:00 AM          | 12:45 PM        | 2h 45min            |
| 2024-01-11 | 15:00 PM          | 16:50 PM        | 1h 50min            |

## How to run
Install .net SDK 8

run the command in prompt in windows in folder project
> dotnet run

Or i made an executable in folder execution/ just double click in ProgramExecution.exe