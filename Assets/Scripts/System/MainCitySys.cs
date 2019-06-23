using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCitySys : SystemRoot {
    public static MainCitySys Instance;
    public override void InitSystem () {
        base.InitSystem ();
        Debug.Log ("init MainCitySys");
        Instance = this;
        
    }

    public void EnterMainCity () {
        //TODO:编码 resSvc.Config()
        resSvc.AsyncLoadScene("SceneMainCity",()=>{


        });


    }

}