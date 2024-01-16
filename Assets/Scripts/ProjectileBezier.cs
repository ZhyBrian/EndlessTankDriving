using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBezier : MonoBehaviour
{
    public float speed = 15f; // ������Inspector����������ٶ�
    public GameObject explosionEffect;
    public float destructionDelay = 4f; // �����ڵ����ӳ�ʱ��
    public float maxLifetime = 20f; // �ڵ���������ʱ��
    public Material hitMaterial; // �²��ʵ�����
    //public GameObject player;
    public int addScore = 200;
    public float detectionAngle = 90f; // ǰ�����Ƕȵ�һ��

    private GameObject ClosestEnemy;
    private Transform target; // Ŀ��λ��
    private Vector3 controlPoint; // ���������ߵĿ��Ƶ�
    private Vector3 startPosition; // ��ʼλ��
    private float startTime; // ��ʼ�ƶ���ʱ��

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
        // ���ڵ��Ժ㶨�ٶ�ֱ�߷���
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator MoveAlongBezierCurve()
    {
        float journeyLength = Vector3.Distance(startPosition, target.position);
        float fracComplete = 0;

        while (fracComplete < 1)
        {
            // �����ٶȼ��㵱ǰ���ƶ�ʱ�����
            float distCovered = (Time.time - startTime) * speed;
            fracComplete = Mathf.Clamp01(distCovered / journeyLength);

            // ���㱴���������ϵ�ǰ��λ��
            Vector3 previousPosition = transform.position;
            Vector3 currentPosition = CalculateQuadraticBezierPoint(fracComplete, startPosition, controlPoint, target.position);
            transform.position = currentPosition;

            // ͨ���Ƚϵ�ǰ��ǰһλ������ô��µ����߷���
            if (fracComplete > 0)
            {
                Vector3 tangent = currentPosition - previousPosition;
                transform.rotation = Quaternion.LookRotation(tangent);
            }

            yield return null;
        }

        // ����������ڵ�����Ŀ�ĵغ���߼�
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // ʹ�ö��α��������߹�ʽ
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // ��һ��
        p += 2 * u * t * p1; // �ڶ���
        p += tt * p2; // ������

        return p;
    }


    public Vector3 CalculateControlPoint(Transform startPoint, Transform endPoint)
    {
        // �����е�
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

            hitMusic.Play();


            // �����ڵ�
            Destroy(gameObject, destructionDelay);
        }
    }
}
