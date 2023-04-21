using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float xmin;
    public float xmax;
    public float ymin;

    public float ymax;
    public float speed;
    
    private float direction = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (xmin != xmax){
            this.transform.localPosition += new Vector3(1,0,0) * Time.deltaTime * speed * direction;
            this.transform.Rotate(Time.deltaTime * speed * 33, 0,0);

            
            if (this.transform.localPosition.x > xmax){
                direction = -1;
            }
            else if (this.transform.localPosition.x < xmin){
                direction = 1;
            }
        }
        if (ymin != ymax){
            this.transform.localPosition += new Vector3(0,1,0) * Time.deltaTime * speed * direction;
            this.transform.Rotate(Time.deltaTime * speed * 33, 0,0);
            if (this.transform.localPosition.y > ymax){
                direction = -1;
            }
            else if (this.transform.localPosition.y < ymin){
                direction = 1;
            }
        }
    }
        
    }
