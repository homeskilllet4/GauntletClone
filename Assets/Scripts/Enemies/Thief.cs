using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy
{
    private void OnEnable()
    {
        //set point values
        _magicPoints = 10;
        _shootingPoints = 500;
        _fightPoints = 10;
        _generatorPoints = 10;

        //set damage and health values based on rank
        switch (rank)
        {
            case 1:
                {
                    _damage = 10;
                    hitPoints = 1;
                    break;
                }
            case 2:
                {
                    _damage = 10;
                    hitPoints = 1;
                    break;
                }
            case 3:
                {
                    _damage = 10;
                    hitPoints = 1;
                    break;
                }
        }

        //set what the enemy can be damaged by
        isMagicDamagable = false;
        isShootDamagable = true;
        isFightDamagable = true;

        //set movement speed
        speed = 5;

        //set material based on rank
        CheckLives();
    }

    //steal the players item
    public void StealItem()
    {
        //steal the player's item
    }

    private void OnTriggerEnter(Collider other)
    {
        _tag = other.gameObject.tag;

        switch (_tag)
        {
            case "Player1":
                //steal player's item
                gameObject.SetActive(false);
                break;
            case "Player2":
                //steal player's item
                gameObject.SetActive(false);
                break;
            case "Player3":
                //steal player's item
                gameObject.SetActive(false);
                break;
            case "Player4":
                //steal player's item
                gameObject.SetActive(false);
                break;
        }
    }
}
