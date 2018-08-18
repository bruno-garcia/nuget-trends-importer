namespace NuGetTrends.Importing

open System;
open System.Threading.Tasks
open Microsoft.Extensions.Hosting

type NuGetImporter() =
    member this.Import(): Task = 
        System.Console.WriteLine "importing"
        Task.CompletedTask
