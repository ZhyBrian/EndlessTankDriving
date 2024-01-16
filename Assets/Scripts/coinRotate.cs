
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    //让金币能够自己旋转


    void Start()
    {

  

    }
    void Update()
    {
       
        this.transform.Rotate(new Vector3(0, 0, 100) * Time.deltaTime, Space.Self);
    }
   

}