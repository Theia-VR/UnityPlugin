namespace TheiaVR.Controllers.Listeners
{
    public interface IStreamListener
    {

        void Start(string aHost, int aPort);

        void Stop();

        bool IsActive();

        void ParseStream(byte[] aBytes);

    }
}
