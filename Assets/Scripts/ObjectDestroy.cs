using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//清理离玩家距离较远的对象
public class ObjectDestroy : MonoBehaviour
{
    public GameObject gb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - FindObjectOfType<PlayerMove>().transform.position.z < -200)
        {
            Destroy(gb);
        }
    }
}
