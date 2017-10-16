using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Run : FSM
{
    GameObject mObj;
    Animation ani;

    public Unit_Run(GameObject obj)
    {
        mObj = obj;
        ani = mObj.transform.GetChild(0).GetComponent<Animation>();
    }

    public void Set()
    {
        ani.CrossFade(WasabiStrings.Animation.Run, 0.2f);
    }

    public UnitState Play()
    {
        mObj.transform.parent.position += Time.deltaTime * mObj.transform.parent.forward * 4;
        return UnitState.Continue;
    }

    public UnitState Combo()
    {
        return UnitState.Continue;
    }

    public void Stop()
    {

    }
}
