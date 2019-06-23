using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot {
    public Text txtTips;
    public Image imgFG;
    public Image imgPoint;
    public Text txtPrg;

    private float fgWidth;

    protected override void InitWnd () {
        base.InitWnd ();

        fgWidth = imgFG.GetComponent<RectTransform> ().sizeDelta.x;

        SetText (txtTips, "这是一条游戏tips");
        SetText (txtPrg, "0%");
        imgFG.fillAmount = 0;
        // imgPoint.transform.localPosition = new Vector2 (0, 0);
        imgPoint.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void SetProgress (float prg) {
        SetText (txtPrg, (int) (prg * 100) + "%");
        imgFG.fillAmount = prg;

        float posX = (prg) * fgWidth;
        imgPoint.rectTransform.anchoredPosition = new Vector2(posX, 0);

        // imgPoint.transform.localPosition = new Vector2 (posX, 0);

    }
}