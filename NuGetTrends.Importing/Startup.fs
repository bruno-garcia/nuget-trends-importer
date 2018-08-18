namespace NuGetTrends.Importing

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Hangfire;
open Hangfire.MemoryStorage;
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration

type Startup(configuration: IConfiguration) =

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddHangfire(fun c -> c.UseStorage (new MemoryStorage()) |> ignore) |> ignore
        services.AddSingleton<NuGetImporter>() |> ignore

        let connectionString = configuration.GetConnectionString("NuGetTrends")
        let pg = services.AddEntityFrameworkNpgsql()
        pg.AddDbContext<NuGetTrendsContext>(fun o -> o.UseNpgsql (connectionString) |> ignore) |> ignore
        ()

    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHangfireDashboard() |> ignore
        app.UseHangfireServer() |> ignore

        ScheduleJobs app |> ignore
