using JorgeJGnz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public enum MapState
{
    Invisible,
    Visible,
    Animated,
    Held
}

public class MapLogic : MonoBehaviour
{
    [Header("Receivers")]
    public GameObject mapGroup;
    public GameObject mapObject;
    public CentroidConstraint mapCentroidConstraint;
    public RangeAction mapRangeAction;
    Transform initialMapParent;

    [Header("Controllers")]
    public Transform rightController;
    public Transform leftController;

    [Header("Boolean actions")]
    public BooleanAction bothInTrigger;
    public BooleanAction grippingL;
    public BooleanAction grippingR;
    public BooleanAction mapUnrolled;

    [Header("Events")]
    public UnityEngine.Events.UnityEvent onInvisible;
    public UnityEngine.Events.UnityEvent onVisible;
    public UnityEngine.Events.UnityEvent onAnimated;
    public UnityEngine.Events.UnityEvent onHeld;

    [Header("Debugging")]
    public string stringCurrentState;
    public bool _bothInTrigger;
    public bool _grippingL;
    public bool _grippingR;
    public bool _mapUnrolled;
    
    MapState currentState {
        get { return _currentState; }
        set {
            _currentState = value;
            switch (value)
            {
                case MapState.Invisible:
                    onInvisible.Invoke();
                    break;
                case MapState.Visible:
                    onVisible.Invoke();
                    break;
                case MapState.Animated:
                    onAnimated.Invoke();
                    break;
                case MapState.Held:
                    onHeld.Invoke();
                    break;
            }
        }
    }
    MapState _currentState;

    private void Start()
    {
        initialMapParent = mapGroup.transform.parent;

        mapObject.SetActive(false);
        mapRangeAction.enabled = false;

        mapCentroidConstraint.enabled = true;

        RecalculateState();
    }

    private void Update()
    {
        _bothInTrigger = bothInTrigger.Value;
        _grippingL = grippingL.Value;
        _grippingR = grippingR.Value;
        _mapUnrolled = mapUnrolled.Value;
    }

    public void RecalculateState()
    {
        // Visible?
        if (currentState == MapState.Invisible && bothInTrigger.Value)
        {
            mapObject.SetActive(true);

            currentState = MapState.Visible;
        }
        else if (currentState == MapState.Visible && !bothInTrigger.Value)
        {
            mapObject.SetActive(false);

            currentState = MapState.Invisible;
        }
        else if (currentState == MapState.Invisible)
        {
            mapObject.SetActive(false);
        }

        // Animated?
        if (currentState == MapState.Visible && grippingL.Value && grippingR.Value)
        {
            mapRangeAction.enabled = true;

            currentState = MapState.Animated;
        }
        else if ((currentState == MapState.Animated && !mapUnrolled.Value) && (!grippingL.Value || !grippingR.Value))
        {
            mapRangeAction.DoTransform(mapRangeAction.minTheresold);
            mapRangeAction.enabled = false;

            currentState = MapState.Invisible;
            RecalculateState();
        }

        // Attached?
        if ((currentState == MapState.Animated && mapUnrolled.Value) && (!grippingL.Value || !grippingR.Value))
        {
            if (!grippingL.Value)
            {
                // We are helding with R hand
                mapRangeAction.DoTransform(mapRangeAction.maxTheresold);
                mapRangeAction.enabled = false;
                mapCentroidConstraint.enabled = false;
                mapGroup.transform.SetParent(rightController);

                currentState = MapState.Held;
            }
            else
            {
                // We are helding with L hand
                mapRangeAction.DoTransform(mapRangeAction.maxTheresold);
                mapRangeAction.enabled = false;
                mapCentroidConstraint.enabled = false;
                mapGroup.transform.SetParent(leftController);

                currentState = MapState.Held;
            }
        }
        else if (currentState == MapState.Held && (!grippingL.Value && !grippingR.Value))
        {
            // Invisible
            mapCentroidConstraint.enabled = true;
            mapGroup.transform.SetParent(initialMapParent);

            currentState = MapState.Invisible;
            RecalculateState();
        }
        else if (currentState == MapState.Held && (grippingL.Value && grippingR.Value))
        {
            // Centroid again (state animated)
            mapCentroidConstraint.enabled = true;
            mapRangeAction.enabled = true;
            mapGroup.transform.SetParent(initialMapParent);

            currentState = MapState.Animated;
            RecalculateState();
        }

        //Debugging
        stringCurrentState = currentState.ToString();
    }
}
