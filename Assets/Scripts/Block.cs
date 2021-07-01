using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum roles 
    {
        Nothing,
        Path,
        StartPosition,
        EndPosition,
        Gate_Open,
        Gate_Closed,
        Switch,
        ContainsVeriaball,
        OperationMachine,
        AssignmentMachine,
        Pedestal
    }

    [SerializeField] public int myIndex;
    [HideInInspector] public GameObject  childRole = null;
    public bool isWalkable = false;
    [HideInInspector] public Variaball myVariaball = null;
    [HideInInspector] public Pedestal myPedestal = null;
    [HideInInspector] public GameObject myMachine = null;

    [Header("Change")]
    [SerializeField] public roles myRole = roles.Nothing;
    
    private LevelCreator lc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void CleanSelf()
    {
        //myRole = roles.Nothing;

        if (childRole != null)
        {
        DestroyImmediate(childRole);
        }

        GetComponentInChildren<MeshRenderer>().enabled = true;

        myVariaball = null;

    }

    public void SetRole()
    {
        lc = FindObjectOfType<LevelCreator>();
        
        CleanSelf();
        
        if (myRole != roles.Nothing)
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        switch(myRole)
        {
            case roles.Path:
                childRole = Instantiate(lc.pathPrefab, transform.position, Quaternion.identity, transform);
                break;
            case roles.Gate_Open:
                childRole = Instantiate(lc.gatePrefab, transform.position, Quaternion.identity, transform);
                childRole.GetComponent<Gate>().OpenGate();
                break;
            case roles.Gate_Closed:
                childRole = Instantiate(lc.gatePrefab, transform.position, Quaternion.identity, transform);
                childRole.GetComponent<Gate>().CloseGate();
                break;
            case roles.Pedestal:
                //SpawnPedestal();
                //GetComponentInChildren<MeshRenderer>().enabled = true;
                childRole = Instantiate(lc.pedestalPrefab, transform.position, Quaternion.identity, transform);
                myPedestal = childRole.GetComponent<Pedestal>();
                break;
            case roles.ContainsVeriaball:
                SpawnVariaball();
                GetComponentInChildren<MeshRenderer>().enabled = true;
                break;
            case roles.OperationMachine:
                childRole = Instantiate(lc.operationMachinePrefab, transform.position, Quaternion.identity, transform);
                myMachine = childRole;
                break;
            case roles.AssignmentMachine:
                childRole = Instantiate(lc.assignmentMachinePrefab, transform.position, Quaternion.identity, transform);
                myMachine = childRole;
                break;
            case roles.Switch:
                childRole = Instantiate(lc.switchPrefab, transform.position, Quaternion.identity, transform);
                break;
            case roles.StartPosition:
                childRole = Instantiate(lc.pathPrefab, transform.position, Quaternion.identity, transform);
                LevelManager lm = FindObjectOfType<LevelManager>();
                lm.playerStartBlockIndex = myIndex;
                break;
            case roles.EndPosition:
                childRole = Instantiate(lc.EndPositionPrefab, transform.position, Quaternion.identity, transform);
                break;
            default:
                break;
        }
    }

    private void SpawnVariaball()
    {
        LevelCreator lc = FindObjectOfType<LevelCreator>();
        GameManager gm = FindObjectOfType<GameManager>();
        LevelManager lm = FindObjectOfType<LevelManager>();

        Vector3 spawnPos = transform.position + new Vector3(0, GetComponentInChildren<MeshRenderer>().bounds.extents.y + gm.variaballYGeneralOffset, 0);

        Variaball variaball = Instantiate(lc.variaballPrefab, spawnPos, Quaternion.identity, lc.variaballsParent).GetComponent<Variaball>();

        myVariaball = variaball;
        lm.variaballsList.Add(variaball);


        variaball.myBlockIndex = myIndex;
    }

    /*
    private void SpawnPedestal()
    {
        LevelCreator lc = FindObjectOfType<LevelCreator>();
        GameManager gm = FindObjectOfType<GameManager>();
        LevelManager lm = FindObjectOfType<LevelManager>();

        Vector3 spawnPos = transform.position + new Vector3(0, GetComponentInChildren<MeshRenderer>().bounds.extents.y + lc.pedestalPrefab.GetComponentInChildren<MeshRenderer>().bounds.extents.y, 0);

        Pedestal pedestal = Instantiate(lc.pedestalPrefab, spawnPos, Quaternion.identity, lc.pedestalsParent).GetComponent<Pedestal>();
        
        myPedestal = pedestal;
        lm.pedestalsList.Add(pedestal);

        pedestal.myBlockIndex = myIndex;

    }
*/

}
