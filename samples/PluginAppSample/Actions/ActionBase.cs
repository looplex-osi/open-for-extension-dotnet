using Looplex.OpenForExtension.Commands;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.ExtensionMethods;

namespace PluginAppSample.Actions
{
    internal abstract class ActionBase
    {
        public abstract void ExecuteDefaultAction(IPluginContext context);

        public void Execute(IPluginContext context)
        {
            if (context == default)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Plugins.Execute<IHandleInput>(context);

            context.Plugins.Execute<IValidateInput>(context);

            context.Plugins.Execute<IDefineActors>(context);

            context.Plugins.Execute<IBind>(context);

            context.Plugins.Execute<IBeforeAction>(context);

            if (context is IDefaultContext defaultContext
                && !defaultContext.SkipDefaultAction)
            {
                ExecuteDefaultAction(context);
            }

            context.Plugins.Execute<IAfterAction>(context);

            context.Plugins.Execute<IReleaseUnmanagedResources>(context);
        }
    }
}
