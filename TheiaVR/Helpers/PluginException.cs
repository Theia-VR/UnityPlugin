using System;

namespace TheiaVR.Helpers
{
    [Serializable]
    public class PluginException : Exception
    {
        public PluginException(string aMessage) : base(aMessage)
        {
            // Custom exception
        }
    }
}
