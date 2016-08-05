using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Helpers;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Kimanju {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();

            WebSocketsServer server = new WebSocketsServer(app);

            server.Map("Echo", args => {
                JObject response = new JObject();
                
                response.Add("Message", args.Message);

                return new WebSocketResponse(HttpStatusCode.Accepted, response);
            });

            server.Start();
        }
    }
}
