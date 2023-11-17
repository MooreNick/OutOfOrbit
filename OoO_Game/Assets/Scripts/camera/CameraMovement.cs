using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Camera cam;

    public Transform npc;

    private Transform player;
    
    private Vector3 targetCamPos;
    private Vector3 camPosOnEnter;
    private bool inCutscene = false;

    public float followFOV = 60;
    public float cutsceneFOV = 30;

    public float zoomSpeed = 0.1f;
    public float panSpeed = 0.1f;

    private void Start()
    {
        //intitialize the player transform for camera follow capability
        player = GameObject.FindGameObjectWithTag("Player").transform;

        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if(player != null)
        {
            if (!inCutscene)
            {
                //follow player
                Vector3 cameraPos = new Vector3(player.position.x, player.position.y, transform.position.z);
                transform.position = cameraPos;
            }
            else //in cutscene
            {
                //adjust fov
                float fovDiff = followFOV - cutsceneFOV;
                if (cam.fieldOfView > cutsceneFOV)
                    cam.fieldOfView -= fovDiff * zoomSpeed;
                //adjust cam position
                Vector3 newCamPos = Vector3.Lerp(transform.position, targetCamPos, panSpeed);
                transform.position = newCamPos;
            }
        }
    }

    public void OnLeaveCutscene(Component sender, object data)
    {
        outOfCutscene();
    }

    public void outOfCutscene()
    {
        inCutscene = false;
        cam.fieldOfView = followFOV;
    }

    public void npcInteractionZoom()
    {
        if (player != null && npc != null) {
            // find position between game objects
            Vector3 between = (player.position + npc.position) / 2;
            targetCamPos = new Vector3(between.x, between.y, -10f);
            camPosOnEnter = transform.position;
            inCutscene = true;
        }
    }
}
