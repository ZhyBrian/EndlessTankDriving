using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //Slider�ĵ�����Ҫ����UIԴ�ļ�

public class HPC : MonoBehaviour
{
    public Slider HP;  //ʵ����һ��Slider

    private void Start()
    {
        HP.value = 1;  //Value��ֵ����0-1֮�䣬��Ϊ������
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))  //�������A����Ѫ���ͻ����
        {
            HP.value = HP.value - 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.D))  //����D����Ѫ���ͻ�����
        {
            HP.value = HP.value + 0.1f;
        }
    }
}

