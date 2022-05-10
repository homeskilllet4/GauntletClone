using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    private bool _isTouchingPlayer;
    public float attackCooldown = 1.0f;

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

    IEnumerator FightPlayer(PlayerClass player)
    {
        while (_isTouchingPlayer)
        {
            player.GetComponent<PlayerClass>().health -= _damage;
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player1":
                _isTouchingPlayer = true;
                StartCoroutine(FightPlayer(other.GetComponent<PlayerClass>()));
                break;
            case "Player2":
                _isTouchingPlayer = true;
                StartCoroutine(FightPlayer(other.GetComponent<PlayerClass>()));
                break;
            case "Player3":
                _isTouchingPlayer = true;
                StartCoroutine(FightPlayer(other.GetComponent<PlayerClass>()));
                break;
            case "Player4":
                _isTouchingPlayer = true;
                StartCoroutine(FightPlayer(other.GetComponent<PlayerClass>()));
                break;
            //player 1 gives shoot points
            case "Player1Projectile":
                playerForPoints = 1;
                if (isShootDamagable)
                    hitPoints--;
                CheckLives();
                if (hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_shootingPoints, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            //player 2 gives shoot points
            case "Player2Projectile":
                playerForPoints = 2;
                if (isShootDamagable)
                    hitPoints--;
                CheckLives();
                if (hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_shootingPoints, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            //player 3 does magic damage
            case "Player3Projectile":
                playerForPoints = 3;
                if (isMagicDamagable)
                    hitPoints--;
                CheckLives();
                if (hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_magicPoints, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            //player 4 does shoot damage
            case "Player4Projectile":
                playerForPoints = 4;
                if (isShootDamagable)
                    hitPoints--;
                CheckLives();
                if (hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_shootingPoints, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            case "Potion":
                //set the player that threw the potion to get the points
                playerForPoints = other.GetComponent<Potion>().playerThatThrew;
                hitPoints = 0;
                GameManager.instance.AddPoints(_potionPoints, playerForPoints);
                gameObject.SetActive(false);
                break;
            //player 1 fight damage
            case "FightWeapon":
                playerForPoints = 1;
                if (isFightDamagable)
                    hitPoints--;
                CheckLives();
                if (hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_fightPoints, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if the player leaves the trigger zone, stop the coroutine for fighting player
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
        {
            _isTouchingPlayer = false;
            StopCoroutine(FightPlayer(other.GetComponent<PlayerClass>()));
        }
    }
}
