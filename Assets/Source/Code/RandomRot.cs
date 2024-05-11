using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float ro = 0;
        int so = Random.Range(0, 3);
        ro = so == 0 ? 0 : so == 1 ? 90 : so == 2 ? 180 : 270;
        transform.Rotate(new Vector3(0,0,ro));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
