using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    // private  BoxCollider plat;
    // private float delayTime = 2.0;
    // Start is called before the first frame update
    private bool collision;
    private int rotateDirection = 3;
    private float prevAmount;
    public float destroyTime;
    public Renderer rend;
    public float burnSpeed;

    void Start()
    {
        this.collision = false;
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("DissolveShader");
        prevAmount = rend.material.GetFloat("_Amount");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collision)
        {
            transform.Rotate(0,0, rotateDirection);
            rotateDirection = -rotateDirection;
            float amount = prevAmount + burnSpeed;
            prevAmount = amount;
            rend.material.SetFloat("_Amount", amount);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            collision = true;
            Destroy(gameObject, destroyTime);
        }
    }



}
