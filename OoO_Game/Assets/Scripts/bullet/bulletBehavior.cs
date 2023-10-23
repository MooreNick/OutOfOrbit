using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform followTransform;
    [SerializeField]
    private float timer = 180.0f;
    private Vector2 shipDirection;

    private void Start()
    {
        transform.Rotate(followTransform.rotation.eulerAngles);
        shipDirection = followTransform.up;
    }

    private void Update()
    {
        MoveBullet();
        timer -= 0.1f;
        if (timer <= 0.0f)
            DestroyBullet();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
            DestroyBullet();
    }

    void MoveBullet()
    {
        transform.Translate(shipDirection * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}