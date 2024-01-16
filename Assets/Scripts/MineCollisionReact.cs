using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollisionReact : MonoBehaviour
{
    //���׿��ƣ��������׷�����ײʱ�����������Ч��

    //�ܹ�����ʱ��
    public float ShakeTime = 0.4f;
    //���κη�����ƫ�Ƶľ���
    public float ShakeAmount = 10.0f;
    //����ƶ����𶯵���ٶ�
    public float ShakeSpeed = 20.0f;


    public GameObject mineEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //�����Ϸ�����Ƿ��������ײ
        if (collision.collider.tag == "Player")
        {
            FindObjectOfType<CameraShake>().ShakeTime = ShakeTime;
            FindObjectOfType<CameraShake>().ShakeAmount = ShakeAmount;
            FindObjectOfType<CameraShake>().ShakeSpeed = ShakeSpeed;
            FindObjectOfType<CameraShake>().StartShake();

            mineEffect.SetActive(true);

            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mineEffect.SetActive(false);
        ParticleSystem ps = mineEffect.GetComponent<ParticleSystem>();
        var psmain = ps.main;
        psmain.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
