 
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

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
        audioSvc.PlayUIAudio(Constant.BGLogin);
        if (nameField.text != "")
        {
            //TODO
            //发送名字到服务器
        }else{
            GameRoot.Instance.AddTips("当前名字不符合规范");
        }


    }

}