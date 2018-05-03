using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        static readonly List<ConnectionModelItem> Model = new List<ConnectionModelItem>();
        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;
            var userName = Context.QueryString["username"];

            var connections = new ConnectionModelItem
            {
                Name = userName,
                ConnectionId = connectionId
            };

            Model.Add(connections);

            return base.OnConnected();
        }

        public void Send(string userName, string messgae)
        {
            var toName = messgae.Split(':').ToArray();
            var toUserId = Model.Where(y => y.Name == toName.FirstOrDefault()).Select(x => x.ConnectionId).FirstOrDefault();
            var userId = Model.Where(y => y.Name == userName).Select(x => x.ConnectionId).FirstOrDefault();
            List<string> connectionsList = new List<string> { userId, toUserId };
            Clients.Clients(connectionsList).broadcastMessage(userName, toName[1]);
        }



        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}