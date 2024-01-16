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
    private Renderer[] childRenderers; // ���ڴ洢�Ӷ����Renderer���
    private bool adjustHeight = false;


    // Start is called before the first frame update
    void Start()
    {
        isTurn = false;
        //��Ϸ��ͣ
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
        playerCollider.enabled = false; // ����Collider
        adjustHeight = true; // ��ʼ�����߶�

        float endTime = Time.time + 1.5f;
        while (Time.time < endTime)
        {
            // �л�̹�˼����Ӷ���Ŀɼ���
            //tankRenderer.enabled = !tankRenderer.enabled;
            foreach (Renderer childRenderer in childRenderers)
            {
                childRenderer.enabled = !childRenderer.enabled;
            }
            yield return new WaitForSeconds(0.15f); // ��˸���
        }

        adjustHeight = false; // ֹͣ�����߶�
        // ȷ��̹�˼����Ӷ������տɼ�
        //tankRenderer.enabled = true;
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.enabled = true;
        }
        playerCollider.enabled = true; // �ٴ�����Collider
    }


}
