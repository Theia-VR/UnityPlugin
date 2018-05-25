namespace TheiaVR.Controllers.Listeners
{
    public interface StreamListener
    {

        void Start(string aHost, int aPort);

        void Stop();

        bool IsActive();

        void ParseStream(byte[] aBytes);

    }
}
