using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Bots.Dialogs
{
  public class InteruptOptionDialog : ComponentDialog
  {
    private readonly IInteruptOptionFactory _interuptOptionFactory;
    private readonly IInteruptOptionService _interuptOptionService;
    public InteruptOptionDialog(string dialogId, IInteruptOptionFactory interuptOptionFactory,
      IInteruptOptionService interuptOptionService) 
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
        if (interuptOption == InteruptOption.Invalid)
        {
          //TODO Log
          await innerDc.Context.SendActivityAsync("");
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
