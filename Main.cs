using System;
using System.Collections;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SaveCrash
{
    public class Main : RocketPlugin<Configuration>
    {
        public static Main Instance;
        public static Configuration Config;

        public const string Version = "1.0.1";

        protected override void Load()
        {
            Config = Configuration.Instance;
            
            Logger.Log($"SaveCrash by Johnanater, version: {Version}");
            Logger.Log(Translate("checking_in", Config.CheckTime));
            
            StartCoroutine(CheckCoroutine());
        }

        public IEnumerator CheckCoroutine()
        {
            yield return new WaitForSeconds(Config.CheckTime);
            
            CheckSave();
        }

        void CheckSave()
        {
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
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"try_save", "Let's try saving!"},
            {"save_success", "Save successful! We're good to go!"},
            {"save_failed", "Save failed! Restarting the server!"},
            {"checking_in", "Checking save in {0} seconds!"}
        };
    }
}