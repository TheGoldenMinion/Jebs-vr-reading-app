using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMaterial : MonoBehaviour
{
    //THIS IS A HELPER SCRIPT TO QUICKLY APPLY MATERIALS TO ASSTS

    [SerializeField] private Material materialToApply;
    [SerializeField] private Renderer[] renderers;

    void Start()
    {
        //Gets all renderers from child objects 
        renderers = GetComponentsInChildren<Renderer>();

        //Iterates through all of them and applies material
        for(int i=0; i<renderers.Length; i++)
        {
            renderers[i].sharedMaterial = materialToApply;
        }
    }
}
