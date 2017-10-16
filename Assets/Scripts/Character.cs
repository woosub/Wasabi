using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    const float jumpPower = 8.0f;
    const float gravity = 18.0f;
    
    Animation mAni;

    //camera dummy test
    public Camera mainCam;
    public Color defaultColor;
    public Color testColor;
    //>

    public Transform toeL;
    public Transform toeR;
    public GameObject ShadowL;
    public GameObject ShadowR;

    RaycastHit hit;

    Vector3 shadowSize = new Vector3(0.8f, 1, 1);
    Vector3 shadowLOriginPos;
    Vector3 shadowROriginPos;

    Renderer ShadowLRender;
    Renderer ShadowRRender;

    Color defaultShadowColor;

    GameObject character;

    public void TestCameraColor(Color color)
    {
        mainCam.backgroundColor = color;
    }

    public override void Init()
    {
        base.Init();

        //Test
        defaultColor = mainCam.backgroundColor;
        //>

        character = GameObject.Find(WasabiStrings.Character.Wasabi);

        mAni = GetComponentInChildren<Animation>();

        shadowLOriginPos = ShadowL.transform.localPosition;
        shadowROriginPos = ShadowR.transform.localPosition;

        ShadowLRender = ShadowL.GetComponent<Renderer>();
        ShadowRRender = ShadowR.GetComponent<Renderer>();

        defaultShadowColor = Color.black;
        
        stateMachine.Add(UnitState.Idle, new Unit_Idle());
        stateMachine.Add(UnitState.Run, new Unit_Run(gameObject));
        stateMachine.Add(UnitState.Jump, new Unit_Jump(jumpPower, gravity, transform));
        stateMachine.Add(UnitState.Die, new Unit_Die());
        stateMachine.Add(UnitState.Attack_1, new Unit_Attack1(character));
        stateMachine.Add(UnitState.Attack_2, new Unit_Attack2(character));
        stateMachine.Add(UnitState.Strike, new Unit_Strike(gameObject));
        
        currentState = UnitState.Run;
        ChangeState(currentState);
    }
    
    // Use this for initialization
    void Start () {
        Application.runInBackground = true;

        Init();
    }

    // Update is called once per frame
    void Update() {

        UpdateUnit();

        if (Physics.Raycast(toeL.position, Vector3.down, out hit))
        {
            ShadowL.transform.localScale = shadowSize * (1.0f - Mathf.Clamp01((toeL.position.y - hit.point.y) * 2));
            ShadowLRender.material.SetColor("_Color2", defaultShadowColor - new Color(0, 0, 0, (toeL.position.y - hit.point.y) * 4.5f));
            if (mAni.IsPlaying("Run"))
            {
                ShadowL.transform.position = new Vector3(toeL.position.x - 0.1f, hit.point.y + 0.02f, toeL.position.z);
            }
            else
            {
                ShadowL.transform.position = hit.point + new Vector3(-0.1f, 0.02f);
            }
        }

        if (Physics.Raycast(toeR.position, Vector3.down, out hit))
        {
            ShadowR.transform.localScale = shadowSize * (1.0f - Mathf.Clamp01((toeR.position.y - hit.point.y) * 10));
            ShadowRRender.material.SetColor("_Color2", defaultShadowColor - new Color(0, 0, 0, (toeR.position.y - hit.point.y) * 8));
            if (mAni.IsPlaying("Run"))
            {
                ShadowR.transform.position = new Vector3(toeR.position.x - 0.08f, hit.point.y + 0.02f, toeR.position.z);
            }
            else
            {
                ShadowR.transform.position = hit.point + new Vector3(-0.1f, 0.02f);
            }
        }

    }
}
