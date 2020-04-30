using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace IISExceptionCapture.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (e.ExceptionObject as Exception);

            if (ex != null)
                Logger.WriteToFile(ex.Message, ex);
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex != null)
                Logger.WriteToFile(ex.Message, ex);
        }
    }
}
