using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignmentMachine : MonoBehaviour
{
    [Header("Change")]
    [SerializeField] public int myInt = 3;
    
    [Space(10)]
    [Header("Do not change")]
    [SerializeField] private ParticleSystem ps = null;
    [SerializeField] private TextMeshPro myText = null;

    void Start()
    {
        SetMyText();
    }

    void Update()
    {
        
    }

    public int UseThisMachine()
    {
        ps.Play();
        
        return myInt;
    }

    // Same result, different meaning
    public int GetMachineValue()
    {
        return myInt;
    }

    private void SetMyText()
    {
        myText.text = "=" + myInt.ToString();
    }
}
