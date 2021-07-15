using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variaball : MonoBehaviour
{
    [Header("Change")]
    [SerializeField] public bool isNull = false;
    [SerializeField] public int myInt = 0;  

    [Space(10)]
    [Header("Do Not Change")]
    [SerializeField] public int myBlockIndex = 0;
    
    [SerializeField] Player player = null;
    [SerializeField] LevelCreator levelCreator = null;
    //public bool isHeld = false;
    //public GameObject myText = null;

    private int id;
    
    void Start()
    {
        levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
    }

    void Update()
    {
        
    }
    
    public void GetPlayer()
    {
        player = FindObjectOfType<Player>();
    }
    public int GetID()
    {
        return id;
    }

    public void SetID(int ID)
    {
        id = ID;
    }

    public int GetIntValue()
    {
        return myInt;
    }

    public void GoToPlayer()
    {
        //isHeld = true;
        //myText.SetActive(false);

        //player = FindObjectOfType<Player>();
        
        transform.parent = player.transform;

        //Vector3 newPos = new Vector3(0, Player.Instance.transform.position.y + Player.Instance.GetComponent<MeshRenderer>().bounds.extents.y + GameManager.Instance.variaballYOffset, 0);
        //Vector3 newPos = new Vector3(0, Player.Instance.transform.position.y + Player.Instance.GetComponent<Collider>().bounds.extents.y + GameManager.Instance.variaballYOffset, 0);
        Vector3 newPos = new Vector3(0, player.transform.position.y + GameManager.Instance.variaballYGeneralOffset, 0);
        
        
        transform.localPosition = newPos;
        //transform.localPosition = newPos;

        player.myVariaball = this;


        UI_Manager.Instance.VariaballHeld(true);
    }

    public void GoToBlock(Block block)
    {
        //isHeld = false;
        //myText.SetActive(true);

        transform.parent = levelCreator.variaballsParent;
        
        var newPos = block.transform.position + new Vector3(0, block.GetComponentInChildren<MeshRenderer>().bounds.extents.y + GameManager.Instance.variaballYGeneralOffset, 0);
        transform.position = newPos;
        block.myRole = Block.roles.ContainsVeriaball;
        block.myVariaball = this;

        UI_Manager.Instance.VariaballHeld(false);
    }

    public void GoToPedestal(Pedestal pedestal)
    {
        //isHeld = false;
        //myText.SetActive(true);

        transform.parent = levelCreator.variaballsParent;
        
        //var newPos = pedestal.transform.position + new Vector3(0, pedestal.GetComponent<MeshRenderer>().bounds.extents.y + GameManager.Instance.variaballYOffset, 0);
        var newPos = pedestal.transform.position + new Vector3(0, GameManager.Instance.variaballYPedestalOffset, 0.262f);
        transform.position = newPos;
        pedestal.GetVariaball(this);

        UI_Manager.Instance.VariaballHeld(false);

    }
}
