using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy
{
    private void OnEnable()
    {
        _magicPoints = 10;
        _shootingPoints = 500;
        _fightPoints = 10;
        _generatorPoints = 10;

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

        isMagicDamagable = false;
        isShootDamagable = true;
        isFightDamagable = true;

        speed = 5;

        CheckLives();
    }
}
