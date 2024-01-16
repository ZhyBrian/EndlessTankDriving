using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //Slider的调用需要引用UI源文件

public class HPC : MonoBehaviour
{
    public Slider HP;  //实例化一个Slider

    private void Start()
    {
        HP.value = 1;  //Value的值介于0-1之间，且为浮点数
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))  //如果按下A键，血量就会减少
        {
            HP.value = HP.value - 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.D))  //按下D键，血量就会增加
        {
            HP.value = HP.value + 0.1f;
        }
    }
}

