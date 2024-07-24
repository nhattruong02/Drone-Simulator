using DroneController.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    public float timeLeft;
    public bool timerOn = false;
    public Text timerTxt;
    public Text missionText;
    int durability = 100;
    public Text textDurability;
    int progress = 0;
    public Text textProgress;
    public GameObject tryAgain;
    public GameObject finish;
    private string mission;
    private GameStage gameStage;
    private Scene currentScene;
    public Text finishText;
    public DroneMovementScript moveDrone;
    private void Start()
    {
        gameStage = new GameStage();
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "TrainingScreen")
        {
            timerOn = true;
        }
        mission = "Đến điểm màu xanh để lấy vật phẩm";
        StartCoroutine(FadeTextIn(mission));
    }
    private void Update()
    {

        if(timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn= false;
                tryAgain.SetActive(true);
            }
        }
    }
    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
    public void ReducedDurability()
    {
        if (durability > 0)
        {

            durability -= 1;
            textDurability.text = durability.ToString();
            Check();
        }
    }

    public void ThroughSuccess()
    {
        progress += 1;
        textProgress.text = progress.ToString()+"/5";
        Check();
    }
    public void Check()
    {

        if (progress == 5)
        {
            Debug.Log(PlayerPrefs.GetInt($"unlocked{currentScene.buildIndex}Level"));
            if (PlayerPrefs.GetInt($"unlocked{currentScene.buildIndex}Level") != currentScene.buildIndex){
                finishText.text = "Bạn đã hoàn thành màn chơi và nhận được 1 sao";
                finish.SetActive(true);
                gameStage.CompleteLevel(currentScene.buildIndex + 1);
                gameStage.CompleteLevel(currentScene.buildIndex);
                PlayerPrefs.SetInt($"textStar", PlayerPrefs.GetInt("textStar") + 1);
                DisableMoveDrone();
            }
            else
            {
                finishText.text = "Bạn đã hoàn thành màn chơi";
                finish.SetActive(true);
                DisableMoveDrone();
            }
           
        }
        if(durability == 0)
        {
            timerOn = false;
            tryAgain.SetActive(true);
            DisableMoveDrone();
        }
    }
    public void StartFadeTextIn(string text)
    {
        StartCoroutine(FadeTextIn(text));
    }
    public IEnumerator FadeTextIn(string text)
    {
        missionText.text = text;
        Color originalColor = missionText.color;
        float currentTime = 0.0f;
        float fadeOutDuration = 3.0f;

        while (currentTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1, 0, currentTime / fadeOutDuration);
            missionText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        missionText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // Đảm bảo văn bản biến mất hoàn toàn khi hoàn thành
    }


    public void DisableMoveDrone()
    {
        moveDrone.sideMovementAmount = 0;
        moveDrone.movementForwardSpeed = 0;
    }
}
