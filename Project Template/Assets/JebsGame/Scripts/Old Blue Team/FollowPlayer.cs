using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    [Header("This script is just for testing to follow the player. Enable or disable by clicking the bool.")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private bool follow = false;
    [SerializeField] private bool createCrab = false;
    [SerializeField] private GameObject crabPrefab;
    [SerializeField] private Transform crabSpawnPoint;
    [SerializeField] private float speed = 5; 
    private Vector3 scaleChange;

    private void Start()
    {
        //Super inefficient!! Just doing this for testing.
        player = GameObject.Find("Main Camera").transform;

    }

    void FixedUpdate()
    {
        //enable agent to follow player.
        //To change follow settings go to navmesh agent
        if (follow)
        {
            agent.destination = player.position;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                RotateTowards(player); 
            }
        }

        //instantiate new crab at spawnpoint
        if (createCrab)
        {
            CreateCrab();
            createCrab = false;
        }
    }

    //When agent is stopped it is used to rotate the crab to always face the player.
    private void RotateTowards(Transform target)
    {
        Vector3 targetPostition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        transform.LookAt(targetPostition);
    }

    //creates new crab
    public void CreateCrab()
    {
        scaleChange = new Vector3(.3f, .3f, .3f);

        GameObject newCrab = Instantiate(crabPrefab, crabSpawnPoint.position, Quaternion.identity);
        newCrab.transform.localScale = scaleChange;
    }
}
