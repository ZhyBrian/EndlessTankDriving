using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlames : MonoBehaviour
{

    public GameObject Flame;
    public GameObject Smoke;

    // Start is called before the first frame update
    void Start()
    {
        Flame.SetActive(false);
        Smoke.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
