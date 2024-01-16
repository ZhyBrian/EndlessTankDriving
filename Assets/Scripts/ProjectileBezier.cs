using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBezier : MonoBehaviour
{
    public float speed = 15f; // 可以在Inspector中设置这个速度
    public GameObject explosionEffect;
    public float destructionDelay = 4f; // 销毁炮弹的延迟时间
    public float maxLifetime = 20f; // 炮弹的最大存在时间
    public Material hitMaterial; // 新材质的引用
    //public GameObject player;
    public int addScore = 200;
    public float detectionAngle = 90f; // 前方检测角度的一半

    private GameObject ClosestEnemy;
    private Transform target; // 目标位置
    private Vector3 controlPoint; // 贝塞尔曲线的控制点
    private Vector3 startPosition; // 起始位置
    private float startTime; // 开始移动的时间

    public AudioSource hitMusic;


    // Start is called before the first frame update
    void Start()
    {
        explosionEffect.SetActive(false);

        ClosestEnemy = FindClosestEnemyInFront();
        target = ClosestEnemy.transform;

        startPosition = transform.position;

        controlPoint = CalculateControlPoint(transform, target);

        startTime = Time.time;
        StartCoroutine(MoveAlongBezierCurve());

        Destroy(gameObject, maxLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // 让炮弹以恒定速度直线飞行
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator MoveAlongBezierCurve()
    {
        float journeyLength = Vector3.Distance(startPosition, target.position);
        float fracComplete = 0;

        while (fracComplete < 1)
        {
            // 根据速度计算当前的移动时间分数
            float distCovered = (Time.time - startTime) * speed;
            fracComplete = Mathf.Clamp01(distCovered / journeyLength);

            // 计算贝塞尔曲线上当前的位置
            Vector3 previousPosition = transform.position;
            Vector3 currentPosition = CalculateQuadraticBezierPoint(fracComplete, startPosition, controlPoint, target.position);
            transform.position = currentPosition;

            // 通过比较当前和前一位置来获得大致的切线方向
            if (fracComplete > 0)
            {
                Vector3 tangent = currentPosition - previousPosition;
                transform.rotation = Quaternion.LookRotation(tangent);
            }

            yield return null;
        }

        // 在这里添加炮弹到达目的地后的逻辑
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // 使用二次贝塞尔曲线公式
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // 第一项
        p += 2 * u * t * p1; // 第二项
        p += tt * p2; // 第三项

        return p;
    }


    public Vector3 CalculateControlPoint(Transform startPoint, Transform endPoint)
    {
        // 计算中点
        Vector3 midpoint = (startPoint.position + endPoint.position) / 2;

        float distance = Vector3.Distance(midpoint, endPoint.position);

        Vector3 controlPoint = startPoint.position + transform.forward * distance * 2;

        // Debug.Log(transform.forward.ToString());

        return controlPoint;
    }

    GameObject FindClosestEnemyInFront()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null && enemyCollider.enabled)
            {
                Vector3 directionToTarget = enemy.transform.position - transform.position;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
                float sqrDistanceToTarget = directionToTarget.sqrMagnitude;

                if (angleToTarget < detectionAngle && sqrDistanceToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = sqrDistanceToTarget;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
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

            hitMusic.Play();


            // 销毁炮弹
            Destroy(gameObject, destructionDelay);
        }
    }
}
