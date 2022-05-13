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
    public int spawnLimit; //max number of enemies that can be spawned
    private int _enemiesSpawned; //number of enemies spawned

    private int _hitPoints; //how many hit points this generator has
    private int _points = 10; //point value given for destroying this generator
    private int playerForPoints;

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
                //meshC.sharedMesh = rank1;
                break;
            case 2:
                meshF.mesh = rank2;
                //meshC.sharedMesh = rank2;
                break;
            case 3:
                meshF.mesh = rank3;
                //meshC.sharedMesh = rank3;
                break;
        }
    }



    //grab enemies from object pool
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        //infinite loop
        while (_enemiesSpawned < spawnLimit)
        {
            //wait for spawn cooldown
            yield return new WaitForSeconds(spawnCD);

            //get the enemy game object based on tag that was input in inspector
            GameObject enemy = ObjectPooler.Instance.GetPooledObject(enemyType);

            //if enemy exists, pull it to spawn location, set their rank, then activate them.
            if (enemy != null);
            {
                enemy.transform.position = _spawnLoc;
                enemy.GetComponent<Enemy>().rank = rank;
                enemy.SetActive(true);
                _enemiesSpawned++;
            }
        }
    }



    //change material of the generator to red when hit for a brief period
    IEnumerator GetHit()
    {
        _ogMat = GetComponent<Renderer>().material;
        //GetComponent<Renderer>().material = redMat;

        yield return new WaitForSeconds(0.0f);

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
        switch (other.tag)
        {
            //player 1 gives shoot points
            case "Player1Projectile":
                playerForPoints = 1;
                _hitPoints--;
                CheckLives();
                if (_hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_points, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            //demon projectile gives shoot points
            case "DemonProjectile":
                _hitPoints--;
                CheckLives();
                if (_hitPoints <= 0)
                    gameObject.SetActive(false);
                break;
            //player 2 gives shoot points
            case "Player2Projectile":
                playerForPoints = 2;
                _hitPoints--;
                CheckLives();
                if (_hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_points, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            //player 3 does magic damage
            case "Player3Projectile":
                playerForPoints = 3;
                _hitPoints--;
                CheckLives();
                if (_hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_points, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            //player 4 does shoot damage
            case "Player4Projectile":
                playerForPoints = 4;
                _hitPoints--;
                CheckLives();
                if (_hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_points, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
            case "Potion":
                //gotta track the player
                _hitPoints = 0;
                GameManager.instance.AddPoints(_points, playerForPoints);
                gameObject.SetActive(false);
                break;
            //player 1 fight damage
            case "FightWeapon":
                playerForPoints = 1;
                _hitPoints--;
                CheckLives();
                if (_hitPoints <= 0)
                {
                    GameManager.instance.AddPoints(_points, playerForPoints);
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
