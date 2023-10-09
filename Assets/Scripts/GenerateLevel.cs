using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sections;
    public int zPos = 200;
    public int zPosIncrement = 175;
    public int zPosMap = 525;
    public int secNum;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerMove>().transform.position.z > zPos)
        {
            secNum = Random.Range(0, 3);
            Instantiate(sections[secNum], new Vector3(0, 0, zPosMap), Quaternion.identity);

            zPos += zPosIncrement;
            zPosMap += zPosIncrement;
            Debug.Log(secNum);
        }
    }
}
