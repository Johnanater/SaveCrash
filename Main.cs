using System;
using System.Threading;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;

namespace SaveCrash
{
    public class Main : RocketPlugin<Configuration>
    {
        public static Main Instance;
        public static Configuration Config;

        protected override void Load()
        {
            Config = Configuration.Instance;
            CheckSave();
        }

        void CheckSave()
        {
            new Thread(() =>
            {
                Thread.Sleep(Config.CheckTime * 1000);
                Logger.Log(Translate("try_save"));
                try
                {
                    SaveManager.save();
                    Logger.Log(Translate("save_success"));
                }
                catch (Exception e)
                {
                    Logger.Log(Translate("save_failed"));
                    Logger.Log(e);
                    Provider.shutdown(Config.ShutdownTime);
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"try_save", "Let's try saving!"},
            {"save_success", "Save successful! We're good to go!"},
            {"save_failed", "Save failed! Restarting the server!"}
        };
    }
}