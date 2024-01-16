using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankController : MonoBehaviour
{
    public bool isHitByProjectile = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && isHitByProjectile)
        {
            // �����ٶȺͽ��ٶ�Ϊ��
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            // rb.angularVelocity = Vector3.zero;

        }
    }
}
