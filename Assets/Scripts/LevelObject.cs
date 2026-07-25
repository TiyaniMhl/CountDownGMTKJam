using UnityEngine;
[CreateAssetMenu(fileName = "NewLevel", menuName = "Scriptable Objects/Level")]
public class LevelObject : ScriptableObject
{
    public string sceneName;
    public int levelNumber;
    public int bullets;
    [Tooltip("Amount of bullets remaining required for three stars.")]
    public int threeStars;
    [Tooltip("Amount of bullets remaining required for two stars.")]
    public int twoStars;

    
}
