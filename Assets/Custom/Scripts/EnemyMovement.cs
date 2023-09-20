using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {
    
    private NavMeshAgent agent;

    public Transform target;
    public float updateSpeed = 0.1f;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        StartCoroutine(FollowTarget());
    }
    
    private IEnumerator FollowTarget() {
        WaitForSeconds Wait = new WaitForSeconds(updateSpeed);
        while (enabled) {
            agent.SetDestination(target.position);
            yield return Wait;
        }   
    }

}
