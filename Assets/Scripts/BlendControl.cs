using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendControl : MonoBehaviour
{
    public GameObject blendGO;
    SkinnedMeshRenderer smr;
    private readonly float blendMax = 0f;
    public float blendCurrent = 100f;
    public float blendSpeed = 100f;
    public bool doMaxBlend = false;
    int blendCount;
    void Start() {
        smr = blendGO.GetComponent<SkinnedMeshRenderer>();
        blendCount = smr.sharedMesh.blendShapeCount;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            doMaxBlend = true;
        }

        if (doMaxBlend) {
            MaxOutBlendShape();
        }
    }

    public void MaxOutBlendShape() {
        if (blendCurrent >= blendMax) {
            for (int i = 0; i < blendCount; i++) {
                smr.SetBlendShapeWeight(i, blendCurrent);
            }
            blendCurrent -= blendSpeed * Time.deltaTime;
        }
    }
}
    