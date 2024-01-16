using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ж�̹�˿��ƣ�������ײʱ���������Ч���������ж�̹��һ����ʼ���Ϻ�ǰ���ٶȣ�����ﵽһ���߶�ʱ�����ƶ���Ҳ����������ת
public class EnemyCollisionReact : MonoBehaviour
{
    public Rigidbody rb;
    public bool isBack = false;
    public Vector3 initSpeed = new Vector3(0, 25, 6);
    public Vector3 backSpeed = new Vector3(0, 0, -6);
    public float backStartHeight = 12f;
    public float mainRotateSpeed = 2f;
    public float rotateRandomBound = 1f;

    //�ܹ�����ʱ��
    public float ShakeTime = 0.5f;
    //���κη�����ƫ�Ƶľ���
    public float ShakeAmount = 15.0f;
    //����ƶ����𶯵���ٶ�
    public float ShakeSpeed = 30.0f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            FindObjectOfType<CameraShake>().ShakeTime = ShakeTime;
            FindObjectOfType<CameraShake>().ShakeAmount = ShakeAmount;
            FindObjectOfType<CameraShake>().ShakeSpeed = ShakeSpeed;
            FindObjectOfType<CameraShake>().StartShake();
            rb.velocity = initSpeed;
            rb.angularVelocity = new Vector3(mainRotateSpeed, Random.Range(-rotateRandomBound, rotateRandomBound), Random.Range(-rotateRandomBound, rotateRandomBound));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y > backStartHeight && isBack == false)
        {
            isBack = true;
            rb.velocity += backSpeed;
        }
    }
}
