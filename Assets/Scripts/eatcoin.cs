using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Eatcoin : MonoBehaviour
{
    //��Ч
    public AudioSource eat_music;
    public AudioSource crush_mine;
    public AudioSource crush_barrier;
    public AudioSource crush_enemy;


    public int score = 0;
    [SerializeField] private Text scoreText;

    public int HP = 10;
    public GameObject smoke;
    public Material badMaterial; // �²��ʵ�����
    public Material goodMaterial; // �²��ʵ�����
    public GameObject player;

    private float maxHP = 10;
    private bool firstNearDie = true;
    private bool firstHealthy = false;

    [SerializeField] private Text HPText;

    public float MP = 1;
    
    public Slider HPC;  //ʵ����һ��Slider
    public Slider MPC;
    void Start()
    {
        HPC.value = 1;  //Value��ֵ����0-1֮�䣬��Ϊ������
        MPC.value = 1;
        smoke.SetActive(false);
    }


    //��ײ���
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
            // �ı�̹�˵Ĳ���
            Renderer myRenderer = player.GetComponent<Renderer>();
            if (myRenderer != null)
            {
                myRenderer.material = badMaterial;
            }
            // �ҵ��������ض��Ӷ���Ĳ���
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
            // �ı�̹�˵Ĳ���
            Renderer myRenderer = player.GetComponent<Renderer>();
            if (myRenderer != null)
            {
                myRenderer.material = goodMaterial;
            }
            // �ҵ��������ض��Ӷ���Ĳ���
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