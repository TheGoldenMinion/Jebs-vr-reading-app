using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCrab : MonoBehaviour
{
    [SerializeField] private PirateController controller;
    [SerializeField] private CrabCombat crab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (crab.isDead)
        {
            controller.isTutorialComplete = true;
        }
    }
}
