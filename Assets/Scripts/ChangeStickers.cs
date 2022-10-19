using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStickers : MonoBehaviour
{
    public Texture[] stickers;
    
    public void ChangeSticker(int i) {
        GetComponent<PaintIn3D.P3dPaintDecal>().Texture = stickers[i];
    }
}
