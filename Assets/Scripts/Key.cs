using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        var _tag = other.gameObject.tag;

        switch (_tag)
        {
            case "Player1":
                GameManager.instance.AddKey(1);
                gameObject.SetActive(false);
                break;
            case "Player2":
                GameManager.instance.AddKey(2);
                gameObject.SetActive(false);
                break;
            case "Player3":
                GameManager.instance.AddKey(3);
                gameObject.SetActive(false);
                break;
            case "Player4":
                GameManager.instance.AddKey(4);
                gameObject.SetActive(false);
                break;
        }
    }
}
