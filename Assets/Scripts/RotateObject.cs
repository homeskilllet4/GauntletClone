using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateAngle;

    private void FixedUpdate()
    {
        transform.Rotate(0, rotateAngle, 0);
    }
}
