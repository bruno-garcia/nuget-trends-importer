namespace NuGetTrends.Importing

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting

module Program =
    let exitCode = 0

    let CreateWebHostBuilder args =
        WebHost
            .CreateDefaultBuilder(args)
            .UseSentry()
            .UseStartup<Startup>();

    [<EntryPoint>]
    let main args =
        CreateWebHostBuilder(args).Build().Run()

        exitCode
