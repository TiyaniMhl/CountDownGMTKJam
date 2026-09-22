using System.Collections.Generic;
using System.IO;

using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<bool> levelsCompleted;
    public List<int> levelStars;
    public int continueFromLevel;
}

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");
    public static void SaveGame(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
    }

    public static SaveData LoadGame()
    {
        LevelDatabase.BuildLevelList();
        if (!File.Exists(SavePath))
        {
            return NewSave();
        }
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static void Delete()
    {
        File.Delete(SavePath);
    }
   static SaveData NewSave()
   {
       SaveData toReturn = new SaveData
       {
           levelsCompleted = new List<bool>(),
           levelStars = new List<int>(),
           continueFromLevel = 1
       };
       foreach (var unused in LevelDatabase.AllLevels)
       {
           toReturn.levelsCompleted.Add(false);
           toReturn.levelStars.Add(0);
       }
       return toReturn;
   }

}
