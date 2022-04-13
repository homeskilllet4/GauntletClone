using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    protected int _magicPoints;
    protected int _shootingPoints;
    protected int _fightPoints;
    protected int _generatorPoints;

    protected float _damage;
    protected int _hitPoints;

    public bool isMagicDamagable;
    public bool isShootDamagable;
    public bool isFightDamagable;
}
