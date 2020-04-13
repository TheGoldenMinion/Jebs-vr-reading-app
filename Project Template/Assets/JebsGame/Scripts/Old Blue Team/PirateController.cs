using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;

    [Header("AudioClips for Tutorial Clips")]
    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip map;
    [SerializeField] private AudioClip shovel;
    [SerializeField] private AudioClip key;
    [SerializeField] private AudioClip crab;
    [SerializeField] private AudioClip crabFight;


    [Header("Audio Clips for Alphabet dialogue")]
    [SerializeField] private AudioClip a;
    [SerializeField] private AudioClip b;
    [SerializeField] private AudioClip c;
    [SerializeField] private AudioClip d;
    [SerializeField] private AudioClip e;
    [SerializeField] private AudioClip f;
    [SerializeField] private AudioClip g;

    //Change these bools to [SerializedFields] private.
    //Currently using these for debugging.

    //Bools to check if Tutorial has been completed
    public bool introHasPlayed = false;
    public bool mapHasPlayed = false;
    public bool shovelHasPlayed = false;
    public bool keyHasPlayed = false;
    public bool enemyHasPlayed = false;
    //public bool enemyHitHasPlayed = false;

    public bool isTutorialComplete = false; 


    //Bools to check if letters have been found
    public bool aIsComplete = false;
    public bool bIsComplete = false;
    public bool cIsComplete = false;
    public bool dIsComplete = false;
    public bool eIsComplete = false;
    public bool fIsComplete = false;
    public bool gIsComplete = false;

    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float crabDelay = 1f;
    [SerializeField] private GameObject tutorialCrab;
    public string currentLetter = null; 

    

    private void TutorialCheck()
    {
        if (!introHasPlayed)
        {
            audioSource.clip = intro;
            audioSource.PlayDelayed(waitTime);
            introHasPlayed = true;
            return;
        }
        if (!mapHasPlayed && introHasPlayed && !audioSource.isPlaying)
        {
            audioSource.clip = map;
            audioSource.PlayDelayed(waitTime);
            mapHasPlayed = true;
            return;
        }
        if (!shovelHasPlayed && mapHasPlayed && !audioSource.isPlaying)
        {
            audioSource.clip = shovel;
            audioSource.PlayDelayed(waitTime);
            shovelHasPlayed = true;
            return;
        }
        if (!keyHasPlayed && shovelHasPlayed && !audioSource.isPlaying)
        {
            audioSource.clip = key;
            audioSource.PlayDelayed(waitTime);
            keyHasPlayed = true;
            return;
        }
        if (!enemyHasPlayed && keyHasPlayed && !audioSource.isPlaying)
        {
            audioSource.clip = crab;
            audioSource.PlayDelayed(waitTime);
            StartCoroutine(SpawnCrabDelay());
            enemyHasPlayed = true;
            return;
        }
    }

    private IEnumerator SpawnCrabDelay()
    {
        yield return new WaitForSeconds(crabDelay);
        tutorialCrab.SetActive(true);
    }

    private void LetterCheck()
    {
        if (!aIsComplete)
        {
            audioSource.clip = a;
            audioSource.Play();
            //aIsComplete = true;
            currentLetter = "a";
            return;
        }

        if (!bIsComplete && aIsComplete && !audioSource.isPlaying)
        {
            audioSource.clip = b;
            audioSource.Play();
            //bIsComplete = true;
            currentLetter = "b";
            return;
        }

        if (!cIsComplete && bIsComplete && !audioSource.isPlaying)
        {
            audioSource.clip = c;
            audioSource.Play();
            //cIsComplete = true;
            currentLetter = "c";
            return;
        }

        if (!dIsComplete && cIsComplete && !audioSource.isPlaying)
        {
            audioSource.clip = d;
            audioSource.Play();
            //dIsComplete = true;
            currentLetter = "d";
            return;
        }

        if (!eIsComplete && dIsComplete && !audioSource.isPlaying)
        {
            audioSource.clip = e;
            audioSource.Play();
            //eIsComplete = true;
            currentLetter = "e";
            return;
        }

        if (!fIsComplete && eIsComplete && !audioSource.isPlaying)
        {
            audioSource.clip = f;
            audioSource.Play();
            //fIsComplete = true;
            currentLetter = "f";
            return;
        }

        if (!gIsComplete && fIsComplete && !audioSource.isPlaying)
        {
            audioSource.clip = g;
            audioSource.Play();
            //gIsComplete = true;
            currentLetter = "g";
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            if (!isTutorialComplete)
            {
                TutorialCheck();
                return;
            }

            LetterCheck();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "MainCamera")
        {
            if (!isTutorialComplete)
            {
                TutorialCheck();
                return;
            }

            //LetterCheck();
        }
    }
}
