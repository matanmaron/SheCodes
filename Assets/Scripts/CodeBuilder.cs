using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public struct Move
{
    public Move(PlayerMoves moveName, int moveValue)
    {
        MoveName = moveName;
        MoveValue = moveValue;      
    }

    public PlayerMoves MoveName { get; private set; }
    public int MoveValue { get; private set; }
}

public class CodeBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    public static CodeBuilder Instance { get; private set; } //singleton

    private GameObject player;
    private List<Move> moves;
    private string code;
    [SerializeField] private LevelManager lvlMngr;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            ResetCodeBuilder();
        }
    }
    private void OnEnable()
    {

        //OnWalk += FunctionName;
        //OnPickUpVariaball += FunctionName;
        //OnDropVariaball += FunctionName;
        //OnUseAsMachine += FunctionName;
        //OnUseOpMachine += FunctionName;
        //OnPressSwitch += FunctionName;

        OnPlayerAction += StoreMove;
    }

    private void OnDisable()
    {
        //OnWalk -= FunctionName;
        //OnPickUpVariaball -= FunctionName;
        //OnDropVariaball -= FunctionName;
        //OnUseAsMachine -= FunctionName;
        //OnUseOpMachine -= FunctionName;
        //OnPressSwitch -= FunctionName;

        OnPlayerAction -= StoreMove;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string BuildCode()
    {
        code = "";
        code += BuildBeginning();
        for (int i=0; i<moves.Count; i++)
        {
            switch (moves[i].MoveName)
            {
                case PlayerMoves.ForWalk:
                    code += BuildForWalk(moves[i], moves[i+1]);
                    Debug.Log("forwalk: " + moves[i].MoveName);
                    Debug.Log("forwalk: " + moves[i + 1].MoveName);
                    i ++;
                    break;
                case PlayerMoves.ForMachine:
                    code += BuildForMachine(moves[i], moves[i + 1],moves[i + 2]);
                    Debug.Log("formachine: " + moves[i].MoveName);
                    Debug.Log("formachine: " + moves[i+1].MoveName);
                    Debug.Log("formachine: " + moves[i+2].MoveName);
                    i +=2;
                    break;
                case PlayerMoves.Up:
                    code += BuildMove(moves[i]);
                    break;
                case PlayerMoves.Down:
                    code += BuildMove(moves[i]);
                    break;
                case PlayerMoves.Left:
                    code += BuildMove(moves[i]);
                    break;
                case PlayerMoves.Right:
                    code += BuildMove(moves[i]);
                    break;
                case PlayerMoves.Press:
                    code += "isSwitch = !isSwitch; \n";
                    break;
                case PlayerMoves.AssignMachine:
                    code += "\n variaball" + moves[i-1].MoveValue + " = " + moves[i].MoveValue + "; \n";
                    break;
                case PlayerMoves.AddMachine:
                    code += BuildOperation(moves[i], moves[i+1]);
                    i++;
                    break;
                case PlayerMoves.SubMachine:
                    code += BuildOperation(moves[i], moves[i + 1]);
                    i++;
                    break;
                case PlayerMoves.MultMachine:
                    code += BuildOperation(moves[i], moves[i + 1]);
                    i++;
                    break;
                default:
                    break;
            }

            if (i == moves.Count)
            {
                    break;
            }
        }
        code += "\n }";
        Debug.Log(code);
        return code;
    }

    public void ResetCodeBuilder()
    {
        moves = new List<Move>();
        code = "";
    }

    public void StoreMove(PlayerMoves move, int value)
    {
        moves.Add(new Move(move, value));
        Debug.Log("move: " + move);
       for (int i = 0; i < moves.Count; i++)
       {
            Debug.Log("move: " + i + " is: " + moves[i].MoveName + " , value is: " + moves[i].MoveValue);
       }
       
    }

    public string BuildBeginning()
    {
        string codeString = "";
        List<Variaball> balls = lvlMngr.GetVariaballsList();
        codeString = "public static void Main() \n { \n Player player = new Player(); \n ";

        if (isForExists())
        {
            codeString += "int i; \n";
        }

        if (isSwitchExists())
        {
            codeString += "bool isSwitch = false; \n"; 
        }

        foreach (Variaball ball in balls)
        {
            codeString += "int variaball" + ball.GetID() + "; \n ";
        }

        return codeString;

      
    }

    public bool isForExists()
    {
        foreach (Move move in moves)
        {
            if (move.MoveName == PlayerMoves.ForMachine || move.MoveName == PlayerMoves.ForWalk)
            {
                return true;
            }
        }
        return false;
    }

    public bool isSwitchExists()
    {
        foreach (Move move in moves)
        {
            if (move.MoveName == PlayerMoves.Press)
            {
                return true;
            }
        }
        return false;
    }

    public string BuildMove(Move DirMove)
    {
        return "\n Player.Move( " + DirMove.MoveName + " );\n";
    }

    public string BuildOperation(Move OpMachine, Move Ball)
    {
        string sign = "";
        switch (OpMachine.MoveName)
        {
            case PlayerMoves.AddMachine:
                sign = "+";
                break;
            case PlayerMoves.SubMachine:
                sign = "-";
                break;
            case PlayerMoves.MultMachine:
                sign = "*";
                break;
        }
        return "\n variaball" + Ball.MoveValue+ " " + sign + " = " + OpMachine.MoveValue + "; \n";
    }

    public string BuildForWalk(Move ForMove, Move DirMove)
    {
        return "\n " +
            "for (i = 0; i < "+ ForMove.MoveValue+ "; i++) \n { "+
            BuildMove(DirMove)+
             " \n }" +
            "\n";
    }

    public string BuildForMachine(Move ForMachine, Move TypeMachine, Move Ball)
    {
        return "\n " +
            "for (i = 0; i < " + ForMachine.MoveValue + "; i++) \n { " +
            BuildOperation(TypeMachine, Ball)+
             " \n }" +
            "\n";
    }
}
