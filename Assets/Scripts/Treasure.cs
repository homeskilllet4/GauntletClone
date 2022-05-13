using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public int points; //how many points are awarded
    public AudioClip treasureShot;
    private string _tag;

    private void OnTriggerEnter(Collider other)
    {
        _tag = other.gameObject.tag;

        switch (_tag)
        {
            case "Player1":
                GameManager.instance.AddPoints(points, 1);
                gameObject.SetActive(false);
                break;
            case "Player2":
                GameManager.instance.AddPoints(points, 2);
                gameObject.SetActive(false);
                break;
            case "Player3":
                GameManager.instance.AddPoints(points, 3);
                gameObject.SetActive(false);
                break;
            case "Player4":
                GameManager.instance.AddPoints(points, 4);
                gameObject.SetActive(false);
                break;
            case "Player1Projectile":
                //gameObject.SetActive(false);
                GameManager.instance.GetComponent<AudioSource>().clip = treasureShot;
                GameManager.instance.GetComponent<AudioSource>().Play();
                break;
            case "Player2Projectile":
                //gameObject.SetActive(false);
                GameManager.instance.GetComponent<AudioSource>().clip = treasureShot;
                GameManager.instance.GetComponent<AudioSource>().Play();
                break;
            case "Player3Projectile":
                //gameObject.SetActive(false);
                GameManager.instance.GetComponent<AudioSource>().clip = treasureShot;
                GameManager.instance.GetComponent<AudioSource>().Play();
                break;
            case "Player4Projectile":
                //gameObject.SetActive(false);
                GameManager.instance.GetComponent<AudioSource>().clip = treasureShot;
                GameManager.instance.GetComponent<AudioSource>().Play();
                break;
        }
    }
}
