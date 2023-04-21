using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_door : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            door.SetActive(false);
        }
    }
}
