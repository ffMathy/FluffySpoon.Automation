using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web
{
    public static class TaskUtilities
    {
        public static TaskContinuationOptions ContinuationOptions =>
            TaskContinuationOptions.AttachedToParent |
            TaskContinuationOptions.ExecuteSynchronously;
    }
}
