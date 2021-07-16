using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoVideo : MonoBehaviour
{
    const string URL = @"https://drive.google.com/uc?export=download&id=1h62wJ9w1vbH5qchztObVzHBR5a7E2i94";

    void Update()
    {
        if (Input.anyKey)
        {
            GameManager.Instance.StopDemo();
            Destroy(gameObject);
        }
    }
}
