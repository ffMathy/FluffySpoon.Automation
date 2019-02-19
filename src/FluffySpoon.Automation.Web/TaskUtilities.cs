using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web
{
	internal class TaskUtilities
	{
		public static TaskContinuationOptions ContinuationOptions =>
			TaskContinuationOptions.AttachedToParent |
			TaskContinuationOptions.ExecuteSynchronously;
	}
}
