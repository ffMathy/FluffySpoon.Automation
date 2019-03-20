using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web
{
    public interface IAwaitable
    {
        TaskAwaiter GetAwaiter();

        Task AsTask();
    }

    public interface IAwaitable<TResult>
    {
        TaskAwaiter<TResult> GetAwaiter();

        Task<TResult> AsTask();
    }
}