using System;
using Newtonsoft.Json.Linq;

namespace Helpers {
    public class WebSocketRequest {
        private readonly String _command;
        private readonly dynamic _args;

        public String Command { get { return _command; } }
        public dynamic Arguments { get { return _args; }}

        public WebSocketRequest(String command, dynamic args) {
            Objects.RequireNonNull(command, "Command can't be null.");
            Objects.Check(command, r => r.Length > 0, "Command can't be empty.");

            this._command = command;
            this._args = args;
        }

        public WebSocketRequest(String request) {
            Objects.RequireNonNull(request, "Request can't be null.");
            Objects.Check(request, r => r.Length > 0, "Request can't be empty.");

            dynamic content = JObject.Parse(request);

            Objects.CheckIf(content.Command != null, "Request is invalid.");

            this._command = content.Command;
            this._args = content.Arguments != null ? content.Arguments : new {};
        }
    }
}