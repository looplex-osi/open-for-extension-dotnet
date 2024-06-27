using Looplex.OpenForExtension.Context;

namespace PluginAppSample.Actions
{
    internal class DefaultAction : ActionBase
    {
        public override void ExecuteDefaultAction(IPluginContext context)
        {
            Console.WriteLine("Default Action");
        }
    }
}
