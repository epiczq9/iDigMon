using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowMouse : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public float zDistance = 0.33f;
    public float yOffset = 0.1f;

    Vector3 brushRotateOld = new Vector3(0, 90, 100);
    Vector3 brushRotateNew = new Vector3(0, 90, 140);

    Sequence brushSequence;
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zDistance));
        transform.position = new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z);

        if (Input.GetButtonDown("Fire1")) {
            RotateForward();
        } else if (Input.GetButtonUp("Fire1")) {
            transform.DORotate(brushRotateOld, 0.5f);
        }
    }

    void RotateForward() {
        transform.transform.DORotate(brushRotateNew, 0.1f).OnComplete(RotateBackwards);
    }

    void RotateBackwards() {
        transform.transform.DORotate(brushRotateOld, 0.1f).OnComplete(CheckButton);
    }

    void CheckButton() {
        if (Input.GetButton("Fire1")) {
            RotateForward();
        } else if (Input.GetButtonUp("Fire1")) {
            transform.DORotate(brushRotateOld, 0.5f);
        }
    }
}
