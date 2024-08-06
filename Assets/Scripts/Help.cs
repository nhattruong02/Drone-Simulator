using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Help : MonoBehaviour
{
    public GameObject gameObject;
    private GameStage gameStage;
    private Scene currentScene;
    public Text finishText;
    private string check = "finished";
    void OnTriggerEnter(Collider other)
    {
        gameStage = new GameStage();
        currentScene = SceneManager.GetActiveScene();
        if (other.gameObject.layer != null)
        {
            if (gameObject.CompareTag("HelpVideo")) {
                if (!PlayerPrefs.GetString($"finished{currentScene.buildIndex}").Equals(check))
                {
                    finishText.text = "Bạn đã hoàn thành màn chơi và nhận được 1 sao";
                    PlayerPrefs.SetString($"finished{currentScene.buildIndex}",check);
                    PlayerPrefs.SetInt($"textStar", PlayerPrefs.GetInt("textStar") + 1);
                    gameStage.CompleteLevel(currentScene.buildIndex + 1);
                }
                else
                {
                    finishText.text = "Bạn đã hoàn thành màn chơi";

                }
            }
            gameObject.SetActive(true);
        }
    }
}
