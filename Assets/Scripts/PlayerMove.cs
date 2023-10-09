using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float leftRightSpeed;
    public static float wheelSpeed = 160;
    public bool isTurn = false;
    public float turnSpeedDamping;
    
    // Start is called before the first frame update
    void Start()
    {
        isTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (this.transform.rotation.eulerAngles.y >= 360 + LevelBoundary.leftTankAngleBound || this.transform.rotation.eulerAngles.y <= LevelBoundary.rightTankAngleBound + 1)
            {
                // transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
                transform.Rotate(0, -leftRightSpeed * Time.deltaTime, 0);
                isTurn = true;
            }
            else
            {
                isTurn = false;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.rotation.eulerAngles.y >= 360 + LevelBoundary.leftTankAngleBound - 1 || this.transform.rotation.eulerAngles.y <= LevelBoundary.rightTankAngleBound)
            {
                // transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
                transform.Rotate(0, leftRightSpeed * Time.deltaTime, 0);
                isTurn = true;
            }
            else
            {
                isTurn = false;
            }
        }
        else
        {
            isTurn = false;
        }
    }

    private void FixedUpdate()
    {
        if (isTurn == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * turnSpeedDamping, Space.Self);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
        }
    }
}
