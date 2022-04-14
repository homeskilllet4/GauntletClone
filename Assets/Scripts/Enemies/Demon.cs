using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy
{
    public float _shootCD; //shooting cooldown
    public bool _canShoot = true; //is CD over

    public Transform spawnPoint;

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

    }

    private void Update()
    {
        if (_canShoot)
        {
            StartCoroutine(ShootPlayer());
        }
    }

    private void TrackPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10);
        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    //shoot the player
                    StartCoroutine(ShootPlayer());
                    Debug.Log("Initialized shooting");
                    Debug.Log("Player In Collider");
                }
            }
        }
    }

    private IEnumerator ShootPlayer()
    {
        Vector3 spawnPosition = spawnPoint.position; ;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);

        if (_canShoot)
        {
            GameObject demonProjectile = ObjectPooler.Instance.GetPooledObject("DemonProjectile");
            if (demonProjectile != null)
            {
                demonProjectile.transform.position = spawnPosition;
                demonProjectile.transform.rotation = spawnRotation;
                demonProjectile.SetActive(true);
                _canShoot = false;
                Debug.Log("Spawned Projectile");
            }
        }

        yield return new WaitForSeconds(_shootCD);

        _canShoot = true;
        Debug.Log("ShotCD reset");
    }


}
