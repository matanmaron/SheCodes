using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static event Action OnAnyAction;
    public static event Action OnWalk;
    public static event Action OnStopWalking;
    public static event Action OnPickUpVariaball;
    public static event Action OnDropVariaball;
    public static event Action OnPlaceVariaballOnPedestal;
    public static event Action OnIllegalAction;
    public static event Action OnUse;
    public static event Action<int> OnUseAsMachine;
    public static event Action<OperationMachine.OperationType, int>  OnUseOpMachine;
    public static event Action OnPressSwitch;

    public enum PlayerMoves { ForWalk, ForMachine, Up, Down, Left, Right, Pickup, Drop, Press, AssignMachine, Ball, AddMachine, SubMachine, MultMachine}
    public static event Action<PlayerMoves, int> OnPlayerAction;
    private const int MOVECOST = 1;

    public enum Direction {up, down, left, right}
    public enum State {Idle, Moving, PickingUp, Dropping, PressingSwitch, UsingMachine}
    [SerializeField] public State state = State.Idle;


    [SerializeField] private int currentBlockIndex;

    private Vector3 movementStartPos = new Vector3();
    private Vector3 movementEndPos = new Vector3();
    private float moveStartTime;
    private float moveEndTime;
    private Direction moveDirection;
    [SerializeField] private int stepsCounter = 1;
    [SerializeField] public Variaball myVariaball = null;

    void Start()
    {
        currentBlockIndex = GameManager.Instance.LevelManager.playerStartBlockIndex;

        TurnTowardsCamera();
    }

    void Update()
    {
        if (state == State.Moving)
        {
            Move();
        }
    }

    public void DirectionPressed(int directionNum)
    {
        switch (directionNum)
        {
            case 0:
                moveDirection = Direction.up;
                break;
            case 1:
                moveDirection = Direction.down;
                break;
            case 2:
                moveDirection = Direction.left;
                break;
            case 3:
                moveDirection = Direction.right;
                break;
        }

        TurnTowardsAction(moveDirection);
        
        switch (state)
        {
            case State.Idle:
                StartMoving();
                break;
            case State.PickingUp:
                PickUpVariaball();
                break;
            case State.Dropping:
                DropVariaball();
                break;
            case State.PressingSwitch:
                PressSwitch();
                break;
            case State.UsingMachine:
                UseMachine();
                break;
        }
    }
    
    private void TurnTowardsCamera()
    {
        transform.rotation = Quaternion.Euler(Vector3.down * 180);
    }

    public void TurnTowardsAction(Player.Direction direction)
    {
        Vector3 dir = Vector3.back;

        switch (direction)
        {
            case Player.Direction.up:
                dir = new Vector3(0,0,0);
                break;
            case Player.Direction.down:
                dir = new Vector3(0,-180,0);

                break;
            case Player.Direction.left:
                dir = new Vector3(0,-90,0);
                break;
            case Player.Direction.right:
                dir = new Vector3(0,90,0);

                break;
        }

        transform.rotation = Quaternion.Euler(dir);
    }
    
    private void Move()
    {
        var relativeTime = Mathf.InverseLerp(moveStartTime, moveEndTime, Time.time);

        var newXPos = Mathf.Lerp(movementStartPos.x, movementEndPos.x, relativeTime);
        var newZPos = Mathf.Lerp(movementStartPos.z, movementEndPos.z, relativeTime);
        var newPos = new Vector3(newXPos, transform.position.y, newZPos);

        transform.position = newPos;
        

        if (transform.position == movementEndPos)
        {
            stepsCounter--;

            if (stepsCounter == 0)
            {
                
                state = State.Idle;

                OnStopWalking?.Invoke();

                ForHandler.Instance.ResetFor();
                if (isLevelEnd())
                {
                    GameManager.Instance.LevelManager.EndLevel();
                }
             
            
            }
            else
            {
                movementStartPos = transform.position;

                int newBlockIndex = 0;

                switch (moveDirection)
                {
                case Direction.up:
                    newBlockIndex = currentBlockIndex + GameManager.Instance.LevelManager.levelGridX;
                    Debug.Log(newBlockIndex);
                    break;
                case Direction.down:
                    newBlockIndex = currentBlockIndex - GameManager.Instance.LevelManager.levelGridX;
                    break;
                case Direction.left:
                    newBlockIndex = currentBlockIndex - 1;
                    break;
                case Direction.right:
                    newBlockIndex = currentBlockIndex + 1;
                    break;
                default:
                    break;
                }
                
                movementEndPos = GameManager.Instance.LevelManager.blocksList[newBlockIndex].transform.position + new Vector3(0, GameManager.Instance.LevelManager.blocksList[newBlockIndex].GetComponentInChildren<MeshRenderer>().bounds.extents.y + GameManager.Instance.playerYOffset, 0);

                currentBlockIndex = newBlockIndex;
                
                moveStartTime = Time.time;
                moveEndTime = moveStartTime + GameManager.Instance.playerMoveDuration;
            }
        }

    }

    
    public void StartMoving() 
    {
        if (state == State.Moving || state == State.PickingUp || state == State.Dropping)
        {
            return;
        }

        stepsCounter = ForHandler.Instance.forNum;
        if(stepsCounter > 1)
        {
            OnPlayerAction?.Invoke(PlayerMoves.ForWalk, stepsCounter);
        }
        
        if (isBlockWalkable(moveDirection))
        {
            OnAnyAction?.Invoke();
            OnWalk?.Invoke();
            movementStartPos = transform.position;

            int newBlockIndex = 0;

            switch (moveDirection)
            {
            case Direction.up:
                newBlockIndex = currentBlockIndex + GameManager.Instance.LevelManager.levelGridX;
                    OnPlayerAction?.Invoke(PlayerMoves.Up, MOVECOST);
                    break;
            case Direction.down:
                newBlockIndex = currentBlockIndex - GameManager.Instance.LevelManager.levelGridX;
                    OnPlayerAction?.Invoke(PlayerMoves.Down, MOVECOST);
                    break;
            case Direction.left:
                newBlockIndex = currentBlockIndex - 1;
                    OnPlayerAction?.Invoke(PlayerMoves.Left, MOVECOST);
                    break;
            case Direction.right:
                newBlockIndex = currentBlockIndex + 1;
                    OnPlayerAction?.Invoke(PlayerMoves.Right, MOVECOST);
                    break;
            default:
                break;
            }
            
            movementEndPos = GameManager.Instance.LevelManager.blocksList[newBlockIndex].transform.position + new Vector3(0, GameManager.Instance.LevelManager.blocksList[newBlockIndex].GetComponentInChildren<MeshRenderer>().bounds.extents.y + GameManager.Instance.playerYOffset, 0);

            currentBlockIndex = newBlockIndex;
            
            moveStartTime = Time.time;
            moveEndTime = moveStartTime + GameManager.Instance.playerMoveDuration;
            
            state = State.Moving;
        }
    }

    private bool isBlockWalkable(Direction direction)
    {
        int newBlockIndex = currentBlockIndex;

        for (int i=0; i<ForHandler.Instance.forNum; i++)
        {
            switch (direction)
            {
            case Direction.up:
                if (currentBlockIndex + GameManager.Instance.LevelManager.levelGridX > GameManager.Instance.LevelManager.levelGridY * GameManager.Instance.LevelManager.levelGridX)
                {
                    OnIllegalAction?.Invoke();
                    return false;
                }
                else
                {
                    newBlockIndex += GameManager.Instance.LevelManager.levelGridX;
                }
                break;
            case Direction.down:
                if (currentBlockIndex - GameManager.Instance.LevelManager.levelGridX < 0)
                {
                    OnIllegalAction?.Invoke();
                    return false;
                }
                else
                {
                    newBlockIndex -= GameManager.Instance.LevelManager.levelGridX;
                }
                break;
            case Direction.left:
                if (currentBlockIndex % GameManager.Instance.LevelManager.levelGridX == 0)
                {
                    OnIllegalAction?.Invoke();
                    return false;
                }
                else
                {
                    newBlockIndex -= 1;
                }
                break;
            case Direction.right:
                if (currentBlockIndex + 1 % GameManager.Instance.LevelManager.levelGridX == 0)
                {
                    OnIllegalAction?.Invoke();
                    return false;
                }
                else
                {
                    newBlockIndex += 1;
                }
                break;
            default:
                break;
            }
        
            if (GameManager.Instance.LevelManager.blocksList[newBlockIndex].isWalkable == false)
            {
                OnIllegalAction?.Invoke();
                return false;
            }
        }

        return true;
    }

    private bool isLevelEnd()
    {
        if (GameManager.Instance.LevelManager.blocksList[currentBlockIndex].GetComponentInChildren<EndPosition>())
        {
            return true;
        }
        return false;
    }

    public void PressSwitchInitial()
    {
        ForHandler.Instance.ResetFor();
        
        if (state == State.PressingSwitch)
        {
            state = State.Idle;
        }
        else
        {
            state = State.PressingSwitch;
        }
    }


    private void PressSwitch()
    {
        Switch switchBtn = AvailableSwitch(moveDirection);
        if (switchBtn)
        {
            OnAnyAction?.Invoke();
            OnUse?.Invoke();
            OnPressSwitch?.Invoke();
            switchBtn.PressSwitch();
            OnPlayerAction?.Invoke(PlayerMoves.Press, 0);
        }
        
        state = State.Idle;
    }

    public Switch AvailableSwitch(Direction direction)
    {
        int blockIndex = 0;
        
        switch (direction)
        {
            case Direction.up:
                if (currentBlockIndex + GameManager.Instance.LevelManager.levelGridX <= GameManager.Instance.LevelManager.levelGridY * GameManager.Instance.LevelManager.levelGridX)
                {
                    blockIndex = currentBlockIndex + GameManager.Instance.LevelManager.levelGridX;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Switch)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex].GetComponentInChildren<Switch>();
                    }
                }

                break;
            case Direction.down:
                if (currentBlockIndex - GameManager.Instance.LevelManager.levelGridX >= 0)
                {
                    blockIndex = currentBlockIndex - GameManager.Instance.LevelManager.levelGridX;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Switch)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex].GetComponentInChildren<Switch>();
                    }
                }
                break;
            case Direction.left:
                if (currentBlockIndex % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    blockIndex = currentBlockIndex - 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Switch)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex].GetComponentInChildren<Switch>();          
                    }
                }

                break;
            case Direction.right:
                if (currentBlockIndex + 1 % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    blockIndex = currentBlockIndex + 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Switch)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex].GetComponentInChildren<Switch>();
                    }
                }
                break;
        }
        
        OnIllegalAction?.Invoke();
        return null;
    }

    public void PickUpOrDropInitial()
    {
        ForHandler.Instance.ResetFor();
        
        if (state == State.PickingUp || state == State.Dropping)
        {
            state = State.Idle;
        }
        else
        {
            if (!myVariaball)
            {
                state = State.PickingUp;
            }
            else
            {
                state = State.Dropping;
            }
        }
    }
    
    
    private void PickUpVariaball()
    {
        Block originBlock = availableVariaballBlock(moveDirection);
        
        
        if (originBlock)
        {
            switch(originBlock.myRole)
            {
                
                case Block.roles.ContainsVeriaball:
                    OnAnyAction?.Invoke();
                    OnPickUpVariaball?.Invoke();
                    OnPlayerAction?.Invoke(PlayerMoves.Pickup, originBlock.myVariaball.GetIntValue());
                    OnPlayerAction?.Invoke(PlayerMoves.Ball, originBlock.myVariaball.GetID());
                    originBlock.myVariaball.GoToPlayer();
                    originBlock.myVariaball = null;
                    originBlock.myRole = Block.roles.Nothing;

                    //UI_Manager.Instance.VariaballHeld(true);

                    break;
                case Block.roles.Pedestal:
                    if (originBlock.myPedestal.myVariaball)
                    {
                        OnAnyAction?.Invoke();
                        OnPickUpVariaball?.Invoke();
                        OnPlayerAction?.Invoke(PlayerMoves.Pickup, originBlock.myPedestal.myVariaball.GetIntValue());
                        OnPlayerAction?.Invoke(PlayerMoves.Ball, originBlock.myPedestal.myVariaball.GetID());
                        originBlock.myPedestal.myVariaball.GoToPlayer();
                        originBlock.myPedestal.RemoveVariaball();

                        //UI_Manager.Instance.VariaballHeld(true);
                    }
                    break;
            }
        }
        
            
        state = State.Idle;
    }
    
    
    private Block availableVariaballBlock(Direction direction)
    {
        int blockIndex = 0;
        
        switch (direction)
        {
            case Direction.up:
                if (currentBlockIndex + GameManager.Instance.LevelManager.levelGridX <= GameManager.Instance.LevelManager.levelGridY * GameManager.Instance.LevelManager.levelGridX)
                {
                    blockIndex = currentBlockIndex + GameManager.Instance.LevelManager.levelGridX;
                    //copy start
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.ContainsVeriaball)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                    else if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        if (GameManager.Instance.LevelManager.blocksList[blockIndex].myPedestal.myVariaball)
                        {
                            //OnAction?.Invoke();
                            return GameManager.Instance.LevelManager.blocksList[blockIndex];
                        }
                    }
                    //copy end
                }

                break;
            case Direction.down:
                if (currentBlockIndex - GameManager.Instance.LevelManager.levelGridX >= 0)
                {
                    blockIndex = currentBlockIndex - GameManager.Instance.LevelManager.levelGridX;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.ContainsVeriaball)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                    else if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        if (GameManager.Instance.LevelManager.blocksList[blockIndex].myPedestal.myVariaball)
                        {
                            //OnAction?.Invoke();
                            return GameManager.Instance.LevelManager.blocksList[blockIndex];
                        }
                    }
                }
                break;
            case Direction.left:
                if (currentBlockIndex % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    blockIndex = currentBlockIndex - 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.ContainsVeriaball)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                    else if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        if (GameManager.Instance.LevelManager.blocksList[blockIndex].myPedestal.myVariaball)
                        {
                            //OnAction?.Invoke();
                            return GameManager.Instance.LevelManager.blocksList[blockIndex];
                        }
                    }
                }

                break;
            case Direction.right:
                if (currentBlockIndex + 1 % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    blockIndex = currentBlockIndex + 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.ContainsVeriaball)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                    else if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        if (GameManager.Instance.LevelManager.blocksList[blockIndex].myPedestal.myVariaball)
                        {
                            //OnAction?.Invoke();
                            return GameManager.Instance.LevelManager.blocksList[blockIndex];
                        }
                    }
                }
                break;
        }
        
        OnIllegalAction?.Invoke();
        return null;
    }


    private void DropVariaball()
    {
        Block destinationBlock = availableDropSpot(moveDirection);
        int ballId = myVariaball.GetID();
        Debug.Log(destinationBlock);

        switch (destinationBlock.myRole)
        {
            case Block.roles.Nothing:
                OnAnyAction?.Invoke();
                OnDropVariaball?.Invoke();                
                myVariaball.GoToBlock(destinationBlock);
                myVariaball = null;
                OnPlayerAction?.Invoke(PlayerMoves.Drop, MOVECOST);
                OnPlayerAction?.Invoke(PlayerMoves.Ball, ballId);

                break;
            case Block.roles.Pedestal:
            
                if (destinationBlock.myPedestal.myVariaball == null)
                {
                    OnAnyAction?.Invoke();
                    OnDropVariaball?.Invoke();
                    myVariaball.GoToPedestal(destinationBlock.myPedestal);
                    OnPlaceVariaballOnPedestal?.Invoke();
                    myVariaball = null;
                    OnPlayerAction?.Invoke(PlayerMoves.Drop, MOVECOST);
                    OnPlayerAction?.Invoke(PlayerMoves.Ball, ballId);
                }
                break;
        }
        
        state = State.Idle;
    }

    private Block availableDropSpot(Direction direction)
    {
        int blockIndex = 0;
        
        switch (direction)
        {
            case Direction.up:
                if (currentBlockIndex + GameManager.Instance.LevelManager.levelGridX <= GameManager.Instance.LevelManager.levelGridY * GameManager.Instance.LevelManager.levelGridX)
                {
                    blockIndex = currentBlockIndex + GameManager.Instance.LevelManager.levelGridX;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Nothing || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                }

                break;
            case Direction.down:
                if (currentBlockIndex - GameManager.Instance.LevelManager.levelGridX >= 0)
                {
                    blockIndex = currentBlockIndex - GameManager.Instance.LevelManager.levelGridX;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Nothing || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                }
                break;
            case Direction.left:
                if (currentBlockIndex % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    blockIndex = currentBlockIndex - 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Nothing || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                }

                break;
            case Direction.right:
                Debug.Log("1");
                if (currentBlockIndex + 1 % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    Debug.Log("2");
                    
                    blockIndex = currentBlockIndex + 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Nothing || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.Pedestal)
                    {
                        Debug.Log("3");
                        //OnAction?.Invoke();
                        Debug.Log(GameManager.Instance.LevelManager.blocksList[blockIndex].myRole);
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                }
                break;
        }
        
        OnIllegalAction?.Invoke();
        return null;
    }

    public void UseMachineInitial()
    {
        
        if (state == State.UsingMachine)
        {
            state = State.Idle;
        }
        else
        {
            state = State.UsingMachine;
        }
    }

    private void UseMachine()
    {
        Block machineBlock = AvailableMachine(moveDirection);

        if (machineBlock && myVariaball)
        {
            //OnAction?.Invoke();
            
            switch(machineBlock.myRole)
            {
                
                case Block.roles.AssignmentMachine:
                    AssignmentMachine assignMachine = machineBlock.myMachine.GetComponent<AssignmentMachine>();
                    myVariaball.myInt = assignMachine.UseThisMachine();
                    UI_Manager.Instance.VariaballModified();
                    
                    OnAnyAction?.Invoke();
                    OnUse?.Invoke();
                    OnUseAsMachine?.Invoke(assignMachine.myInt);
                    OnPlayerAction?.Invoke(PlayerMoves.AssignMachine, assignMachine.GetMachineValue());

                    break;
                case Block.roles.OperationMachine:
                    OperationMachine opMachine = machineBlock.myMachine.GetComponent<OperationMachine>();
                    int machineCounter = ForHandler.Instance.forNum;
                    if (machineCounter > 1)
                    {
                        OnPlayerAction?.Invoke(PlayerMoves.ForMachine, machineCounter);
                    }

                    for (int i=0; i< machineCounter; i++)
                    {
                        myVariaball.myInt = opMachine.UseThisMachine(myVariaball.myInt);
              
                    }
                    
                    OnAnyAction?.Invoke();
                    OnUse?.Invoke();
                    OnUseOpMachine?.Invoke(opMachine.myOperationType, opMachine.myInt);

                    switch (opMachine.GetMachineType())
                    {
                        case 1:
                            OnPlayerAction?.Invoke(PlayerMoves.AddMachine, opMachine.GetMachineValue());
                            break;
                        case -1:
                            OnPlayerAction?.Invoke(PlayerMoves.SubMachine, opMachine.GetMachineValue());
                            break;
                        case 0:
                            OnPlayerAction?.Invoke(PlayerMoves.MultMachine, opMachine.GetMachineValue());
                            break;
                    }
                    OnPlayerAction?.Invoke(PlayerMoves.Ball, myVariaball.GetID());
                    UI_Manager.Instance.VariaballModified();
                    break;
            }

            ForHandler.Instance.ResetFor();
        }

  
        state = State.Idle;
    }

    public Block AvailableMachine(Direction direction)
    {
        int blockIndex = 0;
        
        switch (direction)
        {
            case Direction.up:
                if (currentBlockIndex + GameManager.Instance.LevelManager.levelGridX <= GameManager.Instance.LevelManager.levelGridY * GameManager.Instance.LevelManager.levelGridX)
                {
                    blockIndex = currentBlockIndex + GameManager.Instance.LevelManager.levelGridX;
                    //start
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.OperationMachine || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.AssignmentMachine)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                    //end
                }

                break;
            case Direction.down:
                if (currentBlockIndex - GameManager.Instance.LevelManager.levelGridX >= 0)
                {
                    blockIndex = currentBlockIndex - GameManager.Instance.LevelManager.levelGridX;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.OperationMachine || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.AssignmentMachine)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                }
                break;
            case Direction.left:
                if (currentBlockIndex % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    blockIndex = currentBlockIndex - 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.OperationMachine || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.AssignmentMachine)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                }

                break;
            case Direction.right:
                if (currentBlockIndex + 1 % GameManager.Instance.LevelManager.levelGridX > 0)
                {
                    blockIndex = currentBlockIndex + 1;
                    if (GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.OperationMachine || GameManager.Instance.LevelManager.blocksList[blockIndex].myRole == Block.roles.AssignmentMachine)
                    {
                        //OnAction?.Invoke();
                        return GameManager.Instance.LevelManager.blocksList[blockIndex];
                    }
                }
                break;
        }
        
        OnIllegalAction?.Invoke();    
        return null;
    }

}
