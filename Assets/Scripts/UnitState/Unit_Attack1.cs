using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Attack1 : FSM {

    GameObject mObj;
    Animation ani;

    float time;
    float finishTime;
    
    public Unit_Attack1(GameObject obj)
    {
        mObj = obj;
        ani = mObj.GetComponent<Animation>();
        finishTime = ani[WasabiStrings.Animation.Attack_1].length;
    }

    public void Set()
    {
        time = 0.0f;
        ani.CrossFade(WasabiStrings.Animation.Attack_1, 0.2f);
    }

    public UnitState Play()
    {
        time += Time.deltaTime;

        if (finishTime <= time)
        {
            return UnitState.Run;
        }

        return UnitState.Continue;
    }

    public UnitState Combo()
    {
        if (finishTime * 0.7f <= time)
        {
            return UnitState.Attack_2;
        }

        return UnitState.Continue;
    }

    public void Stop()
    {

    }
}
