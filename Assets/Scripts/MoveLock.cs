using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLock : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.MoveLock++;
    }

    private void OnCollisionExit(Collision collision)
    {
        GameManager.MoveLock--;
    }

}
