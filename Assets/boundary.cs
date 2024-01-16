using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundary : MonoBehaviour
{
    public float bounceStrength = 10f; // 可以在Unity编辑器中调整这个力的强度

    private Rigidbody rb; // 用于缓存坦克的Rigidbody组件

    void Start()
    {
        // 获取并缓存Rigidbody组件
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞的GameObject是否有“Boundary”标签
        if (collision.gameObject.CompareTag("boundary"))
        {
            // 获取碰撞点的法线，它将用来计算反向力
            Vector3 normal = collision.contacts[0].normal;

            // 反向力是沿着碰撞点的法线方向
            Vector3 bounceDirection = -normal * bounceStrength;

            // 应用反向力到坦克的Rigidbody上
            rb.AddForce(bounceDirection, ForceMode.Impulse);
        }
    }
}
