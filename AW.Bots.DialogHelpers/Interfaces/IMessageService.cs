using AW.Bots.DialogHelpers.Models;

namespace AW.Bots.DialogHelpers.Interfaces
{
  public interface IMessageService
  {
    Message Get(string sectionName, string action);
  }
}