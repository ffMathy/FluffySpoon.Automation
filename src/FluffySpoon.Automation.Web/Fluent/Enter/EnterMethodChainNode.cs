using System.Collections.Generic;
using System.Linq;
using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Root;
using FluffySpoon.Automation.Web.Fluent.Targets;
using FluffySpoon.Automation.Web.Fluent.Targets.In;

namespace FluffySpoon.Automation.Web.Fluent.Enter
{
    class EnterMethodChainNode: BaseDomElementTargetsMethodChainNode<EnterMethodChainNode, EnterInTargetMethodChainNode>
    {
        internal string TextToEnter { get; }

        public EnterMethodChainNode(string text)
        {
            TextToEnter = text;
        }
	}
}