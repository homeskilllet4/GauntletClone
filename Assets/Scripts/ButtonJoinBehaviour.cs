using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;
public class ButtonJoinBehaviour : MonoBehaviour
{
    public void JoinWarrior()
    {
        GameManager.instance.OnPlayerJoined(0);
    }
    public void JoinValkyrie()
    {
        GameManager.instance.OnPlayerJoined(1);
    }
    public void JoinWizard()
    {
        GameManager.instance.OnPlayerJoined(2);
    }
    public void JoinElf()
    {
        GameManager.instance.OnPlayerJoined(3);
    }
}
