using AW.Bots.DialogHelpers.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Bots.DialogHelpers.Wrappers
{
  public class DialogContextWrapper : IDialogContextWrapper
  {
    public async Task<DialogTurnResult> CancelAllDialogsAsync(DialogContext dialogContext,
      CancellationToken cancellationToken) =>
      await dialogContext.CancelAllDialogsAsync(cancellationToken);

    public async Task<DialogTurnResult> PromptAsync(DialogContext dialogContext,
      string dialogId, PromptOptions options,
      CancellationToken cancellationToken) =>
      await dialogContext.PromptAsync(dialogId, options);
  }
}
