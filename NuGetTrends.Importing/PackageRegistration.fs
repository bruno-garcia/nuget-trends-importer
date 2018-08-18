namespace NuGetTrends.Importing

//type PackageRegistration() =
//    member val Id = 0 with get,set
//    member val Name = None with get,set

type [<CLIMutable>] PackageRegistration =
  { Id: int
    Name: string
    Description: string
  }