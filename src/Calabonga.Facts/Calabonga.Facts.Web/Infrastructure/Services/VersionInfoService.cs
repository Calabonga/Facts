namespace Calabonga.Facts.Web.Infrastructure.Services
{
    public class VersionInfoService : IVersionInfoService
    {
        public string Version => ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch;

        public string Branch => ThisAssembly.Git.Branch;

        public string Commit => ThisAssembly.Git.Commit;
    }
}