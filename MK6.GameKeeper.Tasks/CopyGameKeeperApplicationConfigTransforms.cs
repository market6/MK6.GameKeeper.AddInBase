using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;

namespace MK6.GameKeeper.Tasks
{
    public class CopyGameKeeperApplicationConfigTransforms : AbstractTask
    {
        [Required]
        public ITaskItem[] ConfigFiles { get; set; }

        [Required]
        public string AssemblyTargetName { get; set; }

        [Required]
        public string OutputDirectory { get; set; }
        
        public override bool Execute()
        {
            //Match filename against app.config pattern
            foreach (var file in ConfigFiles)
            {
                var targetFile = Regex.Replace(file.ItemSpec, @"app(?=\.[^\.]*\.config)", AssemblyTargetName,
                    RegexOptions.IgnoreCase);

                if (targetFile == file.ItemSpec) continue;

                var targetDirectory = GetFullPath(OutputDirectory);
                File.Copy(GetFullPath(file.ItemSpec), Path.Combine(targetDirectory, targetFile), true);
            }

            return true;
        }

        private string GetFullPath(string relativeOrAbsoluteFilePath)
        {
            if (!Path.IsPathRooted(relativeOrAbsoluteFilePath))
            {
                relativeOrAbsoluteFilePath = Path.Combine(Environment.CurrentDirectory, relativeOrAbsoluteFilePath);
            }
            relativeOrAbsoluteFilePath = Path.GetFullPath(relativeOrAbsoluteFilePath);
            return relativeOrAbsoluteFilePath;
        }
    }
}