using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundary : MonoBehaviour
{
    public float bounceStrength = 10f; // ������Unity�༭���е����������ǿ��

    private Rigidbody rb; // ���ڻ���̹�˵�Rigidbody���

    void Start()
    {
        // ��ȡ������Rigidbody���
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // �����ײ��GameObject�Ƿ��С�Boundary����ǩ
        if (collision.gameObject.CompareTag("boundary"))
        {
            // ��ȡ��ײ��ķ��ߣ������������㷴����
            Vector3 normal = collision.contacts[0].normal;

            // ��������������ײ��ķ��߷���
            Vector3 bounceDirection = -normal * bounceStrength;

            // Ӧ�÷�������̹�˵�Rigidbody��
            rb.AddForce(bounceDirection, ForceMode.Impulse);
        }
    }
}
