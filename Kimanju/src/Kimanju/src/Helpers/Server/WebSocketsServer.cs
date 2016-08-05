using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json.Linq;

namespace Helpers {
    public class WebSocketsServer {
        private readonly Dictionary<String, Func<dynamic, WebSocketResponse>> _commands = new Dictionary<String, Func<dynamic, WebSocketResponse>>();
        private bool _started = false;

        public WebSocketsServer(IApplicationBuilder app) {
            app.Use(async (context, next) => {
                if (_started && context.WebSockets.IsWebSocketRequest) {
                    var socket = await context.WebSockets.AcceptWebSocketAsync();
                    await HandleClient(socket);
                    return;
                }
                
                await next();
            });
        }

        private async Task HandleClient(WebSocket socket) {
            byte[] buffer = new byte[1024 * 4];
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue) {
                var message = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                var response = HandleRequest(socket, message);
                
                await socket.SendAsync(
                    new ArraySegment<byte>(Encoding.UTF8.GetBytes(response.ToString())),
                    result.MessageType,
                    result.EndOfMessage,
                    CancellationToken.None
                );

                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private WebSocketResponse HandleRequest(WebSocket socket, String request) {
            WebSocketResponse response;

            try {
                WebSocketRequest webSocketRequest = new WebSocketRequest(request);
                
                Func<dynamic, WebSocketResponse> handler;
                _commands.TryGetValue(webSocketRequest.Command.ToLower(), out handler);

                if (handler != null) {
                    response = handler(webSocketRequest.Arguments);
                } else {
                    response = new WebSocketResponse(HttpStatusCode.NotFound, new JObject());
                }
            } catch (Exception) {
                response = new WebSocketResponse(HttpStatusCode.BadRequest, new JObject());
            }

            return response;
        }

        public void Map(String command, Func<dynamic, WebSocketResponse> handler) {
            Objects.RequireNonNull(command, "Command to map can't be null.");
            Objects.RequireNonNull(handler, "Command handler can't be null.");

            if (_started) {
                throw new InvalidOperationException("Adding a command mapping is not allowed when the server is working.");
            }

            Objects.Check(command, c => !_commands.ContainsKey(command.ToLower()), String.Format("Command '{0}' is already mapped to a handler.", command));

            _commands.Add(command.ToLower(), handler);
        }

        public void Start() {
            _started = true;
        }
    }
} 