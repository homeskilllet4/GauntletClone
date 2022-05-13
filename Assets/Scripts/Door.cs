using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            if (GameManager.instance.UseKey(1))
            {
                Destroy(gameObject);
            }
        }
        else if (other.tag == "Player2")
        {
            if (GameManager.instance.UseKey(2))
            {
                Destroy(gameObject);
            }
        }
        else if(other.tag == "Player3")
        {
            if (GameManager.instance.UseKey(3))
            {
                Destroy(gameObject);
            }
        }
        else if (other.tag == "Player4")
        {
            if (GameManager.instance.UseKey(4))
            {
                Destroy(gameObject);
            }
        }
    }
}
