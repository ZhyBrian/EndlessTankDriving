using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    // ÿ��60 �ص�2 ÿ��section�ܳ�176
    
    public static float leftTankAngleBound = -60f;  // static���εı���Ϊ���������ĳ���������
    public static float rightTankAngleBound = 60f;
    //public static float leftTowerAngleBound = -120f;  
    //public static float rightTowerAngleBound = 120f;
    public float internalLeftTankAngleBound;
    public float internalRightTankAngleBound;
    //public float internalLeftTowerAngleBound;
    //public float internalRightTowerAngleBound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        internalLeftTankAngleBound = leftTankAngleBound;
        internalRightTankAngleBound = rightTankAngleBound;
        //internalLeftTowerAngleBound = leftTowerAngleBound;
        //internalRightTowerAngleBound = rightTowerAngleBound;
        
    }
}
