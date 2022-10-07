using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BoxOpening : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            
        }
    }

    public void OpenBox() {
        transform.DORotate(new Vector3(-90, -30, 0), 1.5f, RotateMode.Fast);
    }
}
