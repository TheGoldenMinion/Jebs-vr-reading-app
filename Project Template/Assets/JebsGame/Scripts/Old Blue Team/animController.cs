using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animController : MonoBehaviour
{

    public GameObject chestTop;
    public bool isOpen = false;
    private Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip chestOpen;
    [SerializeField] AudioClip chestClose;


    private void Awake()
    {
        animator = chestTop.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (!isOpen)
            {
                audioSource.clip = chestOpen;
                animator.Play("Open");
                audioSource.Play();
                isOpen = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {
            if (isOpen)
            {
                audioSource.clip = chestClose;
                animator.Play("Close");
                audioSource.Play();
                isOpen = false;
            }
        }
    }
}
