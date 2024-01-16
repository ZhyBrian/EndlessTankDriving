using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//��Ϸ��ȫ�ֿ�������������Ϸ�Ŀ�ʼ�ͽ���

public class GameController : MonoBehaviour

{
    public AudioSource music_gameover;
    public AudioSource music_begin;

    public static GameController instance;
    public bool gameStarted = false;
    public bool gameOver = false;

    // UI����
    //��ʼ����
    public GameObject startUI;
    //����������ֵͳ��


    //��������
    public GameObject overUI;
    // public Text scoreText; // UI Text���������
    public GameObject scoreText;
    //public GameObject HPText;
    public GameObject dieEffect;
    //public GameObject mineEffect;
    public GameObject player;
    public Material dieMaterial; // �²��ʵ�����
    public float delayTime = 2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //���¿ո������Ϸ��ʼ
        //HP����ʱ��Ϸ��ʼ
        overUI.SetActive(false);
        dieEffect.SetActive(false);
        //mineEffect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            //���¼��ص�ǰ����
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void StarGame()
    {
        gameStarted = true;
        //music_begin.Stop();
        PlayerMove script = player.GetComponent<PlayerMove>();
        if (script != null)
        {
            script.enabled = true;
        }

        startUI.SetActive(false);
        scoreText.SetActive(true);
     //  HPText.SetActive(true);
        Time.timeScale = 1;

    }


    public void GameOver()
    {
       
        gameOver = true;
        music_gameover.Play();
        dieEffect.SetActive(true);
        overUI.SetActive(true);

        // �ı�̹�˵Ĳ���
        Renderer myRenderer = player.GetComponent<Renderer>();
        if (myRenderer != null)
        {
            myRenderer.material = dieMaterial;
        }
        // �ҵ��������ض��Ӷ���Ĳ���
        Transform childObject = player.transform.Find("TankFree_Tower");
        if (childObject != null)
        {
            Renderer childRenderer = childObject.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.material = dieMaterial;
            }
        }

        PlayerMove script = player.GetComponent<PlayerMove>();
        if (script != null)
        {
            script.enabled = false;
        }


        // Time.timeScale = 0;
        StartCoroutine(WaitAndPause());

    }
    private IEnumerator WaitAndPause()
    {
        // �ȴ�2�룬���ʱ��dieEffect����Ӧ�ò������
        yield return new WaitForSeconds(1.0f);

        // Ȼ����ͣʱ��
        Time.timeScale = 0;
    }


}


