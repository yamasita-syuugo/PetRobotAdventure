using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Manager_Camera manager_Camera;

    GameObject target;

    public float speedDelay = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        manager_Camera = GameObject.FindWithTag("Manager").GetComponent<Manager_Camera>();

        target = GameObject.FindWithTag("Player");

        speedDelay = manager_Camera.GetCameraMoveSpeedDeray();

        camera = GetComponent<Camera>();

        size = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        SizeZoom();

        if(target == null) { target = GameObject.FindWithTag("Player");return; }

        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);

        move.x = (target.transform.position.x - transform.position.x) / manager_Camera.GetCameraMoveSpeedDeray() * Time.deltaTime;
        move.y = (target.transform.position.y - transform.position.y) / manager_Camera.GetCameraMoveSpeedDeray() * Time.deltaTime;

        transform.position += move;
    }

    Camera camera;
    [SerializeField]
    float zoomSpeed = 1.0f;
    [SerializeField]
    float maxSize = 6;
    [SerializeField]
    float minSize = 2;
    float size;
    void SizeZoom()
    {
        if(Input.mouseScrollDelta.y != 0) size += Input.mouseScrollDelta.y * zoomSpeed;

        if(size > maxSize) size = maxSize;
        else if(size < minSize) size = minSize;

        camera.orthographicSize += (size - camera.orthographicSize) / 1.1f * Time.deltaTime;
    }
}
