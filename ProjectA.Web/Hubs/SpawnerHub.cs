using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ProjectA.Web.Hubs
{
    public class SpawnerHub : Hub
    {
        public override Task OnConnected()
        {
            Clients.Caller.initHub();
            return base.OnConnected();
        }
    }
}