using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
namespace FunctionApp2
{
    public class CubiHub : ServerlessHub
    {
        [FunctionName("negotiate")]
        public SignalRConnectionInfo Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req)
        {
            return Negotiate(req);
        }

        [FunctionName(nameof(OnConnected))]
        public async Task OnConnected([SignalRTrigger] InvocationContext invocationContext)
        {
            await Clients.All.SendAsync("OnConnected");
        }

        [FunctionName(nameof(Broadcast))]
        public async Task Broadcast([SignalRTrigger] InvocationContext invocationContext, string message)
        {
            await Clients.All.SendCoreAsync("Broadcast", Array.Empty<object>());
        }

        public void OnDisconnected([SignalRTrigger] InvocationContext invocationContext)
        {
        }

    }
}
