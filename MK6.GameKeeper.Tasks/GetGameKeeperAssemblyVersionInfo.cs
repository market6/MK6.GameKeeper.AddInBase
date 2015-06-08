using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;

namespace MK6.GameKeeper.Tasks
{
    public class GetGameKeeperAssemblyVersionInfo : AbstractTask
    {
        [Required]
        public ITaskItem[] AssemblyFiles { get; set; }

        [Output]
        public string Version { get; set; }

        public override bool Execute()
        {
            if (AssemblyFiles.Length <= 0)
            {
                return false;
            }

            Version = GetVersionInfoFromAssemblyFiles(AssemblyFiles).FirstOrDefault();

            return true;
        }

        private IEnumerable<string> GetVersionInfoFromAssemblyFiles(IEnumerable<ITaskItem> assemblies)
        {
            foreach (ITaskItem assembly in assemblies)
            {
                LogMessage(String.Format("Get version info from assembly: {0}", assembly));
                var result = CreateTaskItemFromAssemblyFile(assembly.ItemSpec);
                LogMessage(String.Format("Found version number: {0}", result));

                yield return result;
            }
        }

        private static string CreateTaskItemFromAssemblyFile(string path)
        {
            var assemblyInfoFile = File.ReadAllText(path);

            var version = Regex.Match(assemblyInfoFile, @"(?<=\[assembly:\s?AssemblyVersion\("")[\d\.]*?(?="")");
            if (version.Success)
            {
                return version.Value;
            }

            return String.Empty;
        }
    }
}