using System.Runtime.CompilerServices;

namespace FluffySpoon.Automation.Web
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