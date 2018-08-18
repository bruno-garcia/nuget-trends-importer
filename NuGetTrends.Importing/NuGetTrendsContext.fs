namespace NuGetTrends.Importing

open Microsoft.EntityFrameworkCore

type NuGetTrendsContext =
    inherit DbContext

    new() = { inherit DbContext }
    new(options: DbContextOptions<NuGetTrendsContext>) = { inherit DbContext(options) }

    [<DefaultValue>]
    val mutable packageRegistrations:DbSet<PackageRegistration>
    member x.PackageRegistrations
        with get() = x.packageRegistrations
        and set v = x.packageRegistrations <- v
