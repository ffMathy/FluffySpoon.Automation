using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent
{
    public interface IMethodChainContext: IAwaitable
    {
        Task RunAllAsync();
        Task RunNextAsync();

        TMethodChainNode Enqueue<TMethodChainNode>(TMethodChainNode node) where TMethodChainNode : IBaseMethodChainNode;
    }
}