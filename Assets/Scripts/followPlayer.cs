using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;  // ��unity��ѡ��������player�����transform����
    public Vector3 offset; // ��unity�и�����һ����ֵ
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ҫʹ�����������ң�
        // ����1���ڲ㼶�����϶������ʹ���Ϊplayer���Ӷ������⣺player��תʱ���Ҳ�������ת��
        // ����2���ű�ʵ�֣�

        // Debug.Log(player.position);
        transform.position = player.position + offset;
        // transform.position��transformû������ĸ��д������ǰ�����transform����
    }
}
