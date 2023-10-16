using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1.5f;
    public Transform followTransform;
    private float timeout = 0.0f;
    private Vector2 shipDirection;

    private void Start()
    {
        transform.Rotate(followTransform.rotation.eulerAngles);
        shipDirection = followTransform.up;
    }

    private void Update()
    {
        MoveBullet();
        timeout += 0.1f;
        DestroyBullet();
    }

    void MoveBullet()
    {
        transform.Translate(shipDirection * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        if (timeout > 60.0f)
        {
            Destroy(this.gameObject);
        }
    }
}