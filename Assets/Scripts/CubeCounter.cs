using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCounter : MonoBehaviour
{

    public void countCube()
    {
        Debug.Log("Hi ha un cub menys");
        Vector3 scale = transform.localScale;
        scale *= 2.0f;
        transform.localScale = scale;
    }
}
