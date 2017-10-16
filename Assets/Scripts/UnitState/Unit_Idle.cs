using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Idle : FSM
{

    public void Set()
    {

    }

    public UnitState Play()
    {
        return UnitState.Idle;
    }

    public UnitState Combo()
    {
        return UnitState.Continue;
    }

    public void Stop()
    {

    }
}
