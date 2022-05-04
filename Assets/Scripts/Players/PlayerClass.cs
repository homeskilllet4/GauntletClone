using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerClass : MonoBehaviour
{
    private int playerNum;
    public CharClass charClass;
    private Material _characterMat;

    

    [SerializeField]
    public string className;
    public int health;
    public float movementSpeed;
    public int keyCount;
    public int potionCount;

    public void Start()
    {
        //InitializePlayer();
        GameManager.instance.AddPlayer(this);
    }


    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }

    public void InitializePlayer(CharClass charClass)
    {
        this.charClass = charClass;
        this.className = charClass.className;
        this.tag = charClass.playerTag;
        movementSpeed = charClass.movementSpeed;
        _characterMat = charClass.playerMat;
        health = 600;
        keyCount = 0;
        potionCount = 0;
        GetComponent<Renderer>().material = _characterMat;
    }
}
