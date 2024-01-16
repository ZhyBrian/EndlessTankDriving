using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // �ڵ�Ԥ�Ƽ�
    public GameObject projectileBezierPrefab;
    public Transform fireTransform; // �ڵ������λ��

    //��Ч
    public AudioSource music;
    public AudioSource maxshoot;
    // public float launchSpeed = 30f; // ���Ը�����Ҫ��������ٶ�


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Fire();
            music.Play();


        }
        //if (Input.GetKeyDown(KeyCode.P))
        if (Input.GetKeyDown(KeyCode.P) && FindFirstObjectByType<Eatcoin>().MP >= 1.0)
        {
            BezierFire();
            maxshoot.Play();
            FindFirstObjectByType<Eatcoin>().MP = 0;
        }

    }

    private void Fire()
    {
        // ʵ�����ڵ�
        GameObject projectileInstance = Instantiate(
            projectilePrefab,
            fireTransform.position,
            fireTransform.rotation);


    }
    private void BezierFire()
    {
     
        // ʵ�����ڵ�
        GameObject projectileInstance = Instantiate(
            projectileBezierPrefab,
            fireTransform.position,
            fireTransform.rotation);


    }
}
