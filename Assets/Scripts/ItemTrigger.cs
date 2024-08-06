using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject Item;
    public Logic logic;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != null)
        {
            logic.StartFadeTextIn("Hãy đến điểm màu đỏ");
            gameObject.SetActive(true);
            Item.SetActive(false);
        }
    }
}
