using System.Runtime.CompilerServices;

namespace FluffySpoon.Automation.Web.Async
{
    public interface IAwaitable
    {
        TaskAwaiter GetAwaiter();
    }

    public interface IAwaitable<TResult>
    {
        TaskAwaiter<TResult> GetAwaiter();
    }
}