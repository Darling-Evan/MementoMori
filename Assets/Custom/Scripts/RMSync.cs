using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

 [RequireComponent(typeof(Animator))]
 [RequireComponent(typeof(NavMeshAgent))]

public class RMSync : MonoBehaviour
{
   
    private NavMeshAgent agent;
    private Animator animator;

    private Vector2 velocity;
    private Vector2 smoothDelta;

    private void Awake() {
        Cursor.visible = true;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    private void OnAnimatorMove() {
        Vector3 rootPos = animator.rootPosition;
        rootPos.y = agent.nextPosition.y;
        transform.position = rootPos;
        agent.nextPosition = rootPos;
    }

    private void Update() {
        animator.applyRootMotion = true;
        AnimSync();
    }

    private void AnimSync() {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0f;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.1f);
        smoothDelta = Vector2.Lerp(smoothDelta, deltaPosition, smooth);

        velocity = smoothDelta / Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance) {
            velocity = Vector2.Lerp(Vector2.zero, velocity, agent.remainingDistance / agent.stoppingDistance);
        }

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        animator.SetBool("Move", shouldMove);
        animator.SetFloat("velX", velocity.x);
        animator.SetFloat("velY", velocity.y);

        float deltaMag = worldDeltaPosition.magnitude;
        if (deltaMag > agent.radius / 2f) {
            transform.position = Vector3.Lerp(animator.rootPosition, agent.nextPosition, smooth);
        }
    }
}
