using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherScript : MonoBehaviour
{
    public GameObject player;
    public float crushSpeed;
    public float returnSpeed;
    public BoxCollider boxCollider;
    // Start is called before the first frame update
    private float boundry_x1;
    private float boundry_z1;
    private float boundry_x2;
    private float boundry_z2;
    private float y_pos;
    private Vector3 origPos;
    private bool crushing;
    private bool returning;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        getBoundries();
        this.crushing = false;
        boxCollider = this.GetComponent<BoxCollider>();
        origPos = this.transform.position;
        //Debug.Log(boundry_x1 + " " + boundry_x2 + " " + boundry_z1 + " " + boundry_z2);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.transform.position);
        Transform playerTrans = player.transform;
        if (playerTrans.position.x <= boundry_x1 && playerTrans.position.x >= boundry_x2
            && playerTrans.position.z <= boundry_z1 && playerTrans.position.z >= boundry_z2
            && playerTrans.position.y < y_pos && !crushing && !returning) {
            //Debug.Log("crushing");
            crushing = true;
        }
        
        if (crushing) {
            //this.transform.localPosition -= new Vector3(0.0f, Time.deltaTime * crushSpeed, 0.0f)
            this.transform.position += Vector3.down * Time.deltaTime * crushSpeed;
        } else if (returning) {
            this.transform.position = Vector3.Lerp(this.transform.position, origPos,
                    Time.deltaTime * returnSpeed);
            
        } 

        if (origPos.y  - this.transform.position.y <= 0.1f) {
            returning = false;
        }
        
    }

    private void getBoundries()
    {
        this.boundry_x1 = this.transform.position.x + this.transform.localScale.x/2;
        this.boundry_x2 = this.transform.position.x - this.transform.localScale.x/2;
        this.boundry_z1 = this.transform.position.z + this.transform.localScale.z/2;
        this.boundry_z2 = this.transform.position.z - this.transform.localScale.z/2;
        this.y_pos = this.transform.position.y;
    }

    private void OnCollisionEnter(Collision other) {
        
            returning = true;
            crushing = false;
    }
}
