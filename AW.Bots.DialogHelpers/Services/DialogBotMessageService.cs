using AW.Bots.DialogHelpers.Interfaces;
using AW.Bots.DialogHelpers.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AW.Bots.DialogHelpers.Services
{
  public class DialogBotMessageService : 
    IConfigurationHelperService<DialogBotMessages>
  {
    private readonly IConfiguration _configuration;
    public DialogBotMessageService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public DialogBotMessages Get(string name)
    {
      if (string.IsNullOrEmpty(name))
        return default;

      var msgs = new DialogBotMessages();

      _configuration.GetSection(name.ToLower()).Bind(msgs);

      return msgs;
    }
  }
}
