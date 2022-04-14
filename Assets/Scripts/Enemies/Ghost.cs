using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private void OnEnable()
    {
        _magicPoints = 10;
        _shootingPoints = 10;
        _fightPoints = 0;
        _generatorPoints = 10;

        switch (rank)
        {
            case 1:
                {
                    _damage = 10;
                    break;
                }
            case 2:
                {
                    _damage = 20;
                    break;
                }
            case 3:
                {
                    _damage = 30;
                    break;
                }
        }

        isMagicDamagable = true;
        isShootDamagable = true;
        isFightDamagable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //deal damage to the player
            gameObject.SetActive(false);
        }
        //if it is a magic projectile add _magicPoints to points count and take away a hit point then check lives
        
        //if it is a shoot projectile, add _shootPoints to points count and take away a hit point then check lives
    }
}
