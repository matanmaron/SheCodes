using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [Header("Change")]
    [SerializeField] private Vector3 textOffset = Vector3.zero;
    
    [Space(10)]
    [Header("Do Not Change")]
    [SerializeField] private GameObject inGameTextPrefab = null;
    
    private List<Transform> targetsList = new List<Transform>();
    private List<Transform> textsList = new List<Transform>();
    
    
    public static InGameUIManager Instance { get; private set; } //singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start()
    {

    }

    void Update()
    {
        KeepInPlace();
        
    }

    private Vector3 TargetPosition(Transform targetObj)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(targetObj.position + textOffset);

        return pos;
    }

    private void KeepInPlace()
    {
        for (int i=0; i<textsList.Count; i++)
        {
            Vector3 newPos = TargetPosition(targetsList[i]);

            if (textsList[i].position != newPos)
            {
                textsList[i].position = newPos;
            }
        }
    }

    public void CreateNewText(GameObject targetObj)
    {
        GameObject newText = Instantiate(inGameTextPrefab, Vector3.zero, Quaternion.identity, this.transform);
        newText.transform.position = TargetPosition(targetObj.transform);

        string str = "=";
        int num = 1;

        if (targetObj.GetComponent<Pedestal>())
        {
            str = "=";
            num = targetObj.GetComponent<Pedestal>().myInt;
        }
        else if (targetObj.GetComponent<AssignmentMachine>())
        {
            str = "=";
            num = targetObj.GetComponent<AssignmentMachine>().myInt;
        }
        else if (targetObj.GetComponent<OperationMachine>())
        {
                num = targetObj.GetComponent<OperationMachine>().myInt;
                switch (targetObj.GetComponent<OperationMachine>().myOperationType)
                {
                    case OperationMachine.OperationType.Addition:
                        str = "+";
                        break;
                    case OperationMachine.OperationType.Subtruction:
                        str = "-";
                        break;
                    case OperationMachine.OperationType.Multiplication:
                        str = "*";
                        break;
                }
        }

        

        str = str + num.ToString();
        
        if (targetObj.GetComponent<Variaball>())
        {
            if (targetObj.GetComponent<Variaball>().isNull)
            {
                str = "Null";
            }

            //targetObj.GetComponent<Variaball>().myText = newText;
        }
        
        newText.GetComponent<Text>().text = str;

        targetsList.Add(targetObj.transform);
        textsList.Add(newText.transform);
    }

}
