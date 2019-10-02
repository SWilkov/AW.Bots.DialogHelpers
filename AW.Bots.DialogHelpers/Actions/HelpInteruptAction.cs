using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Interfaces;
using AW.Utils.Interfaces;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

namespace AW.Bots.DialogHelpers.Actions
{
  public class HelpInteruptAction : IInteruptAction
  {
    private string HelpMsgText = "HELP text goes here...";
    private readonly IMessageService _messageService;
    private readonly ITurnContextWrapper _turnContextWrapper;
    private readonly IDialogContextWrapper _dialogContextWrapper;
    public HelpInteruptAction(IMessageService messageService,      
      ITurnContextWrapper turnContextWrapper,
      IDialogContextWrapper dialogContextWrapper)
    {
      _messageService = messageService;
      _turnContextWrapper = turnContextWrapper;
      _dialogContextWrapper = dialogContextWrapper;
    }

    public async Task<DialogTurnResult> Handle(DialogContext innerDc, InteruptAction options, 
      CancellationToken cancellationToken)
    {
      if (innerDc == null)
        throw new ArgumentNullException(nameof(innerDc));
      if (options != InteruptAction.Help)
        throw new ArgumentException($"Invalid interupt option {options} for Help Action");

      var sectionName = Environment.GetEnvironmentVariable("DIALOG_BOT_MESSAGES");
      var msg = _messageService.Get(sectionName, options.ToString().ToLower());
      this.HelpMsgText = msg != null ? msg.Text : this.HelpMsgText;
      
      var helpMessage = MessageFactory.Text(HelpMsgText, HelpMsgText, InputHints.ExpectingInput);
      var response = await _turnContextWrapper.SendActivityAsync(innerDc.Context, helpMessage, cancellationToken);
      
      return new DialogTurnResult(DialogTurnStatus.Waiting);
    }
  }
}
