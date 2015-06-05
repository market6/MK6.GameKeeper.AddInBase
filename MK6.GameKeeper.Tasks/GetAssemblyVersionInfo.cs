using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MK6.GameKeeper.Tasks
{
    public class GetAssemblyVersionInfo : AbstractTask
    {
        [Required]
        public ITaskItem[] AssemblyFiles { get; set; }

        [Output]
        public ITaskItem[] AssemblyVersionInfo { get; set; }

        public override bool Execute()
        {
            if (AssemblyFiles.Length <= 0)
            {
                return false;
            }

            AssemblyVersionInfo = GetVersionInfoFromAssemblyFiles(AssemblyFiles).ToArray();
            return true;
        }

        private IEnumerable<ITaskItem> GetVersionInfoFromAssemblyFiles(IEnumerable<ITaskItem> assemblies)
        {
            foreach (ITaskItem assembly in assemblies)
            {
                LogMessage(String.Format("Get version info from assembly: {0}", assembly));
                yield return CreateTaskItemFromAssemblyFile(assembly.ItemSpec);
            }
        }

        private static TaskItem CreateTaskItemFromAssemblyFile(string path)
        {
            var info = FileVersionInfo.GetVersionInfo(path);
            var assemblyVersion = AssemblyName.GetAssemblyName(info.FileName).Version;

            if (info.FileVersion != info.ProductVersion)
            {
                return new TaskItem(info.FileName, new Hashtable()
                {
                    { "Version", info.ProductVersion }
                });
            }

            return new TaskItem(info.FileName, new Hashtable()
            {
                { "Version", assemblyVersion.ToString() }
            });
        }
    }
}