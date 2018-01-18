namespace FluffySpoon.Automation.Web.Fluent.Upload
{
	public interface IUploadMethodChainNode : IBaseMethodChainNode
	{
		IUploadMethodChainNode this[string filePath] { get; }
	}
}
