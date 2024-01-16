using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Eatcoin : MonoBehaviour
{
    //音效
    public AudioSource eat_music;
    public AudioSource crush_mine;
    public AudioSource crush_barrier;
    public AudioSource crush_enemy;


    public int score = 0;
    [SerializeField] private Text scoreText;

    public int HP = 10;
    public GameObject smoke;
    public Material badMaterial; // 新材质的引用
    public Material goodMaterial; // 新材质的引用
    public GameObject player;

    private float maxHP = 10;
    private bool firstNearDie = true;
    private bool firstHealthy = false;

    [SerializeField] private Text HPText;

    public float MP = 1;
    
    public Slider HPC;  //实例化一个Slider
    public Slider MPC;
    void Start()
    {
        HPC.value = 1;  //Value的值介于0-1之间，且为浮点数
        MPC.value = 1;
        smoke.SetActive(false);
    }


    //碰撞检测
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "coin")
        {
            eat_music.Play();
            Destroy(collider.gameObject);
            score = score + 10;
            // scoreText.text = "Score:" + score;


        }
        else if (collider.tag == "hammer")
        {
            Destroy(collider.gameObject);
        }

    }
     void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "barrier")
        {
            crush_barrier.Play();
            HP--;
        }
        else if(collision.collider.tag == "enemy")
        {
            crush_enemy.Play();
            HP = HP - 3;
        }
        else if(collision.collider.tag =="mine")
        {
            crush_mine.Play();
            HP = HP - 2;
        }
        else if (collision.collider.tag == "hammer")
        {
            eat_music.Play();
            HP = HP + 2;
        }

            if (HP <= 0)
        {
            HPText.text = "HP:" + 0;
 
            GameController.instance.GameOver();
           

        }else if(HP >= 10)
        {
            HP = 10;
        }
    }

    void Update()
    {

        HPText.text = "HP:" + HP;
        HPC.value = HP / maxHP;

        MPC.value = MP;

        if (HPC.value < 0.4 && firstNearDie)
        {
            smoke.SetActive(true);
            // 改变坦克的材质
            Renderer myRenderer = player.GetComponent<Renderer>();
            if (myRenderer != null)
            {
                myRenderer.material = badMaterial;
            }
            // 找到并更改特定子对象的材质
            Transform childObject = player.transform.Find("TankFree_Tower");
            if (childObject != null)
            {
                Renderer childRenderer = childObject.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.material = badMaterial;
                }
            }

            firstNearDie = !firstNearDie;
            firstHealthy = !firstHealthy;
        }
        else if (HPC.value >= 0.4 && firstHealthy)
        {
            smoke.SetActive(false);
            // 改变坦克的材质
            Renderer myRenderer = player.GetComponent<Renderer>();
            if (myRenderer != null)
            {
                myRenderer.material = goodMaterial;
            }
            // 找到并更改特定子对象的材质
            Transform childObject = player.transform.Find("TankFree_Tower");
            if (childObject != null)
            {
                Renderer childRenderer = childObject.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.material = goodMaterial;
                }
            }

            firstNearDie = !firstNearDie;
            firstHealthy = !firstHealthy;
        }

        scoreText.text = "" + score;
    }


}