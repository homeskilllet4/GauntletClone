using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBullet : MonoBehaviour
{
    public GameObject[] players = new GameObject[4];
    private float[] distance = new float[4];
    private int closestPlayer = 0;

    public float speed = 2.0f;
    private float _damage;

    float minDistance;

    private void OnEnable()
    {
        TrackPlayer();
        StartCoroutine(DisableThyself());

        Vector3 playerPos = new Vector3(players[closestPlayer].transform.position.x, players[closestPlayer].transform.position.y, players[closestPlayer].transform.position.z);
        transform.LookAt(playerPos);

        _damage = 10;

        minDistance = float.MaxValue;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        for (int i = 0; i < players.Length; i++)
        {
            float thisDistance = Vector3.Distance(transform.position, players[i].transform.position);

            distance[i] = thisDistance;

            if (thisDistance < minDistance)
            {
                minDistance = thisDistance;
                closestPlayer = i;
                Debug.Log(closestPlayer);
            }
        }
    }

    private void TrackPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 30);
        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    if(players[0] == null)
                        players[0] = hitCollider.gameObject;
                    else if (players[1] == null)
                        players[1] = hitCollider.gameObject;
                    else if (players[2] == null)
                        players[2] = hitCollider.gameObject;
                    else if (players[3] == null)
                        players[3] = hitCollider.gameObject;
                }
            }
        }
    }

    IEnumerator DisableThyself()
    {
        yield return new WaitForSeconds(6);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //deal damage to the player
        }
        if (other.CompareTag("Blockade") || other.CompareTag("Generator"))
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
