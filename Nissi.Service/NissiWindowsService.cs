using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.ServiceModel;

namespace Nissi.Service
{
    public class NissiWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public NissiWindowsService()
        {
            ServiceName = "NissiWCFService";
        }
        public static void Main()
        {
            ServiceBase.Run(new NissiWindowsService());

        }
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(Service));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
