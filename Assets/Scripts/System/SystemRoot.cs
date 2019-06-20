using UnityEngine;
using System.Collections;
 
public class SystemRoot : MonoBehaviour
{
    protected static ResSvc resSvc;
    protected static AudioSvc audioSvc;
    public virtual void Start()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}
 