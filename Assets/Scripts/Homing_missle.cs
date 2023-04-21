using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_missle : MonoBehaviour
{
    public string tagToDamage;
    public Vector3 velocity;
    public GameObject destroyExplosionPrefab;
    private Transform playerTransform;
    private Rigidbody rigid;
    public float speed;
    private float time = 10;
    // Start is called before the first frame update
    void Start()
    {
         playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
         rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
        // the below update code is sourced by https://www.youtube.com/watch?v=0v_H3oOR0aU at 6:05; 
         velocity = (playerTransform.position - this.transform.position).normalized;
         float rotation = Vector3.Cross(velocity, transform.up).z;
         rigid.angularVelocity = new Vector3 (0,0,-rotation * speed);
         rigid.velocity = transform.up * speed;
         time = time - Time.deltaTime;
         if(time <= 0){
            Destroy(this.gameObject);
            Instantiate(destroyExplosionPrefab,transform.position,transform.rotation);
         }



    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == tagToDamage)
        {
            Debug.Log("hit player");

            // Destroy self
            Destroy(this.gameObject);
            Instantiate(destroyExplosionPrefab,transform.position,transform.rotation);

        }
        else {
            Destroy(this.gameObject);
            Instantiate(destroyExplosionPrefab,transform.position,transform.rotation);
        }
    }
}
