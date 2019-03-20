using System;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
    public interface IJavaScriptScope
    {
        Task<string> CreateNewVariableAsync(string expression);

        Task DeleteAllVariablesAsync();
    }
}