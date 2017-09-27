using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    const float jumpPower = 8.0f;
    const float gravity = 18.0f;

    bool isJump = false;
    bool isAttack = false;

    Animation mAni;

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

    // Use this for initialization
    void Start () {
        Application.runInBackground = true;

        mAni = GetComponentInChildren<Animation>();

        shadowLOriginPos = ShadowL.transform.localPosition;
        shadowROriginPos = ShadowR.transform.localPosition;

        ShadowLRender = ShadowL.GetComponent<Renderer>();
        ShadowRRender = ShadowR.GetComponent<Renderer>();

        defaultShadowColor = Color.black;
    }

    // Update is called once per frame
    void Update() {

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

    public void Jump()
    {
        if (isJump)
            return;

        isJump = true;

        StartCoroutine(Jumping());
    }

    IEnumerator Jumping()
    {
        float jumpVal = jumpPower;

        while (true)
        {
            jumpVal -= gravity * Time.deltaTime;

            transform.position += new Vector3(0, jumpVal * Time.deltaTime);

            if (transform.localPosition.y <= 0.0f)
            {
                break;
            }

            yield return null;
        }

        transform.localPosition = Vector3.zero;
        isJump = false;
    }

    public void Attack()
    {
        isAttack = true;
        //mAni.CrossFade()
    }
}
