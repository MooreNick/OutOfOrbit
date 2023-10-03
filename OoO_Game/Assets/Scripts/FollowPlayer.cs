using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if(player != null)
        {
            Vector3 cameraPos = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = cameraPos;
        }
    }
}
