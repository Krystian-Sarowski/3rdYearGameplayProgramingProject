using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform player = null;       //Transform of the player.

    [SerializeField]
    float minX = 0.0f, maxX = 0.0f, minY = 0.0f, maxY = 0.0f; //The minimum and maximum values the position can be.

    float cameraWidth;      //The width of the camera.
    float cameraHeight;     //The height of the camera.
    Vector3 pos;            //The vector for the position of camera.

    //Start is called before the first frame update
    void Start()
    {       
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        pos.z = -10;
    }

    //Update is called once per frame
    void LateUpdate()
    {
        pos.x = Mathf.Clamp(player.position.x, minX, maxX);
        pos.y = Mathf.Clamp(player.position.y, minY, maxY);
        transform.position = pos;
    }
}
