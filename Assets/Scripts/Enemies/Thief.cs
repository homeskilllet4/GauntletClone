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
}
