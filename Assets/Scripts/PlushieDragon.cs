using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlushieDragon : MonoBehaviour
{
    public GameObject plushie;
    SkinnedMeshRenderer smr;
    private readonly float blendMax = 100f;
    public float blendCurrent = 0f;
    public float blendSpeed = 100f;
    public bool openMouthBool = false;
    int blendCount;
    void Start() {
        smr = plushie.GetComponent<SkinnedMeshRenderer>();
        blendCount = smr.sharedMesh.blendShapeCount;
        Debug.Log(blendCount);
    }

    // Update is called once per frame
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
            smr.SetBlendShapeWeight(1, blendCurrent);
            blendCurrent += blendSpeed * Time.deltaTime;
        }
    }
}
