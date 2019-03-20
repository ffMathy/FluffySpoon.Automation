using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Dom
{
    class JavaScriptScope : IJavaScriptScope
    {
        private readonly IWebAutomationFrameworkInstance _automationFrameworkInstance;
        private readonly IJavaScriptTunnel _domTunnel;

        private readonly HashSet<string> _variables;

        public JavaScriptScope(
            IWebAutomationFrameworkInstance automationFrameworkInstance,
            IJavaScriptTunnel domTunnel)
        {
            _automationFrameworkInstance = automationFrameworkInstance;
            _domTunnel = domTunnel;

            _variables = new HashSet<string>();
        }

        public async Task<string> CreateNewVariableAsync(string expression)
        {
            var key = "fluffy-spoon-" + Guid.NewGuid().ToString();
            var accessor = GetAccessorFromKey(key);
            await _automationFrameworkInstance.EvaluateJavaScriptAsync($"{accessor} = {expression}");

            _variables.Add(key);
            return accessor;
        }

        private static string GetAccessorFromKey(string key)
        {
            return $"window['{key}']";
        }

        public async Task DeleteAllVariablesAsync()
        {
            foreach (var key in _variables)
                await _automationFrameworkInstance.EvaluateJavaScriptAsync($"delete {GetAccessorFromKey(key)}");
        }
    }
}
