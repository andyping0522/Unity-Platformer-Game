using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_platform : MonoBehaviour
{

    public GameObject player;
    public float xmin;
    public float xmax;
    public float ymin;

    public float ymax;
    public float speed;
    
    private float direction = 1;

    private CharacterController control;
    void Start()
    {
        control = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (xmin != xmax){
            this.transform.position += new Vector3(1,0,0) * Time.deltaTime * speed * direction;
            
            if (this.transform.position.x > xmax){
                direction = -1;
            }
            else if (this.transform.position.x < xmin){
                direction = 1;
            }
        }
        if (ymin != ymax){
            this.transform.position += new Vector3(0,1,0) * Time.deltaTime * speed * direction;
            if (this.transform.position.y > ymax){
                direction = -1;
            }
            else if (this.transform.position.y < ymin){
                direction = 1;
            }
        }
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject == player){
            if(xmax != xmin){
                control.Move(new Vector3(1,0,0) * Time.deltaTime * speed * direction);
            }
            if(ymin != ymax){
                control.Move(new Vector3(0,1,0) * Time.deltaTime * speed * direction);
            }
            
        }
    }



}
