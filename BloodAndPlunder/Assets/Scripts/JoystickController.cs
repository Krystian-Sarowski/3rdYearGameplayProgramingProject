using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [HideInInspector]
    public Vector3 originPos;

    [HideInInspector]
    public Vector3 currentPos;
    float boundsRadius = 50.0f;
    bool joystickActive = false;
    int joystickIndex;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;

        CalculateBoundsRadius();
        
        currentPos = originPos;

        transform.position = originPos;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(Input.touches[i].phase == TouchPhase.Began)
            {
                Vector3 touchPos = Input.touches[i].position;
                touchPos.z = 0;

                if (Mathf.Abs(Vector3.Distance(touchPos, originPos)) < boundsRadius && !joystickActive)
                {
                    joystickIndex = Input.GetTouch(i).fingerId;
                    joystickActive = true;
                    break;
                }
            }

        }

        if (joystickActive)
        {
            int index = 0;

            for (int i = 0; i < Input.touchCount; i++)
            {
                if(joystickIndex == Input.touches[i].fingerId)
                {
                    index = i;
                    break;
                }
            }

            if (Input.touches[index].phase == TouchPhase.Ended)
            {
                joystickActive = false;
                currentPos = originPos;
            }

            else
            {
                Vector3 touchPos = Input.touches[index].position;
                touchPos.z = 0;

                if (Mathf.Abs(Vector3.Distance(touchPos, originPos)) > boundsRadius)
                {
                    currentPos = originPos + (touchPos - originPos).normalized * boundsRadius;
                }
                else
                {
                    currentPos = touchPos;
                }
            }

            transform.position = currentPos;

            player.setTargetVel(transform.position - originPos);
        }
    }

    void CalculateBoundsRadius()
    {
        Vector3 maxPosition = Vector3.zero;

        transform.localPosition = new Vector3(50, 0, 0);

        maxPosition = transform.position;

        boundsRadius = Mathf.Abs(Vector3.Distance(originPos, maxPosition));

    }
}
