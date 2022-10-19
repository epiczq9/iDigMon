using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeChange : MonoBehaviour {
    public GameObject blendGO;
    SkinnedMeshRenderer smr;
    private readonly float blendMax = 100f;
    public float blendCurrent1 = 0, blendCurrent2 = 0, blendCurrent3 = 0;
    public float blendSpeed = 200f;
    public bool doFirstSwipe = false, doSecondSwipe = false, doThirdSwipe = false;
    int blendCount;
    void Start() {
        smr = blendGO.GetComponent<SkinnedMeshRenderer>();
        blendCount = smr.sharedMesh.blendShapeCount;
    }

    // Update is called once per frame
    void Update() {

        if (doFirstSwipe) {
            FirstSwipe();
        }
        if (doSecondSwipe) {
            SecondSwipe();
        }
        if (doThirdSwipe) {
            ThirdSwipe();
        }

    }
    public void FirstSwipe() {
        if (blendCurrent1 < blendMax) {
            smr.SetBlendShapeWeight(0, blendCurrent1);
            blendCurrent1 += blendSpeed * Time.deltaTime;
        } else {
            doFirstSwipe = false;
            smr.SetBlendShapeWeight(0, blendMax);
        }
    }

    public void SecondSwipe() {
        if (blendCurrent2 < blendMax) {
            smr.SetBlendShapeWeight(0, blendCurrent1);
            blendCurrent1 -= blendSpeed * Time.deltaTime;

            smr.SetBlendShapeWeight(1, blendCurrent2);
            blendCurrent2 += blendSpeed * Time.deltaTime;
        } else {
            smr.SetBlendShapeWeight(0, 0f);
            smr.SetBlendShapeWeight(1, blendMax);
            doSecondSwipe = false;
        }
    }
    public void ThirdSwipe() {
        if (blendCurrent3 < blendMax) {
            smr.SetBlendShapeWeight(1, blendCurrent2);
            blendCurrent2 -= blendSpeed * Time.deltaTime;

            smr.SetBlendShapeWeight(2, blendCurrent3);
            blendCurrent3 += blendSpeed * Time.deltaTime;
        } else {
            smr.SetBlendShapeWeight(1, 0f);
            smr.SetBlendShapeWeight(2, blendMax);
            doThirdSwipe = false;
        }
    }
}
