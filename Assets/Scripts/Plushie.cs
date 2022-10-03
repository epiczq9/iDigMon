using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plushie : MonoBehaviour
{
    public GameObject plushie;
    SkinnedMeshRenderer smr;
    private float blendMax = 100f;
    public float blendCurrent = 0f;
    public float blendSpeed = 100f;
    bool openMouthBool = false;
    int blendCount;
    void Start() {
        smr = plushie.GetComponent<SkinnedMeshRenderer>();
        blendCount = smr.sharedMesh.blendShapeCount;
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            openMouthBool = true;
        }

        if (openMouthBool) {
            OpenMouth();
        }
    }

    public void OpenMouth() {
        if (blendCurrent < blendMax) {
            for(int i = 0; i > blendCount; i++) {
                smr.SetBlendShapeWeight(i, blendCurrent);
            }
            blendCurrent += blendSpeed * Time.deltaTime;
        }
    }
}
