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
    public int hitPoints; //amount of life

    public bool isMagicDamagable; //is damagable by magic
    public bool isShootDamagable; //is damagable by shooting
    public bool isFightDamagable; //is damagable by fighting

    private float _moveSpeed = .05f; //base speed
    public int speed = 1; //speed multiplier

    private bool _isOnBlockade = false; //is blockade between the player and enemy
    private bool _isTouchingBlockade; //is it touching blockade?
    private bool _isPlayerInCollider; //is the player in the collider?
    //public bool _isMoving; //is this enemy moving
    private RaycastHit hit;

    public GameObject[] players = new GameObject[4];
    public float[] distance = new float[4];
    public int closestPlayer = 0;

    public Material life1, life2, life3;

    void FixedUpdate()
    {
        //check where the player is and follow them
        CheckPlayerPos();
        Debug.Log("Checking Player Pos");

        float minDistance = float.MaxValue;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                float thisDistance = Vector3.Distance(transform.position, players[i].transform.position);

                distance[i] = thisDistance;

                if (thisDistance < minDistance)
                {
                    minDistance = thisDistance;
                    closestPlayer = i;
                    Debug.Log(closestPlayer);
                }
            }
        }

        Vector3 playerPos = new Vector3(players[closestPlayer].transform.position.x, players[closestPlayer].transform.position.y, players[closestPlayer].transform.position.z);
        transform.LookAt(playerPos);

        FollowPlayer(players[closestPlayer]);
    }

    protected void CheckPlayerPos()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 20);
        if (hitColliders.Length > 0)
        {
            _isPlayerInCollider = true;
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    if (players[0] == null)
                        players[0] = hitCollider.gameObject;
                    else if (players[1] == null)
                        players[1] = hitCollider.gameObject;
                    else if (players[2] == null)
                        players[2] = hitCollider.gameObject;
                    else if (players[3] == null)
                        players[3] = hitCollider.gameObject;

                    //follow the player
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

    public void CheckLives()
    {
        Renderer mat = GetComponent<Renderer>();

        if (hitPoints <= 0)
            Disable();

        switch (hitPoints)
        {
            case 1:
                mat.material = life1;
                break;
            case 2:
                mat.material = life2;
                break;
            case 3:
                mat.material = life3;
                break;
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
