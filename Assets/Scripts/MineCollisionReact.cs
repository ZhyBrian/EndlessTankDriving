using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollisionReact : MonoBehaviour
{
    //地雷控制：玩家与地雷发生碰撞时，触发相机震动效果

    //总共抖动时间
    public float ShakeTime = 0.4f;
    //在任何方向上偏移的距离
    public float ShakeAmount = 10.0f;
    //相机移动到震动点的速度
    public float ShakeSpeed = 20.0f;


    public GameObject mineEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //检测游戏对象是否与地雷碰撞
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
