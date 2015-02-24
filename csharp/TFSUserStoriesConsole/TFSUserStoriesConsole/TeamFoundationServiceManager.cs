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
    public class TeamFoundationServiceManager : IDisposable
    {
        private TfsTeamProjectCollection teamProjectCollection;
        private WorkItemStore workItemStore;

        public TeamFoundationServiceManager()
        {
            NetworkCredential myNetCredentials = new NetworkCredential("user", "password", "domain");
            ICredentials myCredentials = (ICredentials)myNetCredentials;

            Uri tfsUri = new Uri("http://tfsserver:8080/tfs/lludria");

            try
            {
                teamProjectCollection = new TfsTeamProjectCollection(tfsUri, myCredentials);

                teamProjectCollection.EnsureAuthenticated();

                workItemStore = teamProjectCollection.GetService<WorkItemStore>();

                WorkItemStore wrok = new WorkItemStore(teamProjectCollection);
            }
            catch (TeamFoundationServerUnauthorizedException)
            {
                Console.WriteLine("Invalid user or password, or server not available at this time.");
                throw;
            }
            catch (TeamFoundationServiceUnavailableException)
            {
                Console.WriteLine("TFS is not available at this time.");
                throw;
            }
        }

        public void CloseWorkItem(string workItemId)
        {
            string wiqlQuery = String.Format("SELECT * FROM WorkItems WHERE [System.TeamProject] = '{0}' AND [System.WorkItemType] = 'Task' AND [System.Id] = ", workItemId);

            WorkItemCollection witCollection = workItemStore.Query(wiqlQuery);

            foreach (WorkItem workItem in witCollection)
            {
                WorkItem editWorkItem = workItemStore.GetWorkItem(workItem.Id);

                editWorkItem.State = "Closed";

                editWorkItem.Save();
            }
        }

        public void Dispose()
        {
            teamProjectCollection.Dispose();
        }
    }
}
