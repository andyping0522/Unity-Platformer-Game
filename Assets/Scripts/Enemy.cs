using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int damage;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        // playerHealth = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("trigger");
            // playerHealth.HurtPlayer(damage);
            if (!other.GetComponent<MovementInput>().getDeathFlag()) {
                other.GetComponent<MovementInput>().HurtPlayer(damage);
            }
            
        }
        // else{ Debug.Log("not player");}
    }
}
