using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("Change")]
    [SerializeField] public List<Gate> relatedGates = new List<Gate>();
    
    [Space(10)]
    [Header("Do Not Change")]
    [SerializeField] public List<GameObject> myConnectionLights = new List<GameObject>();

    [HideInInspector] public int activeConnections = 0;
    [SerializeField] private ParticleSystem ps = null;

    
    void Start()
    {
        //ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        
    }

    public void PressSwitch()
    {
        Debug.Log("switch pressed");
        
        foreach (Gate gate in relatedGates)
        {
            gate.ToggleGate();
        }
        
        ps.Play();
    }

}
