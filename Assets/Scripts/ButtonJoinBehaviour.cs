using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;
public class ButtonJoinBehaviour : MonoBehaviour
{
    public void JoinWarrior()
    {
        PlayerManager.instance.OnPlayerJoined(0);
    }
    public void JoinValkeryrie()
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
}
