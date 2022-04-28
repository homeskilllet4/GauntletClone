using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy
{
    private int _drainRate = 3; //how fast does the health drain
    private int _drainLimit = 200; //how much health can the death drain?
    private int _healthDrained = 0; //how much health has been drained so far?
    private bool _isDrainLimitReached; //have we reached the health drain limit?
    private int _magicDamageTaken = 20; //damage taken when hit with magic

    private void OnEnable()
    {
        //set point values
        _magicPoints = 10;
        _shootingPoints = 1;
        _fightPoints = 0;
        _generatorPoints = 0;

        //set damage and health values based on rank
        switch (rank)
        {
            case 1:
                {
                    _damage = 3;
                    hitPoints = 200;
                    break;
                }
            case 2:
                {
                    _damage = 3;
                    hitPoints = 200;
                    break;
                }
            case 3:
                {
                    _damage = 3;
                    hitPoints = 200;
                    break;
                }
        }

        //set what the enemy can be damaged by
        isMagicDamagable = true;
        isShootDamagable = false;
        isFightDamagable = false;

        //set color based on rank
        _healthDrained = 0;
        CheckLives();
    }

    private void Update()
    {
        //if health drain limit is reached, then disable game object
        if (_healthDrained > _drainLimit)
            gameObject.SetActive(false);
    }

    IEnumerator DrainHealth()
    {
        while (_healthDrained <= _drainLimit)
        {
            //take player health at drain rate
            _healthDrained += _drainRate;

            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player enters the trigger range, start draining it's health
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
            if (_healthDrained <= _drainLimit)
                StartCoroutine(DrainHealth());
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
                //add _shootPoints to the player
                gameObject.SetActive(false);
                break;
            case "MagicProjectile":
                hitPoints = hitPoints - _magicDamageTaken;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if the player exits the trigger range, then stop the health drain
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
            StopCoroutine(DrainHealth());
    }
}
