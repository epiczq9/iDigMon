using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase3 : MonoBehaviour
{
    public GameObject slimeContainer, slimeParent, peelingStick, peelPiecePrefab, pickingToolButton, bag;
    public Transform slimeContainerLiftPos, slimeContainerSetAsidePos, slimeSetDownPos;
    public GameObject[] slimes;
    int currentPeel = 0;
    public Transform[] peelingStartTransforms;
    public Transform[] peelingEndTransforms;
    public Transform peelingFinishTransform;
    public CinemachineVirtualCamera vCamStart, vCamPickingTool, vCamPeelingSlime, vCamPullingBag;

    public bool containerRaised = false;
    public bool containerCanBeRaised = false;
    public bool slimeCanBePeeled = false;

    public SwipeDetector swipeDet;

    public GameObject phaseToActivate;
    void Start() {
        containerCanBeRaised = true;
    }
    void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            if (!containerRaised && containerCanBeRaised) {
                SetAsideContainer();
            }
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
    }
    public void SetAsideContainer() {
        slimeParent.transform.parent = null;
        Sequence containerSequence = DOTween.Sequence();
        containerSequence.Append(slimeContainer.transform.DOMove(slimeContainerLiftPos.position, 1f));
        containerSequence.Append(slimeContainer.transform.DOMove(slimeContainerSetAsidePos.position, 0.5f)).OnComplete(SetDownSlime);
        containerRaised = true;
    }
    public void SetDownSlime() {
        Destroy(slimeContainer);
        SwitchCamsToPickingTool();
        slimeParent.transform.DOMove(slimeSetDownPos.position, 0.5f);
        slimeParent.transform.DORotateQuaternion(slimeSetDownPos.rotation, 0.5f).OnComplete(ActivatePickingTool);
    }
    public void ActivatePickingTool() {
        pickingToolButton.SetActive(true);
    }
    public void SwitchCamsToPickingTool() {
        vCamStart.Priority = 10;
        vCamPickingTool.Priority = 20;
    }
    public void SwitchCamsToPeelingSlime() {
        vCamPickingTool.Priority = 10;
        vCamPeelingSlime.Priority = 20;
    }
    public void StartPeelingProcess() {
        SwitchCamsToPeelingSlime();
        Sequence stickStart = DOTween.Sequence();
        stickStart.Append(peelingStick.transform.DOMoveY(peelingStartTransforms[0].position.y, 0.2f));
        stickStart.Append(peelingStick.transform.DOMove(peelingStartTransforms[0].position, 0.8f).OnComplete(SlimeCanBePeeled));
        stickStart.Join(peelingStick.transform.DORotate(peelingStartTransforms[0].eulerAngles, 0.8f));
    }
    public void Peeling() {
        slimeCanBePeeled = false;
        Instantiate(peelPiecePrefab, peelingStick.transform);
        Debug.Log(currentPeel);
        slimes[currentPeel + 1].SetActive(true);
        slimes[currentPeel].SetActive(false);
        if(currentPeel == 4) {
            bag.SetActive(true);
        }
        peelingStick.transform.DOMove(peelingEndTransforms[currentPeel].position, 0.5f).OnComplete(RepositionPeelingStick);
        peelingStick.transform.DORotate(peelingEndTransforms[currentPeel].eulerAngles, 0.5f);
    }
    public void RepositionPeelingStick() {
        currentPeel++;
        if (currentPeel <= 4) {
            peelingStick.transform.DOMove(peelingStartTransforms[currentPeel].position, 0.4f).OnComplete(SlimeCanBePeeled);
            peelingStick.transform.DORotate(peelingStartTransforms[currentPeel].eulerAngles, 0.4f);
        } else {
            peelingStick.transform.DOMove(peelingFinishTransform.position, 0.5f);
            peelingStick.transform.DORotate(peelingFinishTransform.eulerAngles, 0.5f).OnComplete(ActivatePhase4);
        }
    }
    public void SlimeCanBePeeled() {
        slimeCanBePeeled = true;
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
