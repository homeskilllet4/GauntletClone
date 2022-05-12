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
    public PlayerClass[] players;
    public int[] playerScores;
    public int[] playerKeys;
    public int[] playerPotions;

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
        players = new PlayerClass[4];
        playerScores = new int[4];
        playerKeys = new int[4];
        playerPotions = new int[4];
        
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
                    WarriorScore.text = playerScores[0].ToString();
                    Debug.Log("Health");
                    break;
                case "Valkyrie":
                    ValkyrieHealth.text = player.health.ToString();
                    WarriorScore.text = playerScores[0].ToString();

                    break;
                case "Wizard":
                    WizardHealth.text = player.health.ToString();
                    WarriorScore.text = playerScores[0].ToString();
                    break;
                case "Elf":
                    ElfHealth.text = player.health.ToString();
                    WarriorScore.text = playerScores[0].ToString();
                    break;
            }
        }
    }

    public void AddPoints(int pointsToAdd, int playerNum)
    {
        //Debug.Log(playerNum);
       
        playerScores[playerNum - 1] += pointsToAdd;
        //Debug.Log(playerScores[playerNum - 1]);
    }

    public void AddPlayer(PlayerClass player)
    {
        switch (player.charClass.className)
        {
            case "Warrior":
                players[0] = player;
                break;
            case "Valkyrie":
                players[1] = player;
                break;
            case "Wizard":
                players[2] = player;
                break;
            case "Elf":
                players[3] = player;
                break;
            default:
                Debug.Log("GameManager:AddPlayer - Adding Player error");
                break;
        }
    }


    public void StartGame(int playerID)
    {
        startPlayer = playerID;
        SceneManager.LoadSceneAsync(1);
        StartCoroutine(PlayerStart(playerID));
    }

    public void StartUI()
    {
        StartCoroutine(UIRefresh());
    }

    IEnumerator PlayerStart(int playerID)
    {
        yield return new WaitForSeconds(.15f);
        PlayerManager.instance.OnPlayerJoined(playerID);
        StopCoroutine("PlayerStart");
    }

    public IEnumerator UIRefresh()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("HERE");
        UpdateUI();
    }
}
