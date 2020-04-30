using IISExceptionCapture.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ErrorHttpModule : IHttpModule
{
    private HttpApplication _context;

    public ErrorHttpModule() { }

    public void Init(HttpApplication context)
    {
        _context = context;
        _context.Error += new EventHandler(ErrorHandler);
    }

    private void ErrorHandler(object sender, EventArgs e)
    {
        Exception ex = _context.Server.GetLastError();

        if (ex != null)
            Logger.WriteToFile(ex.Message, ex);
    }

    public void Dispose()
    {
    }
}