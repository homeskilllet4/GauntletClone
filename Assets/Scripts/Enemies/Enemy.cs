using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rank;

    protected int _magicPoints; //points awarded for using magic on this enemy
    protected int _shootingPoints; //points awarded for shooting this enemy
    protected int _fightPoints; //points awarded for fighting this enemy
    protected int _generatorPoints; //points awarded for destroying generator

    protected int _damage; //damage done to player
    protected int _hitPoints; //amount of life

    public bool isMagicDamagable; //is damagable by magic
    public bool isShootDamagable; //is damagable by shooting
    public bool isFightDamagable; //is damagable by fighting

    private float _moveSpeed = .05f; //base speed
    public int speed = 1; //speed multiplier

    public bool _isOnBlockade = false; //is blockade between the player and enemy
    public bool _isTouchingBlockade; //is it touching blockade?
    //public bool _isMoving; //is this enemy moving
    private RaycastHit hit;

    void FixedUpdate()
    {
        //check where the player is and follow them
        CheckPlayerPos();
        Debug.Log("Checking Player Pos");
    }

    protected void CheckPlayerPos()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10);
        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    //follow the player
                    FollowPlayer(hitCollider.gameObject);
                    Debug.Log("Player In Collider");
                }
            }
        }
    }

    private void FollowPlayer(GameObject player)
    {
        //move towards the player
        if (Physics.Linecast(transform.position, player.transform.position, out hit))
        {
            if (hit.transform.tag == "Blockade")
            {
                if (_isTouchingBlockade)
                {
                    _isOnBlockade = true;
                    Debug.Log("Blockade in the way");
                }
            }
            else
            {
                _isOnBlockade = false;
                _isTouchingBlockade = false;
            }
        }

        if (_isOnBlockade != true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * _moveSpeed);
            Debug.Log("Moving To Player");
        }
    }

    protected void CheckLives()
    {
        if (_hitPoints <= 0)
        {
            Disable();
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blockade"))
        {
            _isTouchingBlockade = true;
            Debug.Log("Touching blockade");
        }
    }
}
