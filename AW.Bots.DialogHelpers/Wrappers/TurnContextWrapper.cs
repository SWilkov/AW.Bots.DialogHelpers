using AW.Bots.DialogHelpers.Interfaces;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Bots.DialogHelpers.Wrappers
{
  public class TurnContextWrapper : ITurnContextWrapper
  {
    public async Task<ResourceResponse> SendActivityAsync(ITurnContext turnContext, Activity activity,
      CancellationToken cancellationToken)
    {
      return await turnContext.SendActivityAsync(activity, cancellationToken);
    }
  }
}
