
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerRotate : MonoBehaviour
{
    //�ý���ܹ��Լ���ת


    void Start()
    {



    }
    void Update()
    {

        this.transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime, Space.Self);
    }


}