using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace AW.Bots.DialogHelpers.Interfaces
{
  public interface ITurnContextWrapper
  {
    Task<ResourceResponse> SendActivityAsync(ITurnContext turnContext, Activity activity, CancellationToken cancellationToken);
  }
}