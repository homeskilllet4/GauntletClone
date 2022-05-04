using UnityEngine;

[CreateAssetMenu(fileName = "Character Class", menuName = "ScriptableObjects/CharacterClass", order = 1)]
public class CharClass : ScriptableObject
{
    public string className;
    public string playerTag;
    public Material playerMat;
    public GameObject playerProjectile;
   
    [Range(1,3), Tooltip("Player speed: 1=slow, 2=moderate, 3=fast")]
    public int movementSpeed;

    [Range(1, 3), Tooltip("Shot speed: 1=slow, 2=moderate, 3=fast")]
    public int shotSpeed;

    [Range(0, 3), Tooltip("Character Armor: 1=none, 2=moderate, 3=fast")]
    public int armor;
}
