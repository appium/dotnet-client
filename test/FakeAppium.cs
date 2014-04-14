using System;
using System.Threading;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenQA.Selenium.Appium
{

	public class RequestProcessor
	{
		protected string httpMethod = null; 
		protected string partialUrl = null; 
		protected Dictionary<string, object> result = null;

		public RequestProcessor(string httpMethod, string partialUrl, Dictionary<string, object> result){
			this.httpMethod = httpMethod;
			this.partialUrl = partialUrl;
			this.result = result;
		}
			
		public virtual bool process(HttpListenerRequest request, HttpListenerResponse response) {
			string rawUrl = "/wd/hub/session/1234" + partialUrl;
			if(request.HttpMethod == httpMethod && request.RawUrl == rawUrl ) {
				var data = result;
				if(data == null) { data = new Dictionary<string, object>(); }
				response.StatusCode = (int) HttpStatusCode.OK;
				response.ContentType = "application/json";
				Dictionary<string, object> fullData = new Dictionary<string, object>()
				{
					{"status", 0},
					{"sessionId", "1234"},
					{"value", data}
				};
				string jsonResponse = JsonConvert.SerializeObject (fullData); 
				byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);
				response.ContentLength64 = buffer.Length;
				response.OutputStream.Write(buffer, 0, buffer.Length);			
				return true;
			}
			return false;
		}
	}

	public class InitRequestProcessor: RequestProcessor
	{
		public InitRequestProcessor()
			: base ("POST", "", null)
		{
		}

		public override bool process(HttpListenerRequest request, HttpListenerResponse response) {
			string rawUrl = "/wd/hub/session";
			if(request.HttpMethod == httpMethod && request.RawUrl == rawUrl ) {
				response.StatusCode = (int)HttpStatusCode.Redirect;
				response.RedirectLocation = "/wd/hub/session/1234";
				return true;
			}
			return false;
		}
	}

	public class FakeAppium
	{
		private const string ListenBaseAddress = "http://127.0.0.1:4723";

		private Thread listenThread;
		private HttpListener httpListener;
		private bool listening;

		List<RequestProcessor> processors = new List<RequestProcessor>();

		public FakeAppium ()
		{
		}

		public void clear() {
			processors.RemoveAll (item => true);
		}

		public void respondToInit() {
			processors.Add (new InitRequestProcessor ());
			processors.Add (new RequestProcessor("GET", "", null));
		}
			
		public void respondTo(string httpMethod, string partialUrl, Dictionary<string, object> result) {
			processors.Add (new RequestProcessor(httpMethod, partialUrl, result));
		}

		public void Start() {
			httpListener = new HttpListener();
			httpListener.Prefixes.Add(new Uri(ListenBaseAddress).ToString());
			httpListener.Start();
			listening = true;

			listenThread = new Thread(Listen);
			listenThread.Start();
			listenThread.IsBackground = true;
		}

		public void Stop() {	
			listening = false;
		}

		private void Listen() {
			while (httpListener.IsListening) {
				if (!listening)
					return;

				HttpListenerContext context;
				try {
					context = httpListener.GetContext();
				} catch (HttpListenerException e) {
					Console.Error.WriteLine(e.Message);
					Console.Error.WriteLine(e.StackTrace);
					continue;
				}
				string httpMethod = context.Request.HttpMethod;
				string rawUrl = context.Request.RawUrl;

				Console.Out.WriteLine("Processing call to {0} {1}", httpMethod, rawUrl);
				bool processed = false;
				foreach (RequestProcessor processor in processors)
				{
					if (processor.process (context.Request, context.Response)) {
						processed = true;
						break;
					}
				}
				if (!processed) {
					context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
				}								
				context.Response.Close();
			}
		}
	}
}

