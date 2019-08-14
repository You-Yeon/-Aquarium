using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle : MonoBehaviour
{
    private float rotationSpeed; // 물병 회전 속도

    private void Start()
    {
        rotationSpeed = 60f; // 물병 회전 속도
    }

    private void Update()
    {
        transform.Rotate(0f, -rotationSpeed * Time.deltaTime/4, rotationSpeed * Time.deltaTime); // 회전
    }
}
