using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float leftRightSpeed;
    public static float wheelSpeed = 160;
    public bool isTurn = false;
    public float turnSpeedDamping;
    private bool HPdead=false;

    private Collider playerCollider;
    //private Renderer tankRenderer;
    private Renderer[] childRenderers; // 用于存储子对象的Renderer组件
    private bool adjustHeight = false;


    // Start is called before the first frame update
    void Start()
    {
        isTurn = false;
        //游戏暂停
        Time.timeScale = 0;

        playerCollider = GetComponent<Collider>();
        //tankRenderer = GetComponent<Renderer>();
        childRenderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            GameController.instance.StarGame();
        }
        else if(!HPdead)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (this.transform.rotation.eulerAngles.y >= 360 + LevelBoundary.leftTankAngleBound || this.transform.rotation.eulerAngles.y <= LevelBoundary.rightTankAngleBound + 1)
                {
                    // transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
                    transform.Rotate(0, -leftRightSpeed * Time.deltaTime, 0);
                    isTurn = true;
                }
                else
                {
                    isTurn = false;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (this.transform.rotation.eulerAngles.y >= 360 + LevelBoundary.leftTankAngleBound - 1 || this.transform.rotation.eulerAngles.y <= LevelBoundary.rightTankAngleBound)
                {
                    // transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
                    transform.Rotate(0, leftRightSpeed * Time.deltaTime, 0);
                    isTurn = true;
                }
                else
                {
                    isTurn = false;
                }
            }
            else
            {
                isTurn = false;

            }
        }

        if ((Vector3.Angle(transform.forward, Vector3.forward) >= LevelBoundary.rightTankAngleBound + 15) || Vector3.Angle(transform.up, Vector3.up) >= 90)
        {
            rePosition();
        }

        if (adjustHeight)
        {
            transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z);
            transform.rotation = Quaternion.identity;
        }

    }

    private void FixedUpdate()
    {
        if (isTurn == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * turnSpeedDamping, Space.Self);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
        }
    }


    public void rePosition()
    {
        transform.position = new Vector3(0, 0.8f, transform.position.z);
        transform.rotation = Quaternion.identity;

        StartCoroutine(DisableColliderTemporarily());
    }

    private IEnumerator DisableColliderTemporarily()
    {
        playerCollider.enabled = false; // 禁用Collider
        adjustHeight = true; // 开始调整高度

        float endTime = Time.time + 1.5f;
        while (Time.time < endTime)
        {
            // 切换坦克及其子对象的可见性
            //tankRenderer.enabled = !tankRenderer.enabled;
            foreach (Renderer childRenderer in childRenderers)
            {
                childRenderer.enabled = !childRenderer.enabled;
            }
            yield return new WaitForSeconds(0.15f); // 闪烁间隔
        }

        adjustHeight = false; // 停止调整高度
        // 确保坦克及其子对象最终可见
        //tankRenderer.enabled = true;
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.enabled = true;
        }
        playerCollider.enabled = true; // 再次启用Collider
    }


}
