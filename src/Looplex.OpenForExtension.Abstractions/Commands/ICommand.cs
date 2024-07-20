using System.Threading;
using System.Threading.Tasks;
using Looplex.OpenForExtension.Abstractions.Contexts;

namespace Looplex.OpenForExtension.Abstractions.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Task ExecuteAsync(IContext context, CancellationToken cancellationToken);
    }
}