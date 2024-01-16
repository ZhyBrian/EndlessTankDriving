using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // 炮弹预制件
    public GameObject projectileBezierPrefab;
    public Transform fireTransform; // 炮弹发射的位置

    //音效
    public AudioSource music;
    public AudioSource maxshoot;
    // public float launchSpeed = 30f; // 可以根据需要调整这个速度


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
        // 实例化炮弹
        GameObject projectileInstance = Instantiate(
            projectilePrefab,
            fireTransform.position,
            fireTransform.rotation);


    }
    private void BezierFire()
    {
     
        // 实例化炮弹
        GameObject projectileInstance = Instantiate(
            projectileBezierPrefab,
            fireTransform.position,
            fireTransform.rotation);


    }
}
