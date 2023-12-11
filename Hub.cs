using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace siKecil
{
    public class SignalRClient
    {
        private readonly HubConnection hubConnection;
        private readonly IHubProxy hubProxy;

        public SignalRClient(string serverUrl)
        {
            hubConnection = new HubConnection(serverUrl);
            hubProxy = hubConnection.CreateHubProxy("YourHubName");

            // Set up event handlers for receiving messages
            hubProxy.On<string>("ReceiveMessage", message => OnReceiveMessage(message));
        }

        public async Task StartConnectionAsync()
        {
            await hubConnection.Start();
        }

        public async Task SendMessageAsync(string message)
        {
            await hubProxy.Invoke("SendMessage", message);
        }

        private void OnReceiveMessage(string message)
        {
            // Handle received message
        }
    }
}
