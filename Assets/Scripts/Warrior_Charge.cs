using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Charge : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    public float chargeto;
    public float speed;
    private bool ischarging = false;
    private bool chargeback = false;
    private int direction = 1;

    private float originalx;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalx = this.transform.position.x;
        if (chargeto < originalx){
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Transform playerTrans = player.transform;
        if (direction == 1 && ischarging == false && chargeback == false){
            if (playerTrans.position.x <= chargeto && playerTrans.position.x > originalx
                && playerTrans.position.y < this.transform.position.y + 0.25 
                && playerTrans.position.y > this.transform.position.y - 0.25) {
                ischarging= true;
            }
        }
        else if (direction == -1 && ischarging == false && chargeback == false){
            if (playerTrans.position.x >= chargeto && playerTrans.position.x < originalx
                && playerTrans.position.y < this.transform.position.y + 0.25 
                && playerTrans.position.y > this.transform.position.y - 0.25) {
                ischarging= true;
            }
        }

        if (ischarging){
            if(direction == 1 && this.transform.position.x < chargeto){
                this.transform.localPosition += new Vector3(1,0,0) * Time.deltaTime * speed * direction;
            }
            if(direction == 1 && this.transform.position.x >= chargeto){
                ischarging = false;
                chargeback = true;
                this.transform.Rotate(0,180,0);
            }
            if(direction == -1 && this.transform.position.x > chargeto){
                this.transform.localPosition += new Vector3(1,0,0) * Time.deltaTime * speed * direction;
            }
            if(direction == -1 && this.transform.position.x <= chargeto){
                ischarging = false;
                chargeback = true;
                this.transform.Rotate(0,180,0);
            }
        }

        if (chargeback){
            if(direction == 1 && this.transform.position.x > originalx){
                this.transform.localPosition += new Vector3(1,0,0) * Time.deltaTime * speed * -direction;
            }
            if(direction == 1 && this.transform.position.x <= originalx){
                chargeback = false;
                this.transform.Rotate(0,180,0);
            }
            if(direction == -1 && this.transform.position.x < originalx){
                this.transform.localPosition += new Vector3(1,0,0) * Time.deltaTime * speed * -direction;
            }
            if(direction == -1 && this.transform.position.x >= originalx){
                chargeback = false;
                this.transform.Rotate(0,180,0);
            }
        }

    }
}
