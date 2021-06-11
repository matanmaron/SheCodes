using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Block>().isWalkable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
