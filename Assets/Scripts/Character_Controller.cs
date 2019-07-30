using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    

    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {

            anim.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S))
        {

            anim.SetFloat("Vertical", -1f, 0.1f, Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.A))
        {

            anim.SetFloat("Horizontal", -1f, 0.1f, Time.deltaTime);
        } 
        else if(Input.GetKey(KeyCode.D))
        {

            anim.SetFloat("Horizontal", 1f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Horizontal", 0f, 0.1f, Time.deltaTime);
            anim.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
        }
    }
}
