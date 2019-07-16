using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrWnd : WindowRoot {
    //TODO: 您上次写到这里,可以开始UI搭建及 战斗逻辑梳理.
    //左上
    public Image energy;
    public Image blood;
    public Text level;

    //左下
    public Transform ctrlBg;
    public Transform ctrl;

    //右上
    public Image boss;
    public Image bloodRed;
    public Image bloodYellow;
    public Text bossName;

    //右下
    public Image cdBgSkillOne;
    public Text cdSkillOne;
    public Image cdBgSkillTwo;
    public Text cdSkilTwo;
    public Image cdBgSkillThree;
    public Text cdSkillThree;

    //底部 经验
    public Transform explist;
    private Transform[] exps;

    protected override void InitWnd(){
        base.InitWnd();


    }

    private void InitClickEvent(){

    }


    



}