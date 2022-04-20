using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int rank; //what rank of enemies
    
    public float spawnCD = 2.0f; //how often can enemies spawn
    private Vector3 _spawnLoc; //where will the enemy spawn
    public string enemyType; //what type of enemy (tag)

    private int _hitPoints;

    private Material _ogMat;
    public Material redMat; //mat to change color to when hit

    private void Start()
    {
        _hitPoints = rank;
        _spawnLoc = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCD);

            GameObject enemy = ObjectPooler.Instance.GetPooledObject(enemyType);

            if (enemy != null)
            {
                enemy.transform.position = _spawnLoc;
                enemy.GetComponent<Enemy>().rank = rank;
                enemy.SetActive(true);
                Debug.Log("Spawned " + enemyType);
            }
        }
    }

    IEnumerator GetHit()
    {
        _ogMat = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = redMat;

        yield return new WaitForSeconds(1.0f);

        GetComponent<Renderer>().material = _ogMat;
    }

    private void CheckLives()
    {
        if (_hitPoints <= 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DemonProjectile") || other.CompareTag("PlayerProjectile"))
        {
            StartCoroutine(GetHit());
            _hitPoints--;
            CheckLives();
        }
    }
}
