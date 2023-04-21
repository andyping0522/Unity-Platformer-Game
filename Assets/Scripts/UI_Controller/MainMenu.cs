using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject levelPanel;
    private Button thisButton;
    private string ButtonText;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("level1");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LevelPanel()
    {
        levelPanel.SetActive(true);
    }

    public void level()
    {
        ButtonText = EventSystem.current.currentSelectedGameObject.name;
        // ButtonText = this.GetComponentsInChildren<Text>().ToString();
        // var level = GetComponent<Text>();
        Debug.Log("?? " + ButtonText);
        int level = int.Parse(ButtonText) - 1;
        SceneManager.LoadScene(int.Parse(ButtonText));
    }
    public void close()
    {
        levelPanel.SetActive(false);
    }

    
}
