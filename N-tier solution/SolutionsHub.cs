using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N_tier_solution
{
    public class SolutionsHub:Hub
    {
        public void Announce(string message)
        {
            Clients.All.Announce(message);
            
        }

        public void Notify(string message)
        {
            Clients.Caller.Notify(message);
        }
    }
}