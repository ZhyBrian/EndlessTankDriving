using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f; // ������Inspector����������ٶ�
    public GameObject explosionEffect;
    public float destructionDelay = 2f; // �����ڵ����ӳ�ʱ��
    public float maxLifetime = 12f; // �ڵ���������ʱ��
    public Material hitMaterial; // �²��ʵ�����
    //public GameObject player;
    public int addScore = 100;
    public float addMP = (float)0.25;

    public AudioSource hitMusic;

    // Start is called before the first frame update
    void Start()
    {
        explosionEffect.SetActive(false);
        Destroy(gameObject, maxLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // ���ڵ��Ժ㶨�ٶ�ֱ�߷���
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // ����ڵ������˵���̹�˵Ĵ�����
        if (other.gameObject.CompareTag("enemy"))
        {
            // ִ���ڵ���������̹�˵��߼�
            explosionEffect.SetActive(true);
            // �������̹���ϵĻ���Ч��
            Transform flames = other.transform.Find("Flame");
            Transform smoke = other.transform.Find("Smoke");
            if (flames != null)
            {
                flames.gameObject.SetActive(true);
                smoke.gameObject.SetActive(true);
            }

            // �ı�з�̹�˵Ĳ���
            Renderer enemyRenderer = other.GetComponent<Renderer>();
            if (enemyRenderer != null)
            {
                enemyRenderer.material = hitMaterial;
            }
            // �ҵ��������ض��Ӷ���Ĳ���
            Transform childObject = other.transform.Find("TankFree_Tower");
            if (childObject != null)
            {
                Renderer childRenderer = childObject.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.material = hitMaterial;
                }
            }

            // ʹ�ڵ���ʧ
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            // ���õ���̹�˵���ײ����ʹ֮���Դ���
            Collider enemyCollider = other.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }
            other.gameObject.GetComponent<EnemyTankController>().isHitByProjectile = true;

            // �ӷ�
            FindFirstObjectByType<Eatcoin>().score += addScore;
            // �ӵ�
            FindFirstObjectByType<Eatcoin>().MP += addMP;
            if (FindFirstObjectByType<Eatcoin>().MP > 1.0)
            {
                FindFirstObjectByType<Eatcoin>().MP = (float)1.0;
            }

            hitMusic.Play();

            // �����ڵ�
            Destroy(gameObject, destructionDelay);
        }
    }
}
