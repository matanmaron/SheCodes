using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [Header("Change in Editor")]
    [SerializeField] private bool isOpen = false;

    [Space(10)]
    [Header("Do Not Change")]
    [SerializeField] private Material openMaterial = null;
    [SerializeField] private Material closedMaterial = null;
    [SerializeField] private GameObject materialOrigin = null;
    [SerializeField] public List<GameObject> myConnectionLights = new List<GameObject>();
    [HideInInspector] public int activeConnections = 0;

    
    

    
    void Start()
    {
        SetMyState();

    }

    void Update()
    {
        
    }

    public void ToggleGate()
    {
        if (isOpen)
        {
            CloseGate();
        }
        else
        {
            OpenGate();    
        }

        SetMyState();
    }
    public void OpenGate()
    {
        isOpen = true;

        SetMyState();
    }

    public void CloseGate()
    {
        isOpen = false;

        SetMyState();
    }

    private void SetMyState()
    {
        GetComponentInParent<Block>().isWalkable = isOpen;
        
        if (isOpen)
        {
            materialOrigin.GetComponent<Renderer>().material = openMaterial;
        }
        else
        {
            materialOrigin.GetComponent<Renderer>().material = closedMaterial;
        }
    }

    private void OnDestroy()
    {
        //DestroyImmediate(myConnectionsMatrix.gameObject);
    }
}
