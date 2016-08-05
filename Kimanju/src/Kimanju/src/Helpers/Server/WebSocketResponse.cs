using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Helpers {
    public class WebSocketResponse {
        private readonly HttpStatusCode _code;
        private readonly JObject _response;

        public HttpStatusCode Code { get { return _code; } }
        public JObject Response { get { return new JObject(_response); }}

        public WebSocketResponse(HttpStatusCode code, JObject response) {
            this._code = Objects.RequireNonNull(code, "Response code can't be null.");
            this._response = Objects.RequireNonNull(response, "Response data can't be null.");
        }

        public override String ToString() {
            JObject response = new JObject();
            
            response.Add("Code", (int) _code);
            response.Add("Response", _response);

            return response.ToString();
        }
    }
}