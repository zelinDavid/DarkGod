/****************************************************
    文件：ResSvc.cs
	作者：BearYang
    邮箱: 1785275942@qq.com
    日期：2019/1/7 19:54:16
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
 
 


public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance;
    private Action prgCB = null;

    private void Start()
    {
        Instance = this;

    }

    public void LoadSceneAsync(string name, Action finishAction)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        prgCB = () =>
        {
            float progress = operation.progress;
            if (progress == 1.0f)
            {
                finishAction();
            }
            else
            {
                GameRoot.Instance.UpdateloadingInfo(progress, name);
            }
        };
    }

    public void UpdateLoadingProgress(float progress)
    {
        
    }


    private void Update()
    {
        prgCB();

    }
}