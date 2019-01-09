using UnityEngine;
using System.Collections;
 
public class SystemRoot : MonoBehaviour
{
    protected static ResSvc resSvc;

    public virtual void Start()
    {
        resSvc = ResSvc.Instance;

    }
}
 