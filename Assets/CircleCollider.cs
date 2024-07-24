using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    public Logic logic;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MeshCollider>() == null)
        {
            gameObject.SetActive(false);
            logic.ThroughSuccess();
        }
    }
}
