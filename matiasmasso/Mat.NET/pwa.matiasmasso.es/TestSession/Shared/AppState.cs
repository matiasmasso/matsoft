using static System.Net.WebRequestMethods;

namespace TestSession.Shared
{
    public class AppState
    {
        public List<Session> Sessions { get; set; } = new();
        private HttpClient? http;
        public int Counter { get; set; }

        public Session? Session(Guid? guid) => Sessions.FirstOrDefault(x => x.Guid == guid);
        public Session AddSession()
        {
            var retval = new Session(http);
            Sessions.Add(retval);
            return retval;
        }
    }
}
