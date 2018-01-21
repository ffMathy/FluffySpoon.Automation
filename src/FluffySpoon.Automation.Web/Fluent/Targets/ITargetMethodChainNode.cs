namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	public interface ITargetMethodChainNode<out TCurrentMethodChainNode, out TNextMethodChainNode> : IBaseMethodChainNode
		where TNextMethodChainNode : IBaseMethodChainNode
		where TCurrentMethodChainNode : IBaseMethodChainNode
    {
    }
}
