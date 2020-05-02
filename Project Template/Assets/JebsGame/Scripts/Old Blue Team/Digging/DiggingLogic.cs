using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DiggingLogic : MonoBehaviour
{
    [Header("Objects")]
    public GameObject holeMR_Buried;
    public GameObject holeMR_Half;
    public GameObject holeMR_Digged;

    public GameObject chest;
    public Transform chestBuried;
    public Transform chestDigged;

    [Header("Control")]
    public float validShovelfulVelocity = 1.0f;

    [Header("Events")]
    public UnityEvent onValidShovelful;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAll()
    {
        holeMR_Buried.SetActive(false);
        holeMR_Half.SetActive(false);
        holeMR_Digged.SetActive(false);
    }

    public void DiggedChest(bool b)
    {
        if (b)
        {
            chest.transform.position = chestDigged.position;
            chest.transform.rotation = chestDigged.rotation;
        }
        else
        {
            chest.transform.position = chestBuried.position;
            chest.transform.rotation = chestBuried.rotation;
        }
    }

    public void Shovelful(GameObject shovelCollider)
    {
        if (shovelCollider.GetComponent<Collider>().attachedRigidbody.velocity.magnitude >= validShovelfulVelocity)
            onValidShovelful.Invoke();
    }
}
