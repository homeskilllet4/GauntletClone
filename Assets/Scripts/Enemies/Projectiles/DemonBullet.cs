using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBullet : MonoBehaviour
{
    public float speed;
    private float _damage;

    private void OnEnable()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 10);

        _damage = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //deal damage to the player
        }
        if (other.CompareTag("Blockade"))
        {
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Demon"))
        {
            other.GetComponent<Demon>().hitPoints--;
            other.GetComponent<Demon>().CheckLives();
        }
    }
}
