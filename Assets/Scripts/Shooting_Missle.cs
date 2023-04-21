using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Missle : MonoBehaviour
{
    // Start is called before the first frame update
    public Homing_missle MisslePrefab;
    public GameObject destroyExplosionPrefab;
    // public PlayerController player;
    private Transform playerTransform;
    public float time;
    public float up;
    public float down;

    public float left;

    public float right;

    public float speed; 

    public float interval;
    void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("shoot", interval, time);

    }
    
    // Update is called once per frame
    void shoot ()
    {
        if (playerTransform.position.x > this.transform.position.x - left && playerTransform.position.x < this.transform.position.x + right && playerTransform.position.y > this.transform.position.y -down&& playerTransform.position.y < this.transform.position.y + up)
        {
            Homing_missle p = Instantiate<Homing_missle>(MisslePrefab);
            p.transform.position = this.transform.position;
            p.transform.rotation = Quaternion.identity;
            //p.transform.LookAt(playerTransform, new Vector3(0,0,1));
            p.velocity = (playerTransform.position - this.transform.position).normalized * speed;
            p.speed = speed;
            p.destroyExplosionPrefab = destroyExplosionPrefab;
        }
	}
}
