using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks {
    public class PPSeek : Action
    {
        [SerializeField] private bool useMovementPrediction;
        [SerializeField] private bool ShowDebug;
        [SerializeField, Range(-1, 1)] private float PredictionThreshold = 0;
        [SerializeField, Range(0.25f, 2f)] private float PredictionTime = 1f;
        [SerializeField] private float stoppingDistance = 2f;

        public SharedGameObject target;
        public float updateSpeed = 0.1f;

        private Victim victim;
        private NavMeshAgent agent;
        private Animator animator;

        public override void OnAwake() {
            Cursor.visible = true;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        public override void OnStart() {
            agent.updatePosition = false;
            agent.updateRotation = true;

            agent.isStopped = false;
            victim = target.Value.GetComponent<Victim>();
            StartCoroutine(GetTarget());
        }
        public override TaskStatus OnUpdate() {
            if(target.Value == null) {
                Debug.Log("Target Not Defined");
            }
            animator.applyRootMotion = true;
            if (Arrived()) {
                return TaskStatus.Success;
            }
            else {
                return TaskStatus.Running;
            }
        }

        private bool Arrived() {
            float distance = Vector3.Distance(agent.transform.position, victim.transform.position);
            return distance <= stoppingDistance;
        }

        private IEnumerator GetTarget() {
            WaitForSeconds Wait = new WaitForSeconds(updateSpeed);
            while (!Arrived()) {
                if (!useMovementPrediction) {
                    agent.SetDestination(victim.transform.position);
                }
                else {
                    float timeToVictim = Vector3.Distance(victim.transform.position, transform.position) / agent.speed;
                    if (timeToVictim > PredictionTime) {
                        timeToVictim = PredictionTime;
                    }

                    Vector3 targetPos = victim.transform.position + victim.Spy.averageVelocity * timeToVictim;
                    Vector3 targetDirection = (targetPos - transform.position).normalized;
                    Vector3 playerDirection = (victim.transform.position - transform.position).normalized;

                    float dot = Vector3.Dot(playerDirection, targetDirection);
                    if (dot < PredictionThreshold) {
                        targetPos = victim.transform.position;
                    }

                    agent.SetDestination(targetPos);
                }
                yield return Wait;
            }
        }

        public override void OnDrawGizmos() {
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
                    line.startWidth = 0.25f;
                    line.endWidth = 0.25f;

                    line.startColor = Color.yellow;
                    line.endColor = Color.yellow;
                }

                var path = agent.path;

                line.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++) {
                    line.SetPosition(i, path.corners[i]);
                }
            }
        }
    }
}

