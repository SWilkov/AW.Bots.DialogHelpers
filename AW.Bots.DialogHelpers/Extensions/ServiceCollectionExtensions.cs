using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Factories;
using AW.Bots.DialogHelpers.Interfaces;
using AW.Bots.DialogHelpers.Models;
using AW.Bots.DialogHelpers.Services;
using AW.Bots.DialogHelpers.Wrappers;
using AW.Utils.Env.Extensions;
using AW.Utils.Env.Interfaces;
using AW.Utils.Env.Services;
using AW.Utils.Env.Wrappers;
using AW.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AW.Bots.DialogHelpers.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddBotDialogHelpers(this IServiceCollection services)
    {
      #region factories
      services.AddTransient<IInteruptActionFactory, InteruptActionFactory>();
      #endregion

      #region services
      services.AddTransient<IMessageService, MessageService>();
      services.AddTransient<IConfigurationHelperService<DialogBotMessages>, DialogBotMessageService>();
      #endregion

      #region wrappers
      services.AddTransient<ITurnContextWrapper, TurnContextWrapper>();
      services.AddTransient<IDialogContextWrapper, DialogContextWrapper>();
      #endregion

      #region actions
      services.AddTransient<IInteruptAction>(sp =>
      {
        var dict = new Dictionary<InteruptAction, IInteruptAction>
        {
          {
            InteruptAction.Cancel,
            new Actions.CancelInteruptAction(sp.GetRequiredService<IMessageService>(),                                             
                                             sp.GetRequiredService<ITurnContextWrapper>(),
                                             sp.GetRequiredService<IDialogContextWrapper>())
          },
          {
            InteruptAction.Help,
            new Actions.HelpInteruptAction(sp.GetRequiredService<IMessageService>(),
                                           sp.GetRequiredService<ITurnContextWrapper>(),
                                           sp.GetRequiredService<IDialogContextWrapper>())
          }
        };

        return new Composites.InteruptActionComposite(dict);
      });
      #endregion
    }
  }
}
