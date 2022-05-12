using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    void Update()
    {
        //move forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
        
    }

}
