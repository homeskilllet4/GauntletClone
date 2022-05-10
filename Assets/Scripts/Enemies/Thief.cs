using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy
{
    public bool _itemIsStolen;

    private List<Vector3> travelPoints = new List<Vector3>(); //where has thief traveled

    public GameObject stolenItem; //item that was stolen

    private void OnEnable()
    {   
        //set point values
        _magicPoints = 10;
        _shootingPoints = 500;
        _fightPoints = 10;
        _generatorPoints = 10;

        _itemIsStolen = false;

        //set damage and health values based on rank
        switch (rank)
        {
            case 1:
                {
                    _damage = 10;
                    hitPoints = 1;
                    break;
                }
            case 2:
                {
                    _damage = 10;
                    hitPoints = 1;
                    break;
                }
            case 3:
                {
                    _damage = 10;
                    hitPoints = 1;
                    break;
                }
        }

        //set what the enemy can be damaged by
        isMagicDamagable = false;
        isShootDamagable = true;
        isFightDamagable = true;

        //set movement speed
        speed = 5;

        //set material based on rank
        CheckLives();

        //track points
        StartCoroutine(TrackPoints());
    }

    protected override void FixedUpdate()
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
        if (_itemIsStolen != true)
        {
            Vector3 playerPos = new Vector3(players[closestPlayer].transform.position.x, players[closestPlayer].transform.position.y, players[closestPlayer].transform.position.z);
            transform.LookAt(playerPos);
        }


        //follow the closest player
        if (_itemIsStolen != true)
            FollowPlayer(players[closestPlayer]);

    }

    //steal the players item
    public void StealItem()
    {
        _itemIsStolen = true;
        StopCoroutine(TrackPoints());
        StartCoroutine(ReversePoints());
    }

    //track the path until it steal an item
    private IEnumerator TrackPoints()
    {
        while (!_itemIsStolen)
        {
            travelPoints.Add(transform.position);

            yield return new WaitForSeconds(.01f);
        }
    }

    //go back to where it spawned in the same path
    private IEnumerator ReversePoints()
    {
        if (_itemIsStolen)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            travelPoints.Reverse();
            Debug.Log("Travel Points Reversed");


            for (int i = 0; i < travelPoints.Count; i++)
            {
                transform.LookAt(travelPoints[i]);
                transform.position = Vector3.Lerp(transform.position, travelPoints[i], .5f);
                Debug.Log("Travel Points Lerped");
                yield return new WaitForSeconds(.01f);
            }

            gameObject.SetActive(false);
            _itemIsStolen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player1":
                //other.GetComponent<PlayerSomething>().StealItem(); KADE IMPLEMENT
                StealItem();
                break;
            case "Player2":
                //other.GetComponent<PlayerSomething>().StealItem(); KADE IMPLEMENT
                StealItem();
                break;
            case "Player3":
                //other.GetComponent<PlayerSomething>().StealItem(); KADE IMPLEMENT
                StealItem();
                break;
            case "Player4":
                //other.GetComponent<PlayerSomething>().StealItem(); KADE IMPLEMENT
                StealItem();
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
                //sets the player that threw the potion to get the points
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

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!_itemIsStolen)
        {
            //if this enemy is touching the blockade, then set the bool to true so that the enemy will not move through blockade
            if (collision.gameObject.CompareTag("Blockade"))
            {
                _isTouchingBlockade = true;
                _isOnBlockade = true;
                Debug.Log("Touching blockade");
            }
        }
    }

}
