using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌对坦克控制：发生碰撞时有相机抖动效果，并给敌对坦克一个初始向上和前进速度，当其达到一定高度时反向移动，也会进行随机旋转
public class EnemyCollisionReact : MonoBehaviour
{
    public Rigidbody rb;
    public bool isBack = false;
    public Vector3 initSpeed = new Vector3(0, 25, 6);
    public Vector3 backSpeed = new Vector3(0, 0, -6);
    public float backStartHeight = 12f;
    public float mainRotateSpeed = 2f;
    public float rotateRandomBound = 1f;

    //总共抖动时间
    public float ShakeTime = 0.5f;
    //在任何方向上偏移的距离
    public float ShakeAmount = 15.0f;
    //相机移动到震动点的速度
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
