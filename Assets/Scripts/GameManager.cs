using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
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

    private void Start()
    {
        players = new List<PlayerClass>();
    }

    public void UpdateUI()
    {
        foreach(PlayerClass player in players)
        {
            string name = player.charClass.className;
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
        playerScores[playerNum] += pointsToAdd;
    }

    public void Addplayer(PlayerClass newPlayer)
    {
        if(players.Count < 4)
            players.Add(newPlayer);
        else
        {
            Debug.Log("Too many players, cannot add to list");
        }
    }



    IEnumerator UIRefresh()
    {
        yield return new WaitForSeconds(.25f);
        UpdateUI();
    }
}
