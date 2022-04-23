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

    public GameObject[] players = new GameObject[4]; //array of players
    public float[] distance = new float[4]; //array of distances between player and enemy
    public int closestPlayer = 0; //which enemy in array is the closest

    public Material life1, life2, life3; //materials for each health value

    void FixedUpdate()
    {
        //check where the player is and follow them
        CheckPlayerPos();
        Debug.Log("Checking Player Pos");

        //set the minimum distance to the highest value to be able to have values smaller than it.
        float minDistance = float.MaxValue;

        //for each spot in the player array
        for (int i = 0; i < players.Length; i++)
        {
            //if the player is in the array
            if (players[i] != null)
            {
                //check the distance between the player and this enemy
                float thisDistance = Vector3.Distance(transform.position, players[i].transform.position);

                //save the distance
                distance[i] = thisDistance;

                //if the previous distance is smaller than the minimum distance, set that as the minimum distance
                if (thisDistance < minDistance)
                {
                    minDistance = thisDistance;

                    //set closest player to the player's spot in the array
                    closestPlayer = i;
                    Debug.Log(closestPlayer);
                }
            }
        }

        //set closest player's position and look in their direction
        Vector3 playerPos = new Vector3(players[closestPlayer].transform.position.x, players[closestPlayer].transform.position.y, players[closestPlayer].transform.position.z);
        transform.LookAt(playerPos);

        //follow the closest player
        FollowPlayer(players[closestPlayer]);
    }

    //find the player position
    protected void CheckPlayerPos()
    {
        //check if players are in the sphere collider
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 20);
        //if the sphere collider is not empty
        if (hitColliders.Length > 0)
        {
            //player is in collider
            _isPlayerInCollider = true;
            
            //for each player in collider sphere, set the player's spot in array
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
        //if object is between the player and this enemy
        if (Physics.Linecast(transform.position, player.transform.position, out hit))
        {
            //if that object is a wall/blockade
            if (hit.transform.tag == "Blockade")
            {
                //the enemy is touching the blockade
                if (_isTouchingBlockade)
                {
                    //set bool to true that blockade is in the way so that the enemy won't move through the blockade.
                    _isOnBlockade = true;
                    Debug.Log("Blockade in the way");
                }
            }
            else
            {
                //if blockade is not in the way, then set both blockade bools to false
                _isOnBlockade = false;
                _isTouchingBlockade = false;
            }
        }

        //if the enemy is not on a blockade, move towards the player
        if (_isOnBlockade != true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * _moveSpeed);
            Debug.Log("Moving To Player");
        }
    }

    public void CheckLives()
    {
        Renderer mat = GetComponent<Renderer>();
        
        //if this enemy is out of lives, disable the game object.
        if (hitPoints <= 0)
            Disable();

        //based on hit points, change the material applied to this enemy
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

    //public way to disable this game object
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if this enemy is touching the blockade, then set the bool to true so that the enemy will not move through blockade
        if (collision.gameObject.CompareTag("Blockade"))
        {
            _isTouchingBlockade = true;
            Debug.Log("Touching blockade");
        }
    }
}
