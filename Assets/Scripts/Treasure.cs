using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public int points; //how many points are awarded
    public int player; //which player triggered the treasure

    private string _tag;

    private void OnTriggerEnter(Collider other)
    {
        _tag = other.gameObject.tag;

        switch (_tag)
        {
            case "Player1":
                //add points to player 1
                gameObject.SetActive(false);
                break;
            case "Player2":
                //add points to player 2
                gameObject.SetActive(false);
                break;
            case "Player3":
                //add points to player 3
                gameObject.SetActive(false);
                break;
            case "Player4":
                //add points to player 4
                gameObject.SetActive(false);
                break;
            case "PlayerProjectile":
                gameObject.SetActive(false);
                break;
        }
    }
}
