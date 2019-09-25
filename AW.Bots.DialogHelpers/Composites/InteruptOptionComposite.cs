using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpersInterfaces;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Bots.DialogHelpers.Composites
{
  public class InteruptOptionComposite : IInteruptOptionService
  {
    private readonly Dictionary<InteruptOption, IInteruptOptionService> _services;
    public InteruptOptionComposite(Dictionary<InteruptOption, IInteruptOptionService> services)
    {
      _services = services;
    }

    public async Task<DialogTurnResult> Handle(DialogContext context, InteruptOption interuptOption,
      CancellationToken cancellationToken)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));

      return await _services[interuptOption].Handle(context, interuptOption, cancellationToken);
    }
  }
}
