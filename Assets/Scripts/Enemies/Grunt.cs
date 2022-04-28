using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    private void OnEnable()
    {
        //set point values
        _magicPoints = 10;
        _shootingPoints = 5;
        _fightPoints = 25;
        _generatorPoints = 10;

        //set damage and health values based on rank
        switch (rank)
        {
            case 1:
                {
                    _damage = 5;
                    hitPoints = 1;
                    break;
                }
            case 2:
                {
                    _damage = 8;
                    hitPoints = 2;
                    break;
                }
            case 3:
                {
                    _damage = 10;
                    hitPoints = 3;
                    break;
                }
        }

        //set what the enemy can be damaged by
        isMagicDamagable = true;
        isShootDamagable = true;
        isFightDamagable = true;

        //set color based on rank
        CheckLives();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if trigger is player, deal damage to them
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
        {
            //deal damage to the player
        }

        //if trigger is player projectile, take a life from this enemy and change color by new health value.
        if (other.CompareTag("PlayerProjectile"))
        {
            hitPoints--;
            CheckLives();
        }

        switch (other.tag)
        {
            case "Player1":
                playerForPoints = 1;
                //add points to player
                break;
            case "Player2":
                playerForPoints = 2;
                //add points to player
                break;
            case "Player3":
                playerForPoints = 3;
                //add points to player
                break;
            case "Player4":
                playerForPoints = 4;
                //add points to player
                break;
            case "PlayerProjectile":
                hitPoints--;
                if (hitPoints <= 0)
                {
                    //add _shootPoints to the player
                    gameObject.SetActive(false);
                }
                break;
            case "MagicProjectile":
                hitPoints--;
                if (hitPoints <= 0)
                {
                    //add _magicPoints to the player
                    gameObject.SetActive(false);
                }
                break;
            case "Potion":
                hitPoints = 0;
                //add _potionPoints to player
                gameObject.SetActive(false);
                break;
            //need to set up fight damage
        }
    }
}
