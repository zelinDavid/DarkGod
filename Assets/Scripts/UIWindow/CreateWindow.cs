 
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using PEProtocol;

public class CreateWindow : WindowRoot {
    public InputField nameField;

    protected override void InitWnd(){
        base.InitWnd();

        string rdName = resSvc.GetRDNameData();
        nameField.text = rdName;
    }
    
    public void ClickRandBtn() {
        string rdName = resSvc.GetRDNameData();
        Debug.Log("rdName:" + rdName);
        Debug.Log(nameField);
        nameField.text = rdName;
    }

    public void ClickEnterBtn() {
        audioSvc.PlayUIAudio(Constant.UIClickBtn);
        if (nameField.text != "")
        {
            GameMsg msg = new GameMsg{
                cmd = (int)CMD.ReqRename,
                reqRename = new ReqRename{
                    name = nameField.text,
                }
            };
            netSvc.SendMessage(msg);
        }else{
            GameRoot.Instance.AddTips("输入姓名不能为空");
        }


    }
  

    protected override void ClearWnd() {
        this.gameObject.SetActive(false);
    }

}