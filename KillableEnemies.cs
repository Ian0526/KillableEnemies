using BepInEx.Configuration;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using KillableEnemies.hooks;
using KillableEnemies.managers;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KillableEnemies
{

    [BepInPlugin(modGUID, modName, modVersion)]
    public class KillableEnemies : BaseUnityPlugin
    {
        private const string modGUID = "Ovchinikov.KillableEnemies.Main";
        private const string modName = "KillableEnemies";
        private const string modVersion = "0.1.3";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static KillableEnemies instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Enabling KillableEnemies");

            InitializeConfigValues();

            harmony.PatchAll(typeof(VanillaHook));

            mls.LogInfo("Finished Enabling KillableEnemies");
        }

        private void InitializeConfigValues()
        {
            mls.LogInfo("Parsing KillableEnemies config");

            var enemyTypes = new List<(string DisplayName, bool DefaultKillable, int DefaultHP)>
            {
                ("Jester", false, 100),
                ("Ghost Girl", false, 100),
                ("Giant", false, 100),
                ("Coil Head", false, 100),
                ("Spore Lizard", false, 100),
                ("Baboon Bird", false, 100),
                ("Butler Enemy", false, 100),
                ("Centipede", false, 100),
                ("Crawler", false, 100),
                ("Hoarder Bug", false, 100),
                ("Lasso Man", false, 100),
                ("Mouth Dog", false, 100),
                ("Nutcracker Enemy", false, 100),
                ("Rad Mech", false, 100),
                ("Sand Spider", false, 100)
            };

            foreach (var enemy in enemyTypes)
            {
                ConfigEntry<bool> killableEntry = this.Config.Bind<bool>("Killable Enemies", $"Killable {enemy.DisplayName}", enemy.DefaultKillable, $"Should {enemy.DisplayName}s be killable?");
                ConfigEntry<int> hpEntry = this.Config.Bind<int>("Enemy Health", $"{enemy.DisplayName} Health", enemy.DefaultHP, $"Health points for {enemy.DisplayName}s.");

                string killPropertyName = FormatPropertyName(enemy.DisplayName, "Do");
                string hpPropertyName = FormatPropertyName(enemy.DisplayName, "");

                typeof(KillableEnemyManager).GetProperty(killPropertyName + "Kill").SetValue(KillableEnemyManager.Instance, killableEntry.Value, null);
                typeof(KillableEnemyManager).GetProperty(hpPropertyName + "HP").SetValue(KillableEnemyManager.Instance, hpEntry.Value, null);

                killableEntry.SettingChanged += (sender, args) =>
                {
                    if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                    {
                        typeof(KillableEnemyManager).GetProperty(killPropertyName).SetValue(KillableEnemyManager.Instance, killableEntry.Value, null);
                    }
                };

                hpEntry.SettingChanged += (sender, args) =>
                {
                    if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                    {
                        typeof(KillableEnemyManager).GetProperty(hpPropertyName + "HP").SetValue(KillableEnemyManager.Instance, hpEntry.Value, null);
                    }
                };
            }

            mls.LogInfo("Config finished parsing");
        }

        private string FormatPropertyName(string displayName, string prefix)
        {
            string concatenatedName = String.Concat(displayName.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
            return $"{prefix}{concatenatedName}";
        }
    }
}