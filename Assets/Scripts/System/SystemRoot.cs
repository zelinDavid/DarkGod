using UnityEngine;
using System.Collections;
 
public class SystemRoot : MonoBehaviour
{
    protected static ResSvc resSvc;

    private void Start()
    {
        resSvc = ResSvc.Instance;

    }
}
 