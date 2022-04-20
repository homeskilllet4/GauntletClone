using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int rank; //what rank of enemies
    public Mesh rank1, rank2, rank3; //model for first second and third prefabs
    public Material life1, life2, life3;
    
    public float spawnCD = 2.0f; //how often can enemies spawn
    private Vector3 _spawnLoc; //where will the enemy spawn
    public string enemyType; //what type of enemy (tag)

    private int _hitPoints;
    private int _points = 10;

    private Material _ogMat;
    public Material redMat; //mat to change color to when hit

    private void Start()
    {
        _hitPoints = rank;
        _spawnLoc = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        StartCoroutine(Spawn());
        CheckLives();

        MeshFilter meshF = GetComponent<MeshFilter>();
        MeshCollider meshC = GetComponent<MeshCollider>();
        switch (rank)
        {
            case 1:
                meshF.mesh = rank1;
                meshC.sharedMesh = rank1;
                break;
            case 2:
                meshF.mesh = rank2;
                meshC.sharedMesh = rank2;
                break;
            case 3:
                meshF.mesh = rank3;
                meshC.sharedMesh = rank3;
                break;
        }
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

        CheckLives();
    }

    private void CheckLives()
    {
        Renderer mat = GetComponent<Renderer>();

        if (_hitPoints <= 0)
            gameObject.SetActive(false);

        switch (_hitPoints)
        {
            case 1:
                mat.material = life1;
                break;
            case 2:
                mat.material = life2;
                break;
            case 3:
                mat.material = life3;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DemonProjectile") || other.CompareTag("PlayerProjectile"))
        {
            StartCoroutine(GetHit());
            _hitPoints--;
            //CheckLives();
            //add points for killing generator
        }
    }
}
