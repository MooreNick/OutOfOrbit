using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerSpeed = 10f;
    float h, v;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(transform.position.x + (h * playerSpeed),
            transform.position.y + (v * playerSpeed));
    }
}
