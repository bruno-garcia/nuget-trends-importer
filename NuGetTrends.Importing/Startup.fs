namespace NuGetTrends.Importing

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Hangfire;
open Hangfire.MemoryStorage;
open NuGetTrends.Importing.Hangfire

type Startup() =

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddHangfire(fun c -> c.UseStorage (new MemoryStorage()) |> ignore) |> ignore
        services.AddSingleton<NuGetImporter>() |> ignore
        ()

    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if env.IsDevelopment() then 
            app.UseDeveloperExceptionPage() |> ignore
            
        app.UseHangfireDashboard() |> ignore
        app.UseHangfireServer() |> ignore
        
        ScheduleJobs app |> ignore
