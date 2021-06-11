using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    
    private void OnEnable()
    {
        LevelManager.OnLevelEnd += Close;
        Player.OnWalk += Walk;
        Player.OnUse += Use; 
        Player.OnStopWalking += Idle;
    }

    private void OnDisable() 
    {
        LevelManager.OnLevelEnd -= Close;
        Player.OnWalk -= Walk;
        Player.OnUse -= Use;     
        Player.OnStopWalking -= Idle;
    }


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Walk();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Idle();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Use();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Close();
        }
   }

    private void Walk()
    {
        anim.SetTrigger("Walk");
    }

    private void Idle()
    {
        anim.SetTrigger("Idle");
    }

    private void Use()
    {
        anim.SetTrigger("Use");

    }
    private void Close()
    {
        anim.SetTrigger("Close");

    }
}
