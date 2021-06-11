using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    private enum MachineType {Assignment, Operation}
    private enum OperationType {Addition, Subtruction, Multiplication}
    
    [SerializeField] private MachineType myMachineType = MachineType.Assignment;
    [SerializeField] private OperationType myOperationType = OperationType.Addition;
    [SerializeField] private int myInt = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int UseThisMachine(int num)
    {
        switch (myMachineType)
        {
            case MachineType.Assignment:
                return myInt;
            case MachineType.Operation:
                switch (myOperationType)
                {
                    case OperationType.Addition:
                        return (num + myInt);
                    case OperationType.Subtruction:
                        return (num - myInt);
                    case OperationType.Multiplication:
                        return (num * myInt);
                }
                break;
        }

        return 0;
    }
}
