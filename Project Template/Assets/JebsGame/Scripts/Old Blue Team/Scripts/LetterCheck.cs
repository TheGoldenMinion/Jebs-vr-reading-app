using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JorgeJGnz;

public class LetterCheck : MonoBehaviour
{
    [SerializeField] private PirateController controller;
    [SerializeField] private Transform crabSpawnPoint;
    [SerializeField] private GameObject crabPrefab;
    [SerializeField] public string letterInChest;
    public string currentLetter;
    [SerializeField] AudioSource thisAudio;
    [SerializeField] private AudioClip wrongChest;
    [SerializeField] private AudioClip correctChest;
    [SerializeField] private bool crabHasSpawned;
    [SerializeField] private bool crabMusicPlaying;
    [SerializeField] private GameObject crab;

    private void Start()
    {
        currentLetter = controller.currentLetter;
    }

    private void Update()
    {
        currentLetter = controller.currentLetter;

        if (crabHasSpawned && crab != null && !crabMusicPlaying)
        {
            thisAudio.clip = wrongChest;
            thisAudio.Play();
            crabMusicPlaying = true;
        }
    }

    public void CheckLetter()
    {
        currentLetter = controller.currentLetter;

        if (!currentLetter.Equals(letterInChest))
        {
            thisAudio.clip = wrongChest;
            thisAudio.Play();
            SpawnCrab();
            return;
        }
    }

    public void LetterHasBeenGrabbed()
    {
        switch (letterInChest)
        {
            case "a":
                controller.aIsComplete = true;
                break;
            case "b":
                controller.bIsComplete = true;
                break;
            case "c":
                controller.cIsComplete = true;
                break;
            case "d":
                controller.dIsComplete = true;
                break;
            case "e":
                controller.eIsComplete = true;
                break;
            case "f":
                controller.fIsComplete = true;
                break;
            case "g":
                controller.gIsComplete = true;
                break;
            default:
                print("errror with letter checking on chests");
                break;
        }
    }

    public void SpawnCrab()
    {
        crab = Instantiate(crabPrefab, crabSpawnPoint) as GameObject;
        crabHasSpawned = true;
    }
    
}
