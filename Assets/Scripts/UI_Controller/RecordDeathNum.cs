using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordDeathNum : MonoBehaviour
{
    public Text NumOfDeaths;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NumOfDeaths.text = MovementInput.death.ToString();
    }
}
