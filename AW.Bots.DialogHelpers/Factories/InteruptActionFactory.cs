using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Interfaces;
using System;

namespace AW.Bots.DialogHelpers.Factories
{
  public class InteruptActionFactory : IInteruptActionFactory
  {
    public InteruptAction Get(string option)
    {
      if (string.IsNullOrEmpty(option))
        throw new ArgumentNullException(nameof(option));

      switch (option.ToLower())
      {
        case "help":
        case "h":
        case "?":
          return InteruptAction.Help;

        case "cancel":
        case "quit":
        case "q":
          return InteruptAction.Cancel;

        default:
          return InteruptAction.Continue;
      }
    }
  }
}
