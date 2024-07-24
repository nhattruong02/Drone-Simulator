using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStage : MonoBehaviour
{
    public GameObject lockImage;
    public int index;
    private void Start()
    {
        if (index == PlayerPrefs.GetInt($"unlocked{index}Level"))
        {
            lockImage.SetActive(false);
        }
        PlayerPrefs.SetInt($"unlocked{2}Level", 2);

    }

    public void CompleteLevel(int levelToUnlock)
    {
        if (levelToUnlock > PlayerPrefs.GetInt($"unlocked{levelToUnlock}Level"))
        {
            PlayerPrefs.SetInt($"unlocked{levelToUnlock}Level", levelToUnlock);
        }
    }
    public void LoadLevel(int index)
    {
        if (index == PlayerPrefs.GetInt($"unlocked{index}Level") || index == 2 || index == 11)
        {
            SceneManager.LoadScene(index);
        }
    }
}
