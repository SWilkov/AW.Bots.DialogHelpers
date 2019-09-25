using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Interfaces;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace AW.Bots.DialogHelpers.Services
{
  public class CancelInteruptOption : IInteruptOptionService
  {
    private const string CancelMsgText = "Cancelling chat...";
    public async Task<DialogTurnResult> Handle(DialogContext context, InteruptOption options,
      CancellationToken cancellationToken)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (options != InteruptOption.Help)
        throw new ArgumentException($"Invalid interupt option {options} for Cancel Action");

      var cancelMessage = MessageFactory.Text(CancelMsgText, CancelMsgText, InputHints.IgnoringInput);
      await context.Context.SendActivityAsync(cancelMessage, cancellationToken);
      return await context.CancelAllDialogsAsync(cancellationToken);
    }
  }
}
