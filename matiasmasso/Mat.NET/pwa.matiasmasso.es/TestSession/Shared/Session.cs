namespace TestSession.Shared
{
    public class Session
    {
        public Guid Guid { get; set; }
        public int Counter { get; set; }

        private HttpClient? http;

        public Session(HttpClient? http)
        {
            Guid= Guid.NewGuid();
            this.http = http;
        }


    }
}
