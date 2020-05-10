using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace CoreBot1.Dialogs
{
  public class InteruptActionDialog : ComponentDialog
  {
    private readonly IInteruptActionFactory _interuptOptionFactory;
    private readonly IInteruptAction _interuptOptionService;
    public InteruptActionDialog(string dialogId, IInteruptActionFactory interuptOptionFactory,
      IInteruptAction interuptOptionService) 
      : base(dialogId)
    {
      _interuptOptionFactory = interuptOptionFactory;
      _interuptOptionService = interuptOptionService;
    }
    
    protected override async Task<DialogTurnResult> OnContinueDialogAsync(DialogContext innerDc, 
      CancellationToken cancellationToken = default)
    {
      if (innerDc.Context.Activity.Type == ActivityTypes.Message)
      {
        var text = innerDc.Context.Activity.Text.ToLowerInvariant();
        var interuptOption = _interuptOptionFactory.Get(text);
        if (interuptOption != InteruptAction.Cancel || interuptOption != InteruptAction.Continue
          || interuptOption != InteruptAction.Help)
        {
          //TODO Log
          await innerDc.Context.SendActivityAsync("Invalid action");
          return null;
        }

        var dialogResult = await _interuptOptionService.Handle(innerDc, interuptOption, cancellationToken);
        if (dialogResult != null)
          return dialogResult;
      }

      return await base.OnContinueDialogAsync(innerDc, cancellationToken);
    }
  }
}
