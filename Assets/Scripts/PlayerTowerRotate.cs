using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerRotate : MonoBehaviour
{
    public float leftRightRotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.J))
        //{
        //    if (this.transform.rotation.eulerAngles.y >= 360 + LevelBoundary.leftTowerAngleBound || this.transform.rotation.eulerAngles.y <= LevelBoundary.rightTowerAngleBound + 5)
        //    {
        //        //Debug.Log(this.transform.rotation.eulerAngles.y);
        //        transform.Rotate(0, -leftRightRotateSpeed * Time.deltaTime, 0);
        //    }
        //}
        //if (Input.GetKey(KeyCode.L))
        //{
        //    if (this.transform.rotation.eulerAngles.y >= 360 + LevelBoundary.leftTowerAngleBound - 5 || this.transform.rotation.eulerAngles.y <= LevelBoundary.rightTowerAngleBound)
        //    {
        //        //Debug.Log(this.transform.rotation.eulerAngles.y);
        //        transform.Rotate(0, leftRightRotateSpeed * Time.deltaTime, 0);
        //    }
        //}

        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(0, -leftRightRotateSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(0, leftRightRotateSpeed * Time.deltaTime, 0);
        }
    }
}
