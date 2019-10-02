using AW.Bots.DialogHelpers.Actions;
using AW.Bots.DialogHelpers.Interfaces;
using AW.Utils.Interfaces;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Testing;
using Microsoft.Bot.Schema;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.Bot.Builder.Dialogs.Choices.Channel;

namespace AW.Bots.DialogHelpers.Tests.Actions
{
  public class CancelInteruptActionTests
  {
    private IInteruptAction _service;
    private Mock<IMessageService> _mMessageService;
    private Mock<ITurnContextWrapper> _mTurnContextWrapper;
    private Mock<IDialogContextWrapper> _mDialogContextWrapper;
    public CancelInteruptActionTests()
    {
      _mDialogContextWrapper = new Mock<IDialogContextWrapper>();
      _mMessageService = new Mock<IMessageService>();
      _mTurnContextWrapper = new Mock<ITurnContextWrapper>();

      _service = new CancelInteruptAction(_mMessageService.Object,
        _mTurnContextWrapper.Object,
        _mDialogContextWrapper.Object);
    }

    [Fact]
    public async Task dialog_context_is_null_throw_exception()
    {
      await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        await _service.Handle(null, Enums.InteruptAction.Cancel, new CancellationToken()));
    }

    [Fact]
    public async Task interupt_action_is_not_cancel_throw_exception()
    {
      var cs = new ConversationState(new MemoryStorage());
      var ds = cs.CreateProperty<DialogState>("some-state");
      var dialogSet = new DialogSet(ds);

      var mTurnContext = new Mock<ITurnContext>();
      var innerDc = new DialogContext(dialogSet, mTurnContext.Object, new DialogState());
      await Assert.ThrowsAsync<ArgumentException>(async () =>
        await _service.Handle(innerDc, Enums.InteruptAction.Help, new CancellationToken()));
    }

    [Fact]
    public async Task message_service_returns_null_so_keep_default_message()
    {
      var cs = new ConversationState(new MemoryStorage());
      var ds = cs.CreateProperty<DialogState>("some-state");
      var dialogSet = new DialogSet(ds);

      var mTurnContext = new Mock<ITurnContext>();

      _mMessageService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
        .Returns((string sn, string name) => null);
      _mTurnContextWrapper.Setup(x => x.SendActivityAsync(It.IsAny<ITurnContext>(),
        It.IsAny<Activity>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new ResourceResponse());
      _mDialogContextWrapper.Setup(x => x.CancelAllDialogsAsync(It.IsAny<DialogContext>(),
        It.IsAny<CancellationToken>()))
        .ReturnsAsync(new DialogTurnResult(DialogTurnStatus.Cancelled));

      var innerDc = new DialogContext(dialogSet, mTurnContext.Object, new DialogState());
      await Assert.ThrowsAsync<ArgumentException>(async () =>
        await _service.Handle(innerDc, Enums.InteruptAction.Help, new CancellationToken()));

      var result = await _service.Handle(innerDc, Enums.InteruptAction.Cancel, new CancellationToken());

      Assert.IsType<DialogTurnResult>(result);
      Assert.Equal(DialogTurnStatus.Cancelled, result.Status);
    }

    [Fact]
    public async Task message_service_returns_message_so_return_cancelled_dialog_turn_result()
    {
      var cs = new ConversationState(new MemoryStorage());
      var ds = cs.CreateProperty<DialogState>("some-state");
      var dialogSet = new DialogSet(ds);

      var mTurnContext = new Mock<ITurnContext>();

      _mMessageService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
        .Returns((string sn, string name) => new Models.Message { Action = "cancel", Text = "Cancelling" });
      _mTurnContextWrapper.Setup(x => x.SendActivityAsync(It.IsAny<ITurnContext>(),
        It.IsAny<Activity>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new ResourceResponse());
      _mDialogContextWrapper.Setup(x => x.CancelAllDialogsAsync(It.IsAny<DialogContext>(),
        It.IsAny<CancellationToken>()))
        .ReturnsAsync(new DialogTurnResult(DialogTurnStatus.Cancelled));

      var innerDc = new DialogContext(dialogSet, mTurnContext.Object, new DialogState());
      await Assert.ThrowsAsync<ArgumentException>(async () =>
        await _service.Handle(innerDc, Enums.InteruptAction.Help, new CancellationToken()));

      var result = await _service.Handle(innerDc, Enums.InteruptAction.Cancel, new CancellationToken());

      Assert.IsType<DialogTurnResult>(result);
      Assert.Equal(DialogTurnStatus.Cancelled, result.Status);
    }
  }
}
