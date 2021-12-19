using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    [SerializeField] private string[] names;
    [SerializeField] private List<string> usedNames;

    public string getUniqueName()
    {
        for (int i = 0; i < names.Length; i++)
        {
            if(!usedNames.Contains(names[i]))
            {
                usedNames.Add(names[i]);
                return names[i];
            }
        }

        return "prince";
    }
}
