module NuGetTrends.Importing.Hangfire

open System
open System.Threading.Tasks
open System.Linq.Expressions
open Hangfire
open Hangfire.Common
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection;
open Microsoft.FSharp.Linq.RuntimeHelpers

let private AddOrUpdate<'Job>
    (jobManager: IRecurringJobManager)
    (recurringJobId: string)
    (jobExpression: Expression<Func<'Job,Task>>)
    (cronExpression: string) =
        let job = Job.FromExpression jobExpression
        jobManager.AddOrUpdate (recurringJobId, job, cronExpression) |> ignore
        
let ScheduleJobs (app: IApplicationBuilder) =
    let jobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>()
    let expr = 
      <@ System.Func<_, _>(fun (j: NuGetImporter) -> j.Import()) @>
      |> LeafExpressionConverter.QuotationToExpression
      |> unbox<Expression<Func<NuGetImporter, Task>>>
    AddOrUpdate jobManager "NuGetImporter" expr (Cron.Daily(10, 30)) |> ignore

