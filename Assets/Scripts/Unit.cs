using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    protected Dictionary<UnitState, FSM> stateMachine;
    protected UnitState currentState;
    protected UnitState checkState = UnitState.Continue;

    public UnitState attackState = UnitState.Attack_1;

    protected float hp;
    protected float damage;
    
    public virtual void Init()
    {
        stateMachine = new Dictionary<UnitState, FSM>();
    }

    public void SetHP(float val)
    {
        hp += val;

        if (hp <= 0)
        {
            ChangeState(UnitState.Die);
        }
    }

    public void ChangeState(UnitState state)
    {
        stateMachine[currentState].Stop();

        currentState = state;
        stateMachine[currentState].Set();
    }

    void AttackState()
    {
        stateMachine[currentState].Stop();

        currentState = attackState;
        stateMachine[currentState].Set();
    }

    public void Attack()
    {
        if (currentState == UnitState.Jump)
            return;
        
        if (currentState == UnitState.Run)
        {
            attackState = UnitState.Attack_1;
            AttackState();
        }
        else
        {
            attackState = stateMachine[currentState].Combo();

            if (attackState != UnitState.Continue)
            {
                AttackState();
            }
        }
    }

    public void Strike()
    {
        if (currentState == UnitState.Jump)
            return;

        if (currentState == UnitState.Run)
        {
            attackState = UnitState.Strike;
            AttackState();
        }
        else
        {
            attackState = stateMachine[currentState].Combo();

            if (attackState != UnitState.Continue)
            {
                AttackState();
            }
        }
    }

    public void Jump()
    {
        if (currentState != UnitState.Run)
            return;

        ChangeState(UnitState.Jump);
    }
        
    public void UpdateUnit()
    {
        checkState = stateMachine[currentState].Play();
        
        if (checkState != UnitState.Continue)
        {
            ChangeState(checkState);
        }
    }

}
