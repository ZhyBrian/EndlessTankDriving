using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollisionReact : MonoBehaviour
{
    public Rigidbody rb;
    public bool isBack = false;
    public bool isLeftRight = false;
    public Vector3 initSpeed = new Vector3(0, 15, 6);
    public Vector3 backSpeed = new Vector3(0, 0, -6);
    public float backStartHeight = 8f;
    public float mainRotateSpeed = 4f;
    public float rotateRandomBound = 2f;
    public float leftRightStartHeight = 3f;
    public float leftRightRandomBound = 4f;

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
            rb.velocity = initSpeed;
            rb.angularVelocity = new Vector3(mainRotateSpeed, Random.Range(-rotateRandomBound, rotateRandomBound), Random.Range(-rotateRandomBound, rotateRandomBound));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isBack = false;
        isLeftRight = false;
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
        if (transform.position.y > leftRightStartHeight && isLeftRight == false)
        {
            isLeftRight = true;
            rb.velocity += new Vector3(Random.Range(-leftRightRandomBound, leftRightRandomBound), 0, 0);
        }
    }
}
