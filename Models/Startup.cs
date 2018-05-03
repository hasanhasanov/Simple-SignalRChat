using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SignalRChat;

[assembly: OwinStartup(typeof(Startup))]

namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR();
            app.Map("/signalr", builder =>
            {
                builder.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();
                builder.RunSignalR(hubConfiguration);

            });
        }
    }
}
