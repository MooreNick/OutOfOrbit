using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamController : MonoBehaviour
{
    private Transform playerFollowTarget;

    private CinemachineVirtualCamera virtualCam;

    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();

        playerFollowTarget = GameObject.FindGameObjectWithTag("Player").transform;

        if(playerFollowTarget != null)
        {
            virtualCam.Follow = playerFollowTarget;
            virtualCam.LookAt = playerFollowTarget;
        }
    }

}
