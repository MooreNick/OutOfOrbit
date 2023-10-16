using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1.5f;
    public Transform followTransform;
    private Quaternion rot;
    private float timeout = 0.0f;

    private void Start()
    {
        transform.Rotate(followTransform.rotation.eulerAngles);
    }

    private void Update()
    {
        MoveBullet();
        timeout += 0.1f;
        DestroyBullet();
    }

    void MoveBullet()
    {
        transform.Translate(followTransform.up * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        if (timeout > 20.0f)
        {
            Destroy(this.gameObject);
        }
    }
}