using System;
using System.Net.Mime;

using PEProtocol;

using UnityEngine;
using UnityEngine.UI;

public class GuidWnd : WindowRoot {
    public Image icon;
    public Text title;
    public Text content;
    private AutoGuideCfg curtTaskData;
    private string[] dialogArr;
    private int currentIndex;
    private PlayerData pd;

    protected override void InitWnd() {
        base.InitWnd();
        curtTaskData = MainCitySys.Instance.curtTaskData;
        pd = GameRoot.Instance.playerData;
        dialogArr = curtTaskData.dilogArr.Split('#');
        currentIndex = 0;
        ShowNextContent();
    }

    private void ShowNextContent() {
        currentIndex++; //从0开始
        foreach (var item in dialogArr) {
            Debug.Log(item);
        }
        if (currentIndex == dialogArr.Length) {
            FinishTalk();
            Debug.Log(currentIndex);
            Debug.Log(dialogArr);
            return;
        }

        string[] unitInfo = dialogArr[currentIndex].Split('|');
        if (unitInfo.Length < 2) {
            // Debug.Log("eeeeeeeeeeerror");
            return;
        }
        if (int.TryParse(unitInfo[0], out int a)) {
            if (a == 0) {
                SetSprite(icon, PathDefine.SelfIcon);
                SetText(title, pd.name);
            } else if (a == 1) {
                switch (curtTaskData.npcID) {
                    case 0:
                        SetSprite(icon, PathDefine.WiseManIcon);
                        SetText(title, "智者");
                        break;
                    case 1:
                        SetSprite(icon, PathDefine.GeneralIcon);
                        SetText(title, "将军");
                        break;
                    case 2:
                        SetSprite(icon, PathDefine.ArtisanIcon);
                        SetText(title, "工匠");
                        break;
                    case 3:
                        SetSprite(icon, PathDefine.TraderIcon);
                        SetText(title, "商人");
                        break;
                    default:
                        SetSprite(icon, PathDefine.GuideIcon);
                        SetText(title, "小芸");
                        break;
                }
            }
        } else {
            // Debug.Log("2222");
            return;
        }

        string contentStr = unitInfo[1].Replace("$name", pd.name);
        SetText(content, contentStr);
        icon.SetNativeSize();
    }

    private void FinishTalk() {
        SetActive(gameObject, false);
        MainCitySys.Instance.UpdateTask();
    }

    public void clickNext() {
        ShowNextContent();
    }
}