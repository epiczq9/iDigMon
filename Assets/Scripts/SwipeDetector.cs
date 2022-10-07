using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    public Text outputText;

    private void Update() {
        SwipeTap();
    }
    public void SwipeTap() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPosition - startTouchPosition;

            if (!stopTouch) {
                if (Distance.x < -swipeRange && Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y)) {
                    outputText.text = "LEFT";
                    stopTouch = true;
                } else if (Distance.x > swipeRange && Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y)) {
                    outputText.text = "RIGHT";
                    stopTouch = true;
                } else if (Distance.y < -swipeRange && Mathf.Abs(Distance.x) < Mathf.Abs(Distance.y)) {
                    outputText.text = "DOWN";
                    stopTouch = true;
                } else if (Distance.y > swipeRange && Mathf.Abs(Distance.x) < Mathf.Abs(Distance.y)) {
                    outputText.text = "UP";
                    stopTouch = true;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange) {
                outputText.text = "Tap";
            }
        }
    }

    public string SwipeResult() {
        return outputText.text;
    }
}
