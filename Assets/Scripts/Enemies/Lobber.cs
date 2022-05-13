using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobber : Enemy
{
    public float _shootCD; //shooting cooldown
    public bool _canShoot = true; //is CD over
    public Transform spawnPoint; //where the bullet spawns

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
                    _damage = 3;
                    hitPoints = 1;
                    break;
                }
            case 2:
                {
                    _damage = 3;
                    hitPoints = 2;
                    break;
                }
            case 3:
                {
                    _damage = 3;
                    hitPoints = 3;
                    break;
                }
        }

        //set what the enemy can be damaged by
        isMagicDamagable = false;
        isShootDamagable = true;
        isFightDamagable = true;

        //set color based on rank
        CheckLives();
    }

    private void Update()
    {
        //shoots, then has a CD til it can shoot again.
        if (_canShoot)
            StartCoroutine(ShootPlayer());
    }

    private IEnumerator ShootPlayer()
    {
        //set bullet spawn location and rotation
        Vector3 spawnPosition = spawnPoint.position; ;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);

        //if the enemy can shoot, grab the object from the object pool and enable it, then disable shooting.
        if (_canShoot)
        {
            GameObject lobberProjectile = ObjectPooler.Instance.GetPooledObject("LobberProjectile");
            if (lobberProjectile != null)
            {
                lobberProjectile.transform.position = spawnPosition;
                lobberProjectile.transform.rotation = spawnRotation;
                //set the damage of the projectile
                lobberProjectile.GetComponent<LobberBullet>().damage = _damage;
                lobberProjectile.SetActive(true);
                _canShoot = false;
            }
        }

        //wait for cooldown
        yield return new WaitForSeconds(_shootCD);

        //reenable shooting
        _canShoot = true;
        Debug.Log("ShotCD reset");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
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
}
