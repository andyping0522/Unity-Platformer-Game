using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedController : MonoBehaviour
{
    private bool paused = false;
    public GameObject PauseUI;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
        
        // IsPaused = false;
    }

    public void Pause()
    {
        if (paused)
        {
            Debug.Log("start again");
            Continue();
        }
        else{
            Debug.Log("pause");
            PauseUI.SetActive(true);
            Time.timeScale = 0.0f;
            paused = true;
            
        }
        // PauseUI.SetActive(true);
        // Time.timeScale = 0.0f;
        // IsPaused = true;
    }

    public void Home()
    {
        paused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        paused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
