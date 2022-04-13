using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rank;

    protected int _magicPoints;
    protected int _shootingPoints;
    protected int _fightPoints;
    protected int _generatorPoints;

    protected int _damage;
    protected int _hitPoints;

    public bool isMagicDamagable;
    public bool isShootDamagable;
    public bool isFightDamagable;

    private float _moveSpeed = .005f;
    public int speed = 1;

    void Update()
    {
        CheckPlayerPos();
        Debug.Log("Checking Player Pos");
    }

    private void CheckPlayerPos()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10);
        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    FollowPlayer(hitCollider.gameObject);
                    Debug.Log("Player In Collider");
                }
            }
        }
    }

    private void FollowPlayer(GameObject player)
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * _moveSpeed);
        Debug.Log("Moving To Player");
    }
}
