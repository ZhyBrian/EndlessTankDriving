using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f; // 可以在Inspector中设置这个速度
    public GameObject explosionEffect;
    public float destructionDelay = 2f; // 销毁炮弹的延迟时间
    public float maxLifetime = 12f; // 炮弹的最大存在时间
    public Material hitMaterial; // 新材质的引用
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
        // 让炮弹以恒定速度直线飞行
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 如果炮弹触发了敌人坦克的触发器
        if (other.gameObject.CompareTag("enemy"))
        {
            // 执行炮弹触发敌人坦克的逻辑
            explosionEffect.SetActive(true);
            // 激活敌人坦克上的火焰效果
            Transform flames = other.transform.Find("Flame");
            Transform smoke = other.transform.Find("Smoke");
            if (flames != null)
            {
                flames.gameObject.SetActive(true);
                smoke.gameObject.SetActive(true);
            }

            // 改变敌方坦克的材质
            Renderer enemyRenderer = other.GetComponent<Renderer>();
            if (enemyRenderer != null)
            {
                enemyRenderer.material = hitMaterial;
            }
            // 找到并更改特定子对象的材质
            Transform childObject = other.transform.Find("TankFree_Tower");
            if (childObject != null)
            {
                Renderer childRenderer = childObject.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.material = hitMaterial;
                }
            }

            // 使炮弹消失
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            // 禁用敌人坦克的碰撞器，使之可以穿过
            Collider enemyCollider = other.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }
            other.gameObject.GetComponent<EnemyTankController>().isHitByProjectile = true;

            // 加分
            FindFirstObjectByType<Eatcoin>().score += addScore;
            // 加点
            FindFirstObjectByType<Eatcoin>().MP += addMP;
            if (FindFirstObjectByType<Eatcoin>().MP > 1.0)
            {
                FindFirstObjectByType<Eatcoin>().MP = (float)1.0;
            }

            hitMusic.Play();

            // 销毁炮弹
            Destroy(gameObject, destructionDelay);
        }
    }
}
