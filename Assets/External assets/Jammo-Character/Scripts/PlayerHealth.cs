using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    // Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("die");
            // Destroy(gameObject);
            // Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            // _animator.SetTrigger("dead");
        }
    }
}
