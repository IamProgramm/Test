using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public void OnCollisionEnter(Collision collision)
    {
        KhtPool.ReturnObject(gameObject);
    }
}
