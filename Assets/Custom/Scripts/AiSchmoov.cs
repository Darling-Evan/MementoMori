using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]

public class AiSchmoov : MonoBehaviour {
    [SerializeField] private bool useMovementPrediction;
    [SerializeField] private bool ShowDebug;
    [SerializeField, Range(-1,1)] private float PredictionThreshold = 0;
    [SerializeField, Range(0.25f, 2f)] private float PredictionTime = 1f;

    public Victim victim;
    public float updateSpeed = 0.1f;

    private NavMeshAgent agent;
    private Animator animator;

    private Vector2 velocity;
    private Vector2 smoothDelta;

    private void Awake() {
        Cursor.visible = true;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.applyRootMotion = true;
        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    public void Start() {
        StartCoroutine(GetTarget());
    }

    private void OnAnimatorMove() {
        Vector3 rootPos = animator.rootPosition;
        rootPos.y = agent.nextPosition.y;
        transform.position = rootPos;
        agent.nextPosition = rootPos;
    }
    private IEnumerator GetTarget() {
        WaitForSeconds Wait = new WaitForSeconds(updateSpeed);
        while (enabled) {
            if (!useMovementPrediction) {
                agent.SetDestination(victim.transform.position);
            }
            else {
                float timeToVictim = Vector3.Distance(victim.transform.position, transform.position) / agent.speed;
                if(timeToVictim > PredictionTime) {
                    timeToVictim = PredictionTime;
                }

                Vector3 targetPos = victim.transform.position + victim.Spy.averageVelocity * timeToVictim;
                Vector3 targetDirection = (targetPos - transform.position).normalized;
                Vector3 playerDirection = (victim.transform.position - transform.position).normalized;

                float dot = Vector3.Dot(playerDirection, targetDirection);
                if(dot < PredictionThreshold) {
                    targetPos = victim.transform.position;
                }

                agent.SetDestination(targetPos);
            }
            yield return Wait;
        }
    }

    private void Update() {
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

    public void OnDrawGizmos() {
        if (ShowDebug) {
            if (agent == null) {
                return;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.destination, 0.25f);

            var line = this.GetComponent<LineRenderer>();
            if (line == null) {
                line = this.gameObject.AddComponent<LineRenderer>();
                line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
                line.SetWidth(0.25f, 0.25f);
                line.SetColors(Color.yellow, Color.yellow);
            }

            var path = agent.path;

            line.SetVertexCount(path.corners.Length);

            for (int i = 0; i < path.corners.Length; i++) {
                line.SetPosition(i, path.corners[i]);
            }
        }

    }
}
