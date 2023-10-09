using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    // 每块60 重叠2 每个section总长176
    
    public static float leftTankAngleBound = -60f;  // static修饰的变量为整个类而非某个对象服务
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
