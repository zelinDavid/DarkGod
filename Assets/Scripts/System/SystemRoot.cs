using UnityEngine;
using System.Collections;
 
public class SystemRoot : MonoBehaviour
{
    protected static ResSvc resSvc;
    protected static AudioSvc audioSvc;

    public virtual void InitSystem()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}
 