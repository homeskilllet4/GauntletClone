using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public CharClass _charClass;
    private Material _characterMat;

    [SerializeField]
    private int health;
    private int keyCount;
    private int potionCount;

   public void InitailizePlayer(CharClass charClass)
    {
        _charClass = charClass;

        _characterMat = _charClass.playerMat;
        GetComponent<Renderer>().material = _characterMat;
    }

    public void Start()
    {
        health = 600;
        keyCount = 0;
        potionCount = 0;

    }
}
