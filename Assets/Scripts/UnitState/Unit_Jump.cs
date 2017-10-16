using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Jump : FSM
{
    bool isJump = false;

    float mJumpPower;
    float mJumpVal;
    float mGravity;
    Transform transform;

    public Unit_Jump(float jumpPower, float gravity, Transform tr)
    {
        mJumpPower = jumpPower;
        mGravity = gravity;
        transform = tr;
        
    }

    public void Set()
    {        
        if (isJump)
            return;

        isJump = true;

        mJumpVal = mJumpPower;
    }

    public UnitState Play()
    {
        transform.parent.position += Time.deltaTime * transform.parent.forward * 4;

        mJumpVal -= mGravity * Time.deltaTime;

        transform.position += new Vector3(0, mJumpVal * Time.deltaTime);

        if (transform.localPosition.y <= 0.0f)
        {
            transform.localPosition = Vector3.zero;
            isJump = false;
            
            return UnitState.Run;
        }

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
