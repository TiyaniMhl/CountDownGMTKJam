
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class LevelDatabase
    {
        public static List<LevelObject> AllLevels;
        public static void BuildLevelList()
        {
            AllLevels = Resources.LoadAll<LevelObject>("Level Objects").ToList();
        }
        public static LevelObject FindLevel(int req)
        {
            return AllLevels.FirstOrDefault(lo => lo.levelNumber == req);
        }

        public static LevelObject MainMenu()
        {
            return AllLevels.FirstOrDefault(lo => lo.sceneName == "MainMenu");
        }
    }
