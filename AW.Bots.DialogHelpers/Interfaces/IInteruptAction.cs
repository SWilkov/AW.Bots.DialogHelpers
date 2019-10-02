using AW.Bots.DialogHelpers.Enums;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Bots.DialogHelpers.Interfaces
{
  public interface IInteruptAction
  {    
    Task<DialogTurnResult> Handle(DialogContext context, InteruptAction options,
      CancellationToken cancellationToken);
  }
}
