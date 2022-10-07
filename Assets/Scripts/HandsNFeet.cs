using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsNFeet : MonoBehaviour
{
    public GameObject goBlend;
    SkinnedMeshRenderer smr;
    private readonly float blendMax = 0f;
    public float blendCurrent = 100f;
    public float blendSpeed = 100f;
    public bool unfoldBool = false;
    int blendCount;
    void Start() {
        smr = goBlend.GetComponent<SkinnedMeshRenderer>();
        blendCount = smr.sharedMesh.blendShapeCount;
        Debug.Log(blendCount);
    }

    void Update() {
        if (Input.GetButtonDown("Fire2")) {
            unfoldBool = true;
        }

        if (unfoldBool) {
            OpenMouth();
        }
    }

    public void OpenMouth() {
        if (blendCurrent > blendMax) {
            for (int i = 0; i < blendCount; i++) {
                smr.SetBlendShapeWeight(i, blendCurrent);
            }
            blendCurrent -= blendSpeed * Time.deltaTime;
        }
    }
}
