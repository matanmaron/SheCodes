using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pedestal : MonoBehaviour
{
    [Header("Change")]
    [SerializeField] public int myInt = 3;
    [SerializeField] public List<Gate> relatedGates = new List<Gate>();

    [Space(10)]
    [Header("Do Not Change")]
    [SerializeField] public int myBlockIndex = 0;
    public Variaball myVariaball = null;
    [SerializeField] public List<GameObject> myConnectionLights = new List<GameObject>();
    [HideInInspector] public int activeConnections = 0;
    [SerializeField] private TextMeshPro myText = null;

    void Start()
    {
        SetMyText();
    }
    
    public void GetVariaball(Variaball variaball)
    {
        myVariaball = variaball;

        if (myVariaball.myInt == myInt)
        {
            OpenRelatedGates();
        }
    }

    public void RemoveVariaball()
    {
        myVariaball = null;

        CloseRelatedGates();
    }

    private void OpenRelatedGates()
    {
        foreach (Gate gate in relatedGates)
        {
            gate.OpenGate();
        }
    }

    private void CloseRelatedGates()
    {
        foreach (Gate gate in relatedGates)
            {
                gate.CloseGate();
            }
    }

    private void SetMyText()
    {
        myText.text = "==" + myInt.ToString();
    }

}
