using AW.Bots.DialogHelpers.Enums;
using AW.Bots.DialogHelpers.Interfaces;
using System;

namespace AW.Bots.DialogHelpers.Factories
{
  public class InteruptOptionFactory : IInteruptOptionFactory
  {
    public InteruptOption Get(string option)
    {
      if (string.IsNullOrEmpty(option))
        throw new ArgumentNullException(nameof(option));

      switch (option.ToLower())
      {
        case "help":
        case "h":
        case "?":
          return InteruptOption.Help;

        case "cancel":
        case "quit":
        case "q":
          return InteruptOption.Cancel;

        default:
          return InteruptOption.Invalid;
      }
    }
  }
}
