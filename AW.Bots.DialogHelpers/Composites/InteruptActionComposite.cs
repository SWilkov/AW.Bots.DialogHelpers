using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Bots.DialogHelpers.Composites
{
  public class InteruptActionComposite : IInteruptAction
  {
    private readonly Dictionary<InteruptAction, IInteruptAction> _services;
    public InteruptActionComposite(Dictionary<InteruptAction, IInteruptAction> services)
    {
      _services = services;
    }

    public async Task<DialogTurnResult> Handle(DialogContext context, InteruptAction interuptOption,
      CancellationToken cancellationToken)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));

      return await _services[interuptOption].Handle(context, interuptOption, cancellationToken);
    }
  }
}
