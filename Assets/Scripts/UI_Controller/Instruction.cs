using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Instruction : MonoBehaviour
{
    private Transform playerTransform;
    public Transform leftPos;
    public Transform rightPos;
    public GameObject canvas;

    //Fader
    private bool mFaded = false;
    public float Duration = 0.4f;
    private CanvasGroup canvGroup;
    // Start is called before the first frame update

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter/Duration);
            yield return null;
        }
    }

    void Start()
    {
        // canvas.SetActive(true);
        canvGroup = GetComponent<CanvasGroup>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("Update", 0.15f, 0.15f);
        // Update();
    }

    // Update is called once per frame
    void Update()
    {
        //Canvas disappers
        if (playerTransform.position.x < leftPos.position.x || playerTransform.position.x > rightPos.position.x)
        {
            // canvas.SetActive(false);
            // Debug.Log("false");
            // isShowing = !isShowing;
           
            // Update().enabled = true;
            // canvas.enabled = false;
            //没有faded的时候
            if (!mFaded)
            {
                StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));
                mFaded = !mFaded;
            }
            
        }
        else
        {
            // Debug.Log("true");
            // canvas.SetActive(true);
            if (mFaded)
            {
                StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));
                mFaded = !mFaded;
            }

            
        }
    }
}
