namespace TheiaVR.Controllers.Listeners
{
	//Contract of each cloudListener and SkeletonListener
    public interface IStreamListener
    {

        void Start(string aHost, int aPort);

        void Stop();

        bool IsActive();

        void ParseStream(byte[] aBytes);

    }
}
