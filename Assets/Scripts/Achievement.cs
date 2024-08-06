using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public Text textStar;
    private void Start()
    {
        textStar.text = $"Bạn đã nhận được {PlayerPrefs.GetInt("textStar")} sao".ToString();
    }
}
