
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerRotate : MonoBehaviour
{
    //让金币能够自己旋转


    void Start()
    {



    }
    void Update()
    {

        this.transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime, Space.Self);
    }


}