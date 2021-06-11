using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OperationMachine : MonoBehaviour
{
    public enum OperationType {Addition, Subtruction, Multiplication}
    
    [Header("Change")]
    [SerializeField] public OperationType myOperationType = OperationType.Addition;
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

    public int GetMachineType()
    {
        switch (myOperationType)
        {
            case OperationType.Addition:
                return 1;
            case OperationType.Subtruction:
                return -1;
            case OperationType.Multiplication:
                return 0;
        }

        return 0;
    }

    public int GetMachineValue()
    {
        return myInt;
    }

    public int UseThisMachine(int num)
    {
        ps.Play();

        switch (myOperationType)
        {
            case OperationType.Addition:
                return (num + myInt);
            case OperationType.Subtruction:
                return (num - myInt);
            case OperationType.Multiplication:
                return (num * myInt);
        }

        return 0;
    }

    private void SetMyText()
    {
        switch (myOperationType)
        {
            case OperationType.Addition:
                myText.text = "+" + myInt.ToString();
                break;
            case OperationType.Subtruction:
                myText.text = "-" + myInt.ToString();
                break;
            case OperationType.Multiplication:
                myText.text = "*" + myInt.ToString();
                break;
        }
    }
}
