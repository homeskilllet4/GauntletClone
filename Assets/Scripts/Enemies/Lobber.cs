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
                lobberProjectile.SetActive(true);
                _canShoot = false;
                Debug.Log("Spawned Projectile");
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
                hitPoints--;
                if (hitPoints <= 0)
                {
                    //add _shootPoints to the player
                    gameObject.SetActive(false);
                }
                break;
            case "Potion":
                hitPoints = 0;
                //add _potionPoints to player
                gameObject.SetActive(false);
                break;
                //need to set up fight damage
        }
    }
}
