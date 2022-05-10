using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject player;
    public int playerThatThrew;

    private void OnEnable()
    {
        switch (player.tag)
        {
            case "Player1":
                playerThatThrew = 1;
                break;
            case "Player2":
                playerThatThrew = 2;
                break;
            case "Player3":
                playerThatThrew = 3;
                break;
            case "Player4":
                playerThatThrew = 4;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
