using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    private void OnEnable()
    {
        _magicPoints = 10;
        _shootingPoints = 5;
        _fightPoints = 25;
        _generatorPoints = 10;

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

        isMagicDamagable = true;
        isShootDamagable = true;
        isFightDamagable = true;

        CheckLives();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //deal damage to the player
        }

        //if it is a magic projectile add _magicPoints to points count and takes away one health then checks if all lives are gone

        //if player is hit with fight, add _fightPoints to ponits count and takes away one health then checks if all lives are gone

        //if it is a shoot projectile, add _shootPoints to points count and takes away one health then checks if all lives are gone
    }
}
