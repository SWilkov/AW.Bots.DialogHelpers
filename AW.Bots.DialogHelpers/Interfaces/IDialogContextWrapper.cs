using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace AW.Bots.DialogHelpers.Interfaces
{
  public interface IDialogContextWrapper
  {
    Task<DialogTurnResult> CancelAllDialogsAsync(DialogContext dialogContext, 
      CancellationToken cancellationToken);
    Task<DialogTurnResult> PromptAsync(DialogContext dialogContext,
      string dialogId, PromptOptions options,
      CancellationToken cancellationToken);
  }
}