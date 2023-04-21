using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    private IEnumerator WaitForSceneLoad()
	{
		yield return new WaitForSeconds(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Score.score = 0;
	}
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("change scene");
        StartCoroutine(WaitForSceneLoad());
    }
}
