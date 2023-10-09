using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;  // 在unity中选择以引用player对象的transform属性
    public Vector3 offset; // 在unity中更改这一向量值
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 要使得相机跟随玩家：
        // 方法1：在层级树中拖动相机，使其成为player的子对象（问题：player旋转时相机也会跟着旋转）
        // 方法2：脚本实现：

        // Debug.Log(player.position);
        transform.position = player.position + offset;
        // transform.position：transform没有首字母大写，代表当前对象的transform属性
    }
}
