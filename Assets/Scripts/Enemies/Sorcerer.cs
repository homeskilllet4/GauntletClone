using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : Enemy
{
    private bool _isInvisible = false;
    public float invisCD = 5.0f;
    public float invisTime = 3.0f;
    public Material invisMat;

    public float attackCooldown = 2.0f;
    private bool _isTouchingPlayer;


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

        StartCoroutine(GoInvis());
    }

    IEnumerator GoInvis()
    {
        Renderer mR = GetComponent<Renderer>();
        Material ogMaterial;

        while (true)
        {
            ogMaterial = mR.material;
            _isInvisible = true;
            mR.material = invisMat;

            yield return new WaitForSeconds(invisTime);

            Debug.Log("Go out of invis");
            _isInvisible = false;
            mR.material = ogMaterial;

            yield return new WaitForSeconds(invisCD);
        }
    }

    IEnumerator FightPlayer()
    {
        while (_isTouchingPlayer)
        {
            if (!_isInvisible)
            {
                //damage player

                yield return new WaitForSeconds(attackCooldown);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTouchingPlayer = true;
            StartCoroutine(FightPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTouchingPlayer = false;
        }
    }


}
