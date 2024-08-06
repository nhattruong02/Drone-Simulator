using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextStar : MonoBehaviour
{
    public Text textStar;
    private void Start()
    {
        textStar.text = PlayerPrefs.GetInt("textStar").ToString();
    }
}
