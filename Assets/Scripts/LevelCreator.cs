using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [Header("Level Grid Size")]
    [SerializeField] public int levelGridX = 10;
    [SerializeField] public int levelGridY = 10;

    //[SerializeField] private List<Block> blocksList = new List<Block>();
    [Space(10)]
    [Header("Moves")]
    [SerializeField] public int levelMaxMoves = 10;
    
    [Space(10)]
    [Header("Level Buttons")]
    [SerializeField] public bool directionsButtonsEnabled = true;
    [SerializeField] public bool forButtonEnabled = true;
    [SerializeField] public bool pressButtonEnabled = true;
    [SerializeField] public bool pickupDropButtonEnabled = true;
    [SerializeField] public bool useMachineButtonEnabled = true;
    


    [Space(10)]
    [Header("Prefabs")]
    
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public GameObject emptyBlockPrefab = null;
    [SerializeField] public GameObject pathPrefab = null;
    [SerializeField] public GameObject switchPrefab = null;
    //[SerializeField] public GameObject pedestalPrefab = null;
    [SerializeField] public GameObject pedestalPrefab = null;
    [SerializeField] public GameObject gatePrefab = null;
    [SerializeField] public GameObject operationMachinePrefab = null;
    [SerializeField] public GameObject assignmentMachinePrefab = null;
    [SerializeField] public GameObject EndPositionPrefab = null;
    [SerializeField] public GameObject variaballPrefab = null;

    [Space(10)]
    [Header("Materials")]
    [SerializeField] private List<Material> connectionLightsMaterial = new List<Material>();
    [HideInInspector] public List<Material> freeConnectionLightsMaterial = new List<Material>();
    
    [Space(10)]
    [Header("Fathers")]
    [SerializeField] private Transform blocksParent = null;
    [SerializeField] public Transform variaballsParent = null;
    //[SerializeField] public Transform pedestalsParent = null;

    private void Start() {
        //SetConnectionLights();
    }

    public void CreateLevelGrid()
    {
        ClearLevelGrid();
        
        int count = 0;

        LevelManager lm = FindObjectOfType<LevelManager>();
        
        Vector3 firstBlockPos = new Vector3(-((float)levelGridX / 2) + (emptyBlockPrefab.GetComponentInChildren<MeshRenderer>().bounds.extents.x), 0, -((float)levelGridY / 2) + (emptyBlockPrefab.GetComponentInChildren<MeshRenderer>().bounds.extents.x));
        Vector3 BlockPos = firstBlockPos;

        for (int i=0; i<levelGridY; i++)
        {
            for (int j=0; j<levelGridX; j++)
            {
                var newBlock = Instantiate(emptyBlockPrefab, BlockPos, Quaternion.identity,blocksParent).GetComponent<Block>();
                lm.blocksList.Add(newBlock);
                newBlock.myIndex = count;
                count++;

                BlockPos.x += (emptyBlockPrefab.GetComponentInChildren<MeshRenderer>().bounds.size.x);
            }
            BlockPos.x = firstBlockPos.x;
            BlockPos.z += (emptyBlockPrefab.GetComponentInChildren<MeshRenderer>().bounds.size.x);
        }
    }

    public void ClearLevelGrid()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
        
        foreach(Block block in lm.blocksList)
        {
            DestroyImmediate(block.gameObject);
        }

        lm.blocksList.Clear();

        //InGameUIManager.Instance.ClearMatrixes();

        ClearVariaballs();
        //ClearPedestals();
    }
    
    public void ClearBlocksRoles()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
        
        foreach(Block block in lm.blocksList)
        {
            block.myRole = Block.roles.Nothing;
            block.CleanSelf();
        }

        ClearVariaballs();
        //ClearPedestals();
    }

    public void SetBlocksType()
    {   
        ClearVariaballs();
        //ClearPedestals();

        LevelManager lm = FindObjectOfType<LevelManager>();
        
        foreach(Block block in lm.blocksList)
        {
            block.SetRole();
        }
    }

    private void ClearVariaballs()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();

        foreach(Variaball variaball in lm.variaballsList)
        {
            DestroyImmediate(variaball.gameObject);
        }
        lm.variaballsList.Clear();
    }
    
    /*
    private void ClearPedestals()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();

        foreach(Pedestal pedestal in lm.pedestalsList)
        {
            DestroyImmediate(pedestal.gameObject);
        }
        lm.pedestalsList.Clear();
    }
    */

    public void SetConnectionLights()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
        
        freeConnectionLightsMaterial.Clear();
        
        for(int i=0; i<connectionLightsMaterial.Count; i++)
        {
            freeConnectionLightsMaterial.Add(connectionLightsMaterial[i]);
        }

        
        foreach(Block block in lm.blocksList)
        {
            if (block.myRole == Block.roles.Gate_Open || block.myRole == Block.roles.Gate_Closed)
            {
                block.GetComponentInChildren<Gate>().activeConnections = 0;
            }
            
            if (block.myRole == Block.roles.Switch)
            {
                Switch sw = block.GetComponentInChildren<Switch>();
                sw.activeConnections = 0;


                for (int i=0; i<Mathf.Min(block.GetComponentInChildren<Switch>().relatedGates.Count, 4); i++)
                {
                    Material mat = GetRandomLightMaterial();
                    
                    sw.myConnectionLights[block.GetComponentInChildren<Switch>().activeConnections].GetComponent<Renderer>().material = mat;
                    sw.activeConnections++;

                    Gate gate = block.GetComponentInChildren<Switch>().relatedGates[i];
                    gate.myConnectionLights[gate.activeConnections].GetComponent<Renderer>().material = mat;
                    gate.activeConnections++;
                }
            }
            if (block.myRole == Block.roles.Pedestal)
            {
                Pedestal pd = block.GetComponentInChildren<Pedestal>();
                pd.activeConnections = 0;


                for (int i=0; i<Mathf.Min(block.GetComponentInChildren<Pedestal>().relatedGates.Count, 4); i++)
                {
                    Material mat = GetRandomLightMaterial();
                    
                    pd.myConnectionLights[block.GetComponentInChildren<Pedestal>().activeConnections].GetComponent<Renderer>().material = mat;
                    pd.activeConnections++;

                    Gate gate = block.GetComponentInChildren<Pedestal>().relatedGates[i];
                    gate.myConnectionLights[gate.activeConnections].GetComponent<Renderer>().material = mat;
                    gate.activeConnections++;
                }
            }
        }
    }

    private Material GetRandomLightMaterial()
    {
        int num = Random.Range(0, freeConnectionLightsMaterial.Count);
        
        Material mat = freeConnectionLightsMaterial[num];

        freeConnectionLightsMaterial.RemoveAt(num);

        return mat;
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //LineTest();
        }
    }
    /*
    private void LineTest()
    {
        Debug.Log("line");
        
        var tra1 = LevelManager.Instance.blocksList[0].transform;
        var tra2 = LevelManager.Instance.blocksList[16].transform;

        LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        lineRenderer.SetPosition(0, new Vector3(tra1.position.x, tra1.position.y + 2, tra1.position.z));
        lineRenderer.SetPosition(1, new Vector3(tra2.position.x, tra2.position.y + 2, tra2.position.z));

    }
    */
}
