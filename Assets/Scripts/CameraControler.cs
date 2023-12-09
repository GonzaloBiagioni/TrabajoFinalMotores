using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public static CameraControler instance;

    public Transform target;

    private float startFOV, targetFOV;

    public float zoomSpeed = 1f;

    public Camera theCam;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startFOV = theCam.fieldOfView;
        targetFOV = startFOV;
    }

    void Update()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView,targetFOV, zoomSpeed * Time.deltaTime);
    }

    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom;
    }
    public void ZoomOut()
    {
        targetFOV = startFOV;
    }    
}
