using Rocket.API;

namespace SaveCrash
{
    public class Configuration : IRocketPluginConfiguration
    {
        public int ShutdownTime;
        public int CheckTime;

        public void LoadDefaults()
        {
            ShutdownTime = 5;
            CheckTime = 30;
        }
    }
}