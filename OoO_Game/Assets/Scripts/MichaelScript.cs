using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelScript : MonoBehaviour
{
    public Transform playerTransform;
    private float rotationOffset = 90f;

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += rotationOffset;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }
}
