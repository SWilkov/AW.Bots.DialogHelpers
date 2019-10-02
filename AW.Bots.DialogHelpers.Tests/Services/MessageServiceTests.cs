using AW.Bots.DialogHelpers.Interfaces;
using AW.Bots.DialogHelpers.Models;
using AW.Bots.DialogHelpers.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AW.Bots.DialogHelpers.Tests.Services
{
  public class MessageServiceTests
  {
    private IMessageService _service;
    private Mock<IConfigurationHelperService<DialogBotMessages>> _mConfigHelper;
    public MessageServiceTests()
    {
      _mConfigHelper = new Mock<IConfigurationHelperService<DialogBotMessages>>();
      _service = new MessageService(_mConfigHelper.Object);
    }

    [Fact]
    public void section_name_null_throw_exception()
    {
      Assert.Throws<ArgumentNullException>(() =>
       _service.Get(null, "cancel"));
    }

    [Fact]
    public void action_is_empty_throw_exception()
    {
      Assert.Throws<ArgumentNullException>(() =>
       _service.Get("dialogs", ""));
    }

    [Fact]
    public void dialog_bot_messages_null_return_null()
    {
      _mConfigHelper.Setup(x => x.Get(It.IsAny<string>()))
        .Returns((string name) => null);

      var msg = _service.Get("dialogs", "cancel");

      Assert.Null(msg);
    }

    [Fact]
    public void dialog_bot_messages_null_messages_return_null()
    {
      _mConfigHelper.Setup(x => x.Get(It.IsAny<string>()))
        .Returns((string name) => new DialogBotMessages
        {
          Name = "Dialog Bot Messages",
          Messages = null
        });

      var msg = _service.Get("dialogs", "cancel");

      Assert.Null(msg);
    }

    [Fact]
    public void dialog_bot_messages_empty_messages_return_null()
    {
      _mConfigHelper.Setup(x => x.Get(It.IsAny<string>()))
        .Returns((string name) => new DialogBotMessages
        {
          Name = "Dialog Bot Messages",
          Messages = new List<Message>()
        });

      var msg = _service.Get("dialogs", "cancel");

      Assert.Null(msg);
    }

    [Fact]
    public void no_message_with_passed_action_return_null()
    {
      _mConfigHelper.Setup(x => x.Get(It.IsAny<string>()))
        .Returns((string name) => new DialogBotMessages
        {
          Name = "Dialog Bot Messages",
          Messages = new List<Message>
          {
            new Message { Action = "help", Text = "HELP ME" }
          }
        });

      var msg = _service.Get("dialogs", "cancel");

      Assert.Null(msg);
    }

    [Fact]
    public void cancel_action_in_uppercase_found_correct_message()
    {
      _mConfigHelper.Setup(x => x.Get(It.IsAny<string>()))
        .Returns((string name) => new DialogBotMessages
        {
          Name = "Dialog Bot Messages",
          Messages = new List<Message>
          {
            new Message { Action = "help", Text = "HELP ME" },
            new Message { Action = "cancel", Text = "Cancelling the Dialog" }
          }
        });

      var msg = _service.Get("dialogs", "CANCEL");

      Assert.NotNull(msg);
      Assert.IsType<Message>(msg);
      Assert.Equal("cancel", msg.Action);
    }
  }
}
