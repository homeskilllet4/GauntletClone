using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private void OnEnable()
    {
        //set point values
        _magicPoints = 10;
        _shootingPoints = 10;
        _fightPoints = 0;
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
                    _damage = 20;
                    hitPoints = 2;
                    break;
                }
            case 3:
                {
                    _damage = 30;
                    hitPoints = 3;
                    break;
                }
        }

        //set what the enemy can be damaged by
        isMagicDamagable = true;
        isShootDamagable = true;
        isFightDamagable = false;

        //set color based on rank
        CheckLives();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if trigger is player, deal damage to them and then disable this enemy GO
        if (other.CompareTag("Player"))
        {
            //deal damage to the player
            gameObject.SetActive(false);
        }
        //if it is a magic projectile add _magicPoints to points count and take away a hit point then check lives
        
        //if it is a shoot projectile, add _shootPoints to points count and take away a hit point then check lives
    }
}
