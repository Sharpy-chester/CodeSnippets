using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRaycast : MonoBehaviour
{
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] float interactDist;
    private static InteractRaycast _instance;
    public static InteractRaycast Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public bool RayHit(out RaycastHit hit)
    {
        return Physics.Raycast(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward), out hit, interactDist); ;
    }
}
