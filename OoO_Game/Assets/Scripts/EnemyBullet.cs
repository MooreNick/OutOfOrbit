using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyBullet();
            Debug.Log("Injured Player");
        }
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
