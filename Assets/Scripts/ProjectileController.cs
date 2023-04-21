using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour {

    public Vector3 velocity;
    // public int damageAmount = 50;
    public string tagToDamage;

    public GameObject destroyExplosionPrefab;

    // Update is called once per frame
    void Update () {
        this.transform.Translate(velocity * Time.deltaTime);
	}

    // Handle collisions
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == tagToDamage)
        {
            Debug.Log("hit player");

            // Destroy self
            Destroy(this.gameObject);
            Instantiate(destroyExplosionPrefab,this.transform.position,this.transform.rotation);
        } 
        else {
            Destroy(this.gameObject);
            Instantiate(destroyExplosionPrefab,this.transform.position,this.transform.rotation);
        }
    }
}
