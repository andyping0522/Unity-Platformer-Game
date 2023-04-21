using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Script : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public float xmin;
    public float xmax;

    public float yrange;
    public float speed;

    public float yspeed;
    
    private float direction = 1;
    private float radiance = Mathf.PI / 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (xmin != xmax){
            this.transform.localPosition += new Vector3(1,0,0) * Time.deltaTime * speed * direction;

            
            if (this.transform.localPosition.x > xmax){
                direction = -1;
                this.transform.Rotate(0,180,0);
            }
            else if (this.transform.localPosition.x < xmin){
                direction = 1;
                this.transform.Rotate(0,180,0);
            }
        }
        if (yrange > 0){
            this.transform.localPosition += new Vector3(0,yrange*Mathf.Sin(radiance),0) * Time.deltaTime;
            radiance += Time.deltaTime  * yspeed;
            
        }
    }
}
