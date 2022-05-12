using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private List<GameObject> players;

    public List<CharClass> classes;
    
    public GameObject playerPref;
    public Vector3 spawnPoint;

    public int playerCount;
    public int controllerCount;


    void Start()
    {
        //setting spawn to Vector3.zero
        spawnPoint = Vector3.zero;

        players = new List<GameObject>();

        //Get inititial controller count (searching for xbox controllers for now)
        playerCount = 0;
        RefreshControllers();
    }


    public void RefreshControllers()
    {
        controllerCount = 0;
        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            if (InputSystem.devices[i].displayName == "Xbox Controller" || InputSystem.devices[i].displayName == "Wireless Controller")
                controllerCount++;
        }
    }


   
    public void OnPlayerJoined(int index)
    {
        Debug.Log("Attempting to add player");

        //Check for update controller count
        RefreshControllers();
        if(playerCount < controllerCount)
        {
            Debug.Log("Player added");
            playerCount++;

            spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;

            GameObject playerInstance = Instantiate(playerPref, spawnPoint, Quaternion.identity);
            playerInstance.name = "Player" + playerCount;
            CharClass tempCharClass = classes[index];
            if(playerInstance.GetComponent<PlayerClass>() == null)
                playerInstance.AddComponent<PlayerClass>();
            playerInstance.GetComponent<PlayerClass>().InitializePlayer(tempCharClass);
            players.Add(playerInstance);
        }
        else
        {
            Debug.Log("Not enough valid controllers - Try Plugging in another device");
        }



    }
}
