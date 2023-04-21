using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordKeyNumber : MonoBehaviour
{
    public Text keyText;
    public int keyNum;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = Score.score.ToString() + "/" + keyNum.ToString();
    }
}
