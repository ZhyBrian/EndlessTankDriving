using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//游戏的全局控制器，控制游戏的开始和结束

public class GameController : MonoBehaviour

{
    public AudioSource music_gameover;
    public AudioSource music_begin;

    public static GameController instance;
    public bool gameStarted = false;
    public bool gameOver = false;

    // UI界面
    //开始界面
    public GameObject startUI;
    //分数与生命值统计


    //结束界面
    public GameObject overUI;
    // public Text scoreText; // UI Text组件的引用
    public GameObject scoreText;
    //public GameObject HPText;
    public GameObject dieEffect;
    //public GameObject mineEffect;
    public GameObject player;
    public Material dieMaterial; // 新材质的引用
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
        //按下空格键，游戏开始
        //HP归零时游戏开始
        overUI.SetActive(false);
        dieEffect.SetActive(false);
        //mineEffect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            //重新加载当前场景
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

        // 改变坦克的材质
        Renderer myRenderer = player.GetComponent<Renderer>();
        if (myRenderer != null)
        {
            myRenderer.material = dieMaterial;
        }
        // 找到并更改特定子对象的材质
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
        // 等待2秒，这段时间dieEffect动画应该播放完成
        yield return new WaitForSeconds(1.0f);

        // 然后暂停时间
        Time.timeScale = 0;
    }


}


