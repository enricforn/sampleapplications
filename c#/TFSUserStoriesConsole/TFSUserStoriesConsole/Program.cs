using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TFSUserStoriesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TeamFoundationServiceManager tfsSessionManager = new TeamFoundationServiceManager();

            tfsSessionManager.CloseWorkItem("13579");
        }
    }
}