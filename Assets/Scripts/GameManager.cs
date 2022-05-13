using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //private variable used to spawn player once scene is loaded for first time
    private int startPlayer;
    private bool isWarrior;
    private bool isValkyrie;
    private bool isWizard;
    private bool isElf;



    public GameObject playerMenu;


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
        StartCoroutine(UIRefresh());
    }

    public void UpdateUI()
    {
        if (isWarrior)
        {
            WarriorHealth.text = players[0].health.ToString();
            WarriorScore.text = playerScores[0].ToString();
        }
        if (isValkyrie)
        {
            ValkyrieHealth.text = players[1].health.ToString();
            ValkyrieScore.text = playerScores[1].ToString();
        }
        if (isWizard)
        {
            WizardHealth.text = players[2].health.ToString();
            WizardScore.text = playerScores[2].ToString();
        }
        if (isElf)
        {
            ElfHealth.text = players[3].health.ToString();
            ElfScore.text = playerScores[3].ToString();
        }
    }

    public void AddPoints(int pointsToAdd, int playerNum)
    {
        playerScores[playerNum - 1] += pointsToAdd;
    }

    public void AddKey(int playerNum)
    {
        playerKeys[playerNum - 1] += 1;
    }

    public void AddPotion(int playerNum)
    {
        playerPotions[playerNum - 1] += 1;
    }


    public bool UseKey(int playerNum)
    {
        if(playerKeys[playerNum - 1] > 0)
        {
            playerKeys[playerNum - 1] -= 1;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void AddPlayer(PlayerClass player)
    {
        switch (player.charClass.className)
        {
            case "Warrior":
                players[0] = player;
                isWarrior = true;
                break;
            case "Valkyrie":
                players[1] = player;
                isValkyrie = true;
                break;
            case "Wizard":
                players[2] = player;
                isWizard = true;
                break;
            case "Elf":
                players[3] = player;
                isElf = true;   
                break;
            default:
                Debug.Log("GameManager:AddPlayer - Adding Player error");
                break;
        }
    }


    public void LosePotion(int player)
    {
        if(playerPotions[player - 1] > 0)
        {
            playerPotions[player - 1] -= 1;
        }
    }



    public void UsePotion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 20);
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

    public IEnumerator UIRefresh()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            UpdateUI();
        }
    }


    public void PopMenu()
    {
        playerMenu.SetActive(true);
    }
}
