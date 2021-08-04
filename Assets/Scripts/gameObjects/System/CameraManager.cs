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
        int nullTargets = 0;
        for(int i = 0; i < group.m_Targets.Length; i++)
        {
            if(group.m_Targets[i].target == null)
            {
                nullTargets++;
            }
        }

        if(group.m_Targets.Length - nullTargets < 2)
        {
            Prince[] princes = FindObjectsOfType<Prince>();
            if(princes.Length > 0)
            {
                foreach(Prince prince in princes)
                {
                    if(prince.IsCryin()) { continue; }
                    target.target = prince.transform;
                    target.weight = 1;
                    target.radius = 0;

                    group.m_Targets.SetValue(target, i++);
                } 
            }

            i = 0;
        }
    }
}
