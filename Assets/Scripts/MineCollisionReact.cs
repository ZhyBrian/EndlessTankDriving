using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollisionReact : MonoBehaviour
{

    //总共抖动时间
    public float ShakeTime = 0.4f;
    //在任何方向上偏移的距离
    public float ShakeAmount = 10.0f;
    //相机移动到震动点的速度
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
