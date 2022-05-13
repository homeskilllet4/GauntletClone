using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject playerToFollow;
    public Vector3 offset;
    private bool lookingForPlayer;

    void Start()
    {
        lookingForPlayer = true;
        StartCoroutine(FindAValidPlayer());
    }



    void LateUpdate()
    {
        if(playerToFollow != null)
            transform.position = playerToFollow.transform.position + offset;
    }



    IEnumerator FindAValidPlayer()
    {
        while(lookingForPlayer)
        {
            yield return new WaitForSeconds(1f);
            if (GameObject.Find("Player1"))
            {
                playerToFollow = GameObject.Find("Player1");
                lookingForPlayer = false;
            }
            else if (GameObject.Find("Player2"))
            {
                playerToFollow = GameObject.Find("Player2");
                lookingForPlayer = false;

            }
            else if (GameObject.Find("Player3"))
            {
                playerToFollow = GameObject.Find("Player3");
                lookingForPlayer = false;

            }
            else if (GameObject.Find("Player4"))
            {
                playerToFollow = GameObject.Find("Player4");
                lookingForPlayer = false;

            }
            else
            {
                Debug.Log("No player to follow with Camera");
            }
            if(playerToFollow != null)
                offset = transform.position - playerToFollow.transform.position;
        }
    }
}
