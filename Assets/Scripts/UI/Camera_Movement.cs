using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool faster = Input.GetKey(KeyCode.LeftShift);

        int direction_x = right ? 1 : left ? -1 : 0;
        int direction_y = up ? 1 : down ? -1 : 0;

        Vector3 velocity = new Vector3(direction_x, direction_y, 0);
        velocity *= 0.005f;
        velocity *= mainCamera.orthographicSize;
        velocity *= faster ? 2 : 1;

        mainCamera.transform.position += velocity;
    }

    public void ZoomIn(){
        if (mainCamera.orthographicSize > 10)
            mainCamera.orthographicSize -= 10;
    }

    public void ZoomOut(){
        if (mainCamera.orthographicSize < 100)
            mainCamera.orthographicSize += 10;
    }
}
