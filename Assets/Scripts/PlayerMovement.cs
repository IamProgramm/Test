using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    
    public Animator animator;
    public NavMeshAgent agent;
    public GameObject[] destinations;
    public float BeforeRotationTime = 0;
    public float RotationSpeed = 0;
    public float StopTime = 0;
    public float WaitingForTargetsShot = 0;
    int CurrentStage = 1;


    void Start()
    {
        StartCoroutine(PathFind());
        PlayerPrefs.DeleteAll();
    }



    IEnumerator PathFind()
    {
        while(true)
        {
            for (int i = 0; i < destinations.Length; i++)
            {
                int target = destinations[i].GetComponent<PlaygroundEnemyScript>().UpdateTargetsForTargets;
                if (target <= 0) 
                {
                    if (i != PlayerPrefs.GetInt("Point") || i == 0 && CurrentStage == 1)
                    {
                        CurrentStage++;
                        animator.SetBool("Run", true);
                    }
                    PlayerPrefs.SetInt("Point", i);
                    agent.SetDestination(destinations[i].transform.position);
                    yield return new WaitForSeconds(BeforeRotationTime);
                    agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, destinations[i].transform.rotation, RotationSpeed);
                    yield return new WaitForSeconds(StopTime);
                }
                else
                {
                    animator.SetBool("Run", false);
                    i = PlayerPrefs.GetInt("Point") - 1;
                    yield return new WaitForSeconds(WaitingForTargetsShot);
                }
                
            }
        }
    }

}
