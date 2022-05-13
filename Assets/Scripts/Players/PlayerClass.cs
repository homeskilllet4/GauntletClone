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
        StartCoroutine(ConstantDamage());
    }


    public IEnumerator ConstantDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            health -= 3;
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Food")
        {
            health += 50;
            Destroy(other.gameObject);
        }
    }


    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }

    public void InitializePlayer(CharClass characterClass)
    {
        charClass = characterClass;
        className = characterClass.className;
        tag = characterClass.playerTag;
        movementSpeed = characterClass.movementSpeed;
        _characterMat = characterClass.playerMat;
        health = 600;
        keyCount = 0;
        potionCount = 0;
        GetComponent<Renderer>().material = _characterMat;
        switch (this.tag)
        {
            case "Player1":
                GetComponent<PlayerController>().playerProjectile = GetComponent<PlayerController>().Projectiles[0];
                break;
            case "Player2":
                GetComponent<PlayerController>().playerProjectile = GetComponent<PlayerController>().Projectiles[1];
                break;
            case "Player3":
                GetComponent<PlayerController>().playerProjectile = GetComponent<PlayerController>().Projectiles[2];
                break;
            case "Player4":
                GetComponent<PlayerController>().playerProjectile = GetComponent<PlayerController>().Projectiles[3];
                break;
        }
    }
}
