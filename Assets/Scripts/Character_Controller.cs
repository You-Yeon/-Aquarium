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
        Move();
    }

    void Move ()
    {

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) // ↖
        {

            anim.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Horizontal", -0.5f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) // ↗
        {
            anim.SetFloat("Horizontal", 0.8f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) // ↙
        {

            anim.SetFloat("Vertical", -0.1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Horizontal", -1f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) // ↘
        {

            anim.SetFloat("Vertical", -1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Horizontal", -0.25f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W)) // ↑
        {

            anim.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Horizontal", 0.5f, 0.1f, Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S)) // ↓
        {

            anim.SetFloat("Vertical", -0.85f, 0.1f, Time.deltaTime);
            anim.SetFloat("Horizontal", -1f, 0.1f, Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.A)) // ←
        {
            anim.SetFloat("Vertical", 0.66f, 0.1f, Time.deltaTime);
            anim.SetFloat("Horizontal", -1f, 0.1f, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)) // →
        {

            anim.SetFloat("Vertical", -1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Horizontal", 0.66f, 0.1f, Time.deltaTime);
        }
        else // *
        {
            anim.SetFloat("Horizontal", 0f, 0.1f, Time.deltaTime);
            anim.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
        }

    }
}
