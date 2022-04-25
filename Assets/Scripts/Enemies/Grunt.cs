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

        //if it is a magic projectile add _magicPoints to points count and takes away one health then checks if all lives are gone

        //if player is hit with fight, add _fightPoints to ponits count and takes away one health then checks if all lives are gone

        //if it is a shoot projectile, add _shootPoints to points count and takes away one health then checks if all lives are gone
    }
}
