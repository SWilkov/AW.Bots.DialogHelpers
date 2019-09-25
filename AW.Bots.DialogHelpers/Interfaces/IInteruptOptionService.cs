using AW.Bots.DialogHelpers.Enums;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Bots.DialogHelpers.Interfaces
{
  public interface IInteruptOptionService
  {
    Task<DialogTurnResult> Handle(DialogContext context, InteruptOption options,
      CancellationToken cancellationToken);
  }
}
