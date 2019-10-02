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
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;

namespace AW.Bots.DialogHelpers.Actions
{
  public class CancelInteruptAction : IInteruptAction
  {
    private string CancelMsgText = "Cancelling chat...";
    private readonly IMessageService _messageService;
    private readonly ITurnContextWrapper _turnContextWrapper;
    private readonly IDialogContextWrapper _dialogContextWrapper;
    public CancelInteruptAction(IMessageService messageService,      
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
      if (options != InteruptAction.Cancel)
        throw new ArgumentException($"Invalid interupt option {options} for Cancel Action");

      var sectionName = Environment.GetEnvironmentVariable("DIALOG_BOT_MESSAGES");
      var msg = _messageService.Get(sectionName, options.ToString().ToLower());
      this.CancelMsgText = msg != null ? msg.Text : this.CancelMsgText;

      var cancelMessage = MessageFactory.Text(CancelMsgText, CancelMsgText, InputHints.IgnoringInput);
      await _turnContextWrapper.SendActivityAsync(innerDc.Context, cancelMessage, cancellationToken);
      return await _dialogContextWrapper.CancelAllDialogsAsync(innerDc, cancellationToken);
    }    
  }
}
