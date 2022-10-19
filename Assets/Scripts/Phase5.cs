using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Phase5 : MonoBehaviour
{
    public GameObject doll, paintBrush;
    public PaintIn3D.P3dChangeCounter changeCounter;
    public GameObject[] nextMaskButtons, masks;
    public bool armButtonActivated = false, earsButtonActivated = false,
        legsButtonActivated = false, faceButtonActivated = false, finishedButtonActivated = false;
    public CinemachineVirtualCamera[] vCamsPainting;

    public SwipeDetector swipeDet;

    public GameObject phaseToActivate;
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        ButtonActivation();
    }

    public int GetPercentPainted() {
        return (int)(changeCounter.Ratio * 100);
    }
    public void DebugPercent() {
        Debug.Log(GetPercentPainted());
    }
    public void ButtonActivation() {
        if (35 <= GetPercentPainted() && !armButtonActivated) {
            nextMaskButtons[0].SetActive(true);
            armButtonActivated = true;
        } else if (40 <= GetPercentPainted() && !earsButtonActivated) {
            nextMaskButtons[1].SetActive(true);
            earsButtonActivated = true;
        } else if (47 <= GetPercentPainted() && !legsButtonActivated) {
            nextMaskButtons[2].SetActive(true);
            legsButtonActivated = true;
        } else if (55 <= GetPercentPainted() && !faceButtonActivated) {
            nextMaskButtons[3].SetActive(true);
            faceButtonActivated = true;
        } else if (59 <= GetPercentPainted() && !finishedButtonActivated) {
            nextMaskButtons[4].SetActive(true);
            finishedButtonActivated = true;
        }
    }
    public void ActivateSingleMask(int maskToActivateIndex) {
        for (int i = 0; i < masks.Length; i++) {
            masks[i].SetActive(false);
        }
        masks[maskToActivateIndex].SetActive(true);
    }
    public void SwitchCamsForPainting(CinemachineVirtualCamera vCam1, CinemachineVirtualCamera vCam2) {
        vCam1.Priority = 10;
        vCam2.Priority = 20;
    }
    public void ActivateBodyPainting() {
        ActivateSingleMask(0);
    }
    public void ActivateArmsPainting() {
        ActivateSingleMask(1);
        //SwitchCamsForPainting(vCamsPainting[0], vCamsPainting[1]);
    }
    public void ActivateEarsPainting() {
        ActivateSingleMask(2);
        //SwitchCamsForPainting(vCamsPainting[1], vCamsPainting[2]);
    }
    public void ActivateLegsPainting() {
        ActivateSingleMask(3);
        //SwitchCamsForPainting(vCamsPainting[2], vCamsPainting[3]);
    }
    public void ActivateFacePainting() {
        ActivateSingleMask(4);
        //SwitchCamsForPainting(vCamsPainting[3], vCamsPainting[4]);
    }
    public void FinishedPainting() {
        ActivatePhase6();
    }
    public void ActivatePhase6() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
