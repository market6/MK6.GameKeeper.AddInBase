using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;

namespace MK6.GameKeeper.Tasks
{
    public class UpdateGameKeeperAddInVersion : AbstractTask
    {
        [Required]
        public ITaskItem[] CodeFiles { get; set; }
        [Required]
        public string Version { get; set; }
        [Output]
        public ITaskItem[] UpdatedCodeFiles { get; set; }

        public override bool Execute()
        {
            if (CodeFiles.Length <= 0 || String.IsNullOrEmpty(Version))
            {
                return false;
            }

            UpdatedCodeFiles = UpdateAddInAttributeVersionProperty(CodeFiles, Version).ToArray();

            return true;
        }

        private IEnumerable<ITaskItem> UpdateAddInAttributeVersionProperty(IEnumerable<ITaskItem> codeFiles,
            string version)
        {
            foreach (var file in codeFiles)
            {
                var content = File.ReadAllText(file.ItemSpec);
                var modified = Regex.Replace(content,
                    @"(?<=\[AddIn\([^)]*Version\s?=\s?"").*?(?="")",
                    version);

                if (content == modified) continue;

                LogMessage(String.Format("Updating AddIn Version in: {0}", file.ItemSpec));

                File.WriteAllText(file.ItemSpec, modified);
                yield return file;
            }
        }
    }
}