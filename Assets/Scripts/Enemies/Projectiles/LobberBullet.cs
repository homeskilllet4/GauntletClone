using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobberBullet : MonoBehaviour
{
    public GameObject[] players = new GameObject[4]; //the array of players
    private float[] distance = new float[4]; //array of distances from players
    private int closestPlayer = 0; //spot in array of closest player

    public float speed = 2.0f; //movement speed
    private float _damage; //damage dealt to entities hit

    float minDistance; //float for the minimum distance

    private void OnEnable()
    {
        //track the player's location
        TrackPlayer();

        //start the timer to disable the object
        StartCoroutine(DisableThyself());

        //look at the location of the player
        if (players[closestPlayer] != null)
        {
            Vector3 playerPos = new Vector3(players[closestPlayer].transform.position.x, players[closestPlayer].transform.position.y, players[closestPlayer].transform.position.z);
            transform.LookAt(playerPos);
        }

        //set damage values
        _damage = 10;

        //set min distance high so other distances can be lower than it.
        minDistance = float.MaxValue;
    }

    private void Update()
    {
        //move forward
        transform.position += transform.forward * speed * Time.deltaTime;

        //for each player in array
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
    }

    //track player
    private void TrackPlayer()
    {
        //check if players are in the sphere collider
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 30);
        //if the sphere collider is not empty
        if (hitColliders.Length > 0)
        {
            //for each player in collider sphere, set the player's spot in array
            foreach (Collider hitCollider in hitColliders)
            {
                switch (hitCollider.tag)
                {
                    case "Player1":
                        if (players[0] == null)
                            players[0] = hitCollider.gameObject;
                        break;
                    case "Player2":
                        if (players[1] == null)
                            players[1] = hitCollider.gameObject;
                        break;
                    case "Player3":
                        if (players[2] == null)
                            players[2] = hitCollider.gameObject;
                        break;
                    case "Player4":
                        if (players[3] == null)
                            players[3] = hitCollider.gameObject;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //after 6 seconds, disable this game object
    IEnumerator DisableThyself()
    {
        yield return new WaitForSeconds(6);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if this bullet hits player, deal damage and set this game object to disabled
        if (other.CompareTag("Player"))
        {
            //deal damage to the player
            gameObject.SetActive(false);
        }

        //if this bullet hits blockade or generator set GO to disabled
        if (other.CompareTag("Blockade") || other.CompareTag("Generator"))
        {
            gameObject.SetActive(false);
        }
    }
}
