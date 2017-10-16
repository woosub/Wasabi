using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Strike : FSM
{
    GameObject mObj;
    Animation ani;

    string aniName;

    Vector3 originPos;
    Vector3 nextPos;

    float time;
    bool effectFlag = false;

    Renderer renderer;

    public Unit_Strike(GameObject obj)
    {
        mObj = obj;
        ani = mObj.transform.GetChild(0).GetComponent<Animation>();

        aniName = WasabiStrings.Animation.Strike;

        ani[aniName].speed = 6;

        renderer = mObj.transform.GetChild(0).Find("Wasabi_").GetComponent<Renderer>();
        
    }

    public void Set()
    {
        ani.Play(aniName);

        originPos = mObj.transform.parent.position;
        nextPos = originPos + new Vector3(1.6f, 0, 0);

        time = 0.0f;
        effectFlag = false;

        renderer.material.SetColor("_Color2", Color.yellow);
        //mObj.GetComponent<Character>().TestCameraColor(mObj.GetComponent<Character>().testColor);
    }

    public UnitState Play()
    {
        time += Time.deltaTime * 10;

        mObj.transform.parent.position = Vector3.Lerp(originPos, nextPos, time);

        if (time >= 0.2f)
        {
            if (effectFlag == false)
            {
                effectFlag = true;
                renderer.material.SetFloat("_Strike", 1);
                renderer.material.SetFloat("_StrikeVal", 1);
            }
        }

        if (effectFlag)
        {
            if (time >= 1.5f)
            {
                renderer.material.SetFloat("_StrikeVal", Mathf.Clamp01(1 - (time / 3f)));
            }
        }

        if (time >= 3.0f)
        {
            return UnitState.Run;
        }

        return UnitState.Continue;
    }

    public UnitState Combo()
    {
        if (time >= 2.3f)
        {
            return UnitState.Strike;
        }

        return UnitState.Continue;
    }

    public void Stop()
    {
        renderer.material.SetColor("_Color2", Color.black);
        renderer.material.SetFloat("_Strike", 0);
        mObj.GetComponent<Character>().TestCameraColor(mObj.GetComponent<Character>().defaultColor);
    }
}
