using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

}
