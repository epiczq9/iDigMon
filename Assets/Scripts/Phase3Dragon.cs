using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase3Dragon : MonoBehaviour
{
    [Header("Objects")]
    public GameObject slimeParent;
    public GameObject peelingStick;
    public GameObject peelPiecePrefab;
    public GameObject bag;
    [Header("UI Elements")]
    public GameObject uiUp;
    public GameObject uiLeft;
    public GameObject uiDown;
    public GameObject uiTap;
    [Header("Transforms")]
    public Transform slimeSetDownPos;
    [Header("Others")]
    public GameObject[] slimes;
    int currentPeel = 0;
    public Transform[] peelingStartTransforms;
    public Transform[] peelingEndTransforms;
    public Transform peelingFinishTransform;
    public CinemachineVirtualCamera vCamStart, vCamPeelingSlime, vCamPullingBag;

    public bool slimeCanBePeeled = false;

    public SwipeDetector swipeDet;

    public GameObject phaseToActivate;
    void Start() {
        StartPeelingProcess();
    }
    void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "LEFT" || (Input.GetAxis("Horizontal") < 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "RIGHT" || (Input.GetAxis("Horizontal") > 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "DOWN" || (Input.GetAxis("Vertical") < 0)) {
            if (slimeCanBePeeled) {
                Peeling();
            }
            swipeDet.outputText.text = "EMPTY";
        }

        if (Input.GetButton("Fire2")) {
            Instantiate(peelPiecePrefab, peelingStick.transform);
        }
    }
    public void SetDownSlime() {
        slimeParent.transform.DOMove(slimeSetDownPos.position, 0.5f);
        slimeParent.transform.DORotateQuaternion(slimeSetDownPos.rotation, 0.5f);
    }

    public void SwitchCamsToPeelingSlime() {
        vCamStart.Priority = 10;
        vCamPeelingSlime.Priority = 20;
    }
    public void StartPeelingProcess() {
        SwitchCamsToPeelingSlime();
        Sequence stickStart = DOTween.Sequence();
        //stickStart.Append(peelingStick.transform.DOMoveY(peelingStartTransforms[0].position.y, 0.2f));
        stickStart.Append(peelingStick.transform.DOMove(peelingStartTransforms[0].position, 0.8f).OnComplete(SlimeCanBePeeled));
        //stickStart.Join(peelingStick.transform.DORotate(peelingStartTransforms[0].eulerAngles, 0.8f));
    }
    public void Peeling() {
        slimeCanBePeeled = false;
        Instantiate(peelPiecePrefab, peelingStick.transform);
        Debug.Log(currentPeel);
        slimes[currentPeel + 1].SetActive(true);
        slimes[currentPeel].SetActive(false);
        if(currentPeel == 4) {
            //bag.SetActive(true);
            Debug.Log("FINISHED PEELING");
        }
        peelingStick.transform.DOMove(peelingEndTransforms[currentPeel].position, 0.5f).OnComplete(RepositionPeelingStick);
        //peelingStick.transform.DORotate(peelingEndTransforms[currentPeel].eulerAngles, 0.5f);
    }
    public void RepositionPeelingStick() {
        currentPeel++;
        if (currentPeel <= 4) {
            peelingStick.transform.DOMove(peelingStartTransforms[currentPeel].position, 0.4f).OnComplete(SlimeCanBePeeled);
            //peelingStick.transform.DORotate(peelingStartTransforms[currentPeel].eulerAngles, 0.4f);
        } else {
            peelingStick.transform.DOMove(peelingFinishTransform.position, 0.5f).OnComplete(ActivatePhase4);
            //peelingStick.transform.DORotate(peelingFinishTransform.eulerAngles, 0.5f).OnComplete(ActivatePhase4);
        }
    }
    public void SlimeCanBePeeled() {
        slimeCanBePeeled = true;
        uiDown.SetActive(true);
    }
    public void SwitchCamsToRemovingBag() {
        vCamPeelingSlime.Priority = 10;
        vCamPullingBag.Priority = 20;
    }
    public void ActivatePhase4() {
        //SwitchCamsToRemovingBag();
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
