using System.Collections.Generic;

namespace KillableEnemies.managers
{
    internal class KillableEnemyManager
    {
        private static KillableEnemyManager _instance;
        public static KillableEnemyManager Instance => _instance ?? (_instance = new KillableEnemyManager());

        public Dictionary<ulong, EnemyAI> NetworkObjectIDToEnemyAI { get; } = new Dictionary<ulong, EnemyAI>();
        public Dictionary<EnemyAI, ulong> EnemyAIToNetworkObjectID { get; } = new Dictionary<EnemyAI, ulong>();

        public bool DoJesterKill { get; set; }
        public bool DoGhostGirlKill { get; set; }
        public bool DoGiantKill { get; set; }
        public bool DoCoilHeadKill { get; set; }
        public bool DoSporeLizardKill { get; set; }

        public int JesterHP { get; set; }
        public int GhostGirlHP { get; set; }
        public int GiantHP { get; set; }
        public int CoilHeadHP { get; set; }
        public int SporeLizardHP { get; set; }
    }
}
