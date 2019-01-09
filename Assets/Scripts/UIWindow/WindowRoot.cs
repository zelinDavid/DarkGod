using UnityEngine;
using System.Collections;

public class WindowRoot : MonoBehaviour
{
    NetSvc netSvc;   
    public virtual void SetWindowActive(bool active = true)
    {
        if (active)
        {
            gameObject.SetActive(true);
            Init();
        }
        else
        {

        }
    }


    
     protected virtual void Init()
    {
        netSvc = NetSvc.Instance;
    }

}
