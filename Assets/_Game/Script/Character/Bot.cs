using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    public bool isBuff = false;
    IState<Bot> currentState;
    private Vector3 destionation;
    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;


    //protected override void Start()
    //{
    //    base.Start();
    //    ChangeState(new PatrolState());
    //}

    public override void OnInit()
    {
        base.OnInit();
        isBuff = false;
        
    }

    public void SetDestination(Vector3 position)
    {
        agent.isStopped = false;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(position);
    }
    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            currentState.OnExcute(this);
            //check stair
            CanMove(TF.position);
        }
        else if (GameManager.Instance.IsState(GameState.Pause))
        {
            MoveStop();
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void MoveStop()
    {
        agent.isStopped = true;
    }
}
