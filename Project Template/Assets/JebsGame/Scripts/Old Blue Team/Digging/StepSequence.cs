using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Step
{
    // public string name;
    public UnityEvent onStepReached;
}

public class StepSequence : MonoBehaviour
{
    [Header("Steps")]
    [SerializeField]
    public List<Step> steps = new List<Step>();
    public UnityEvent onNullStep;
    public int initialState = 0;

    public int currentStep
    {
        get { return _currentStep; }
        set
        {
            if (steps[value] == null || steps[value].onStepReached == null) onNullStep.Invoke();
            else steps[value].onStepReached.Invoke();

            _currentStep = value;
        }
    }
    int _currentStep;

    public void SetStep(int index)
    {
        if (index >= 0 && index < steps.Count) currentStep = index;
    }

    public void NextStep()
    {
        if (currentStep < steps.Count - 1) currentStep++;
    }

    private void Start()
    {
        SetStep(initialState);
    }
}
