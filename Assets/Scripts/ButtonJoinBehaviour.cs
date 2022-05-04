using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
public class ButtonJoinBehaviour : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void JoinWarrior()
    {
        PlayerManager.instance.OnPlayerJoined(0);
    }


    public void JoinValkyrie()
    {
        PlayerManager.instance.OnPlayerJoined(1);
    }


    public void JoinWizard()
    {
        PlayerManager.instance.OnPlayerJoined(2);
    }



    public void JoinElf()
    {
        PlayerManager.instance.OnPlayerJoined(3);
    }

    public void StartWarrior()
    {
        GameManager.instance.StartGame(0);

    }

    public void StartValkyrie()
    {
        GameManager.instance.StartGame(1);
    }

    public void StartWizard()
    {
        GameManager.instance.StartGame(2);

    }

    public void StartElf()
    {
        GameManager.instance.StartGame(3);

    }

}
