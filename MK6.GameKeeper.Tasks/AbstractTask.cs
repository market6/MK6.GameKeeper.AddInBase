using Microsoft.Build.Framework;

namespace MK6.GameKeeper.Tasks
{
    public abstract class AbstractTask : ITask
    {
        private const string LogSubCategory = "MK6.GameKeeper.AddIn";

        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }

        public abstract bool Execute();

        protected void LogError(string code, string message)
        {
            BuildEngine.LogErrorEvent(new BuildErrorEventArgs(LogSubCategory, code, null, 0, 0, 0, 0, message, LogSubCategory, LogSubCategory));
        }

        protected void LogWarning(string code, string message)
        {
            BuildEngine.LogWarningEvent(new BuildWarningEventArgs(LogSubCategory, code, null, 0, 0, 0, 0, message, LogSubCategory, LogSubCategory));
        }

        protected void LogMessage(string message, MessageImportance importance = MessageImportance.Normal)
        {
            BuildEngine.LogMessageEvent(new BuildMessageEventArgs(message, LogSubCategory, LogSubCategory, importance));
        }
    }
}
