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

    private int _hitPoints; //how many hit points this generator has
    private int _points = 10; //point value given for destroying this generator

    private Material _ogMat; //original material
    public Material redMat; //mat to change color to when hit

    private void Start()
    {
        //set the hit points to the rank set in inspector, set the spawn location of enemies
        _hitPoints = rank;
        _spawnLoc = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //start spawning enemies then change the color of the generator based on rank
        StartCoroutine(Spawn());
        CheckLives();

        //get mesh filter and collider
        MeshFilter meshF = GetComponent<MeshFilter>();
        MeshCollider meshC = GetComponent<MeshCollider>();

        //for each rank, change the mesh and the mesh collider
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

    //grab enemies from object pool
    private IEnumerator Spawn()
    {
        //infinite loop
        while (true)
        {
            //wait for spawn cooldown
            yield return new WaitForSeconds(spawnCD);

            //get the enemy game object based on tag that was input in inspector
            GameObject enemy = ObjectPooler.Instance.GetPooledObject(enemyType);

            //if enemy exists, pull it to spawn location, set their rank, then activate them.
            if (enemy != null)
            {
                enemy.transform.position = _spawnLoc;
                enemy.GetComponent<Enemy>().rank = rank;
                enemy.SetActive(true);
                Debug.Log("Spawned " + enemyType);
            }
        }
    }

    //change material of the generator to red when hit for a brief period
    IEnumerator GetHit()
    {
        _ogMat = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = redMat;

        yield return new WaitForSeconds(1.0f);

        CheckLives();
    }

    //change material based on hitpoints
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
        //if the player or demon hits the generator with a projectile, take away a hit point then change the color
        if (other.CompareTag("DemonProjectile") || other.CompareTag("PlayerProjectile"))
        {
            StartCoroutine(GetHit());
            _hitPoints--;
            //CheckLives();
            //add points for killing generator
        }
    }
}
