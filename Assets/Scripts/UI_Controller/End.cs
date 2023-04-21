using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject EndUI;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0.0f;
        EndUI.SetActive(true);
        Score.score = 0;
    }
}
