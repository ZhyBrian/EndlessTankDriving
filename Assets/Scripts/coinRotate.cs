
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    //�ý���ܹ��Լ���ת


    void Start()
    {

  

    }
    void Update()
    {
       
        this.transform.Rotate(new Vector3(0, 0, 100) * Time.deltaTime, Space.Self);
    }
   

}