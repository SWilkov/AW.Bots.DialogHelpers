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
  public class HelpInteruptOption : IInteruptOptionService
  {
    private const string HelpMsgText = "HELP ME";

    public async Task<DialogTurnResult> Handle(DialogContext context, InteruptOption options, 
      CancellationToken cancellationToken)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (options != InteruptOption.Help)
        throw new ArgumentException($"Invalid interupt option {options} for Help Action");

      var helpMessage = MessageFactory.Text(HelpMsgText, HelpMsgText, InputHints.ExpectingInput);
      var response = await context.Context.SendActivityAsync(helpMessage, cancellationToken);

      return new DialogTurnResult(DialogTurnStatus.Waiting);
    }
  }
}
