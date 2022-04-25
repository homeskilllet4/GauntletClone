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

        //start the invisibility loop
        StartCoroutine(GoInvis());
    }

    //turn this enemy invisible for the invis time, turns off invis, then loops
    IEnumerator GoInvis()
    {
        Renderer mR = GetComponent<Renderer>();
        Material ogMaterial;

        //always loop
        while (true)
        {
            //save the starting material, switch material to invisible material
            ogMaterial = mR.material;
            _isInvisible = true;
            mR.material = invisMat;

            //wait for the amount of time they are supposed to be invisible
            yield return new WaitForSeconds(invisTime);

            //turn off invisibility and switch back to original material
            Debug.Log("Go out of invis");
            _isInvisible = false;
            mR.material = ogMaterial;

            //wait until the cooldown is done then start invis again
            yield return new WaitForSeconds(invisCD);
        }
    }

    //if this enemy is touching the player and not invisible, do damage in specific intervals.
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
        //if this enemy is touching the player, set bool to true then activate fight damage
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
        {
            _isTouchingPlayer = true;
            StartCoroutine(FightPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if the player leaves the trigger zone, stop the coroutine for fighting player
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
        {
            _isTouchingPlayer = false;
            StopCoroutine(FightPlayer());
        }
    }


}
