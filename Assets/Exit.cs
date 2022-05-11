using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private int scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (scene == 3)
        {
            scene = 1;
            SceneManager.LoadScene(scene);
        }
        else
            SceneManager.LoadScene(scene++);
    }
}
