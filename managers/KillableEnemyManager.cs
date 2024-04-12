using System.Collections.Generic;

namespace KillableEnemies.managers
{
    internal class KillableEnemyManager
    {
        private static KillableEnemyManager _instance;
        public static KillableEnemyManager Instance => _instance ?? (_instance = new KillableEnemyManager());

        public Dictionary<ulong, EnemyAI> NetworkObjectIDToEnemyAI { get; } = new Dictionary<ulong, EnemyAI>();
        public Dictionary<EnemyAI, ulong> EnemyAIToNetworkObjectID { get; } = new Dictionary<EnemyAI, ulong>();

        // Existing properties
        public bool DoJesterKill { get; set; }
        public bool DoGhostGirlKill { get; set; }
        public bool DoGiantKill { get; set; }
        public bool DoCoilHeadKill { get; set; }
        public bool DoSporeLizardKill { get; set; }
        public bool DoBaboonBirdKill { get; set; }
        public bool DoButlerEnemyKill { get; set; }
        public bool DoCentipedeKill { get; set; }
        public bool DoCrawlerKill { get; set; }
        public bool DoHoarderBugKill { get; set; }
        public bool DoLassoManKill { get; set; }
        public bool DoMouthDogKill { get; set; }
        public bool DoNutcrackerEnemyKill { get; set; }
        public bool DoRadMechKill { get; set; }
        public bool DoSandSpiderKill { get; set; }

        public int JesterHP { get; set; }
        public int GhostGirlHP { get; set; }
        public int GiantHP { get; set; }
        public int CoilHeadHP { get; set; }
        public int SporeLizardHP { get; set; }

        public int BaboonBirdHP { get; set; }
        public int ButlerEnemyHP { get; set; }
        public int CentipedeHP { get; set; }
        public int CrawlerHP { get; set; }
        public int HoarderBugHP { get; set; }
        public int LassoManHP { get; set; }
        public int MouthDogHP { get; set; }
        public int NutcrackerEnemyHP { get; set; }
        public int RadMechHP { get; set; }
        public int SandSpiderHP { get; set; }
    }
}