using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 8;
    void Update()
    {
        if (transform.position.y > 8) Destroy(gameObject);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
