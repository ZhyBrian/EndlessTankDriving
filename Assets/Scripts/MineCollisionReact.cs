using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollisionReact : MonoBehaviour
{

    //�ܹ�����ʱ��
    public float ShakeTime = 0.4f;
    //���κη�����ƫ�Ƶľ���
    public float ShakeAmount = 10.0f;
    //����ƶ����𶯵���ٶ�
    public float ShakeSpeed = 20.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            FindObjectOfType<CameraShake>().ShakeTime = ShakeTime;
            FindObjectOfType<CameraShake>().ShakeAmount = ShakeAmount;
            FindObjectOfType<CameraShake>().ShakeSpeed = ShakeSpeed;
            FindObjectOfType<CameraShake>().StartShake();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
