using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {

    public ProjectileController projectilePrefab;
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
            ProjectileController p = Instantiate<ProjectileController>(projectilePrefab);
            p.transform.position = this.transform.position;
            p.velocity = (playerTransform.position - this.transform.position).normalized * speed;
            p.destroyExplosionPrefab = destroyExplosionPrefab;
        }
	}
}
