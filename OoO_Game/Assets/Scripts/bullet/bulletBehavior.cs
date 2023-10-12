using System.Collections;

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform followTransform;
    private Quaternion rot;
    private float timeout = 0.0f;

    private void Awake()
    {
        rot = followTransform.rotation;
    }

    void FixedUpdate()
    {
        timeout += 0.1f;
        while (timeout < 3.0f)
            transform.Translate((transform.up * speed * Time.deltaTime));
        
    }
}