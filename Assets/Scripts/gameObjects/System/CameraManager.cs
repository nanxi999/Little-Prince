using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update

    CinemachineTargetGroup group;
    Cinemachine.CinemachineTargetGroup.Target target;
    private int i = 0;
    void Start()
    {
        group = GameObject.Find("Targets").GetComponent<CinemachineTargetGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
