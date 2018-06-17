namespace TheiaVR.Controllers.Listeners
{
    /// <summary>
    /// Contract of each CloudListener and SkeletonListener
    /// </summary>
    public interface IStreamListener
    {
        /// <summary>
        /// Starting a listener with an ip and a port.
        /// </summary>
        /// <param name="aHost">The IP address</param>
        /// <param name="aPort">The port </param>
        void Start(string aHost, int aPort);

        /// <summary>
        /// Stopping the listener
        /// </summary>
        void Stop();

        /// <summary>
        /// Get the status of the listener
        /// </summary>
        /// <returns>true if the listener is active</returns>
        bool IsActive();

        /// <summary>
        /// Parse the listened data
        /// </summary>
        /// <param name="aBytes">The bytes array to parse</param>
        void ParseStream(byte[] aBytes);

    }
}
