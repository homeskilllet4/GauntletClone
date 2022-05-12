using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //private variable used to spawn player once scene is loaded for first time
    private int startPlayer;



    [SerializeField]
    public List<PlayerClass> players;
    public List<int> playerScores;
    public List<int> playerKeys;
    public List<int> playerPotions;

    //Publicly assigned text objects for each individual player class.
    [Tooltip("Player UI elements")]
    public Text WarriorScore;
    public Text WarriorHealth;
    public Text ValkyrieScore;
    public Text ValkyrieHealth;
    public Text WizardScore;
    public Text WizardHealth;
    public Text ElfScore;
    public Text ElfHealth;


    //initialize lists and start UI update cycle
    private void Start()
    {
        players = new List<PlayerClass>();
        playerScores = new List<int>(4);
        playerKeys = new List<int>(4);
        playerPotions = new List<int>(4);
        StartCoroutine(UIRefresh());
    }

    public void UpdateUI()
    {
        foreach(PlayerClass player in players)
        {
            string name = player.charClass.className;
            Debug.Log("Updating UI");
            switch (name)
            {
                case "Warrior":
                    WarriorHealth.text = player.health.ToString();
                    break;
                case "Valkyrie":
                    ValkyrieHealth.text = player.health.ToString();
                    break;
                case "Wizard":
                    WizardHealth.text = player.health.ToString();
                    break;
                case "Elf":
                    ElfHealth.text = player.health.ToString();
                    break;
            }
        }
    }

    public void AddPoints(int pointsToAdd, int playerNum)
    {
        Debug.Log(playerNum);
       
        //playerScores[playerNum - 1] += pointsToAdd;
    }

    public void AddPlayer(PlayerClass player)
    {
        players.Add(player);
    }


    public void StartGame(int playerID)
    {
        startPlayer = playerID;
        SceneManager.LoadSceneAsync(1);
        StartCoroutine(PlayerStart(playerID));
    }


    IEnumerator PlayerStart(int playerID)
    {
        yield return new WaitForSeconds(.15f);
        PlayerManager.instance.OnPlayerJoined(playerID);
        StopCoroutine("PlayerStart");
    }

    IEnumerator UIRefresh()
    {
        yield return new WaitForSeconds(.2f);
        UpdateUI();
    }
}
