using AW.Bots.DialogHelpers.Interfaces;
using AW.Bots.DialogHelpers.Models;
using System;
using System.Linq;

namespace AW.Bots.DialogHelpers.Services
{
  public class MessageService : IMessageService
  {
    private readonly IConfigurationHelperService<DialogBotMessages> _configurationHelperService;
    public MessageService(IConfigurationHelperService<DialogBotMessages> configurationHelperService)
    {
      _configurationHelperService = configurationHelperService;
    }

    public Message Get(string sectionName, string action)
    {
      if (string.IsNullOrEmpty(sectionName))
        throw new ArgumentNullException(nameof(sectionName));
      if (string.IsNullOrEmpty(action))
        throw new ArgumentNullException(nameof(action));

      var msgs = _configurationHelperService.Get(sectionName);
      if (msgs == null || msgs.Messages == null || !msgs.Messages.Any())
        return null;

      return msgs.Messages.FirstOrDefault(x => x.Action.ToLower() == action.ToLower());
    }
  }
}
