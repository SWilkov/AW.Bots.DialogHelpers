using AW.Bots.DialogHelpers.Enums;

namespace AW.Bots.DialogHelpers.Interfaces
{
  public interface IInteruptActionFactory
  {
    InteruptAction Get(string option);
  }
}