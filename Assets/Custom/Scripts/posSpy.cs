using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class posSpy : MonoBehaviour {
    [SerializeField, Range(0.1f, 5f)] private float collectionDuration = 1f;
    [SerializeField, Range(0.001f,1f)] private float collectionInterval = 0.1f;

    private CharacterController controller;
    
    private Queue<Vector3> velocityHistory;
    private float LastPosTime;
    private int MaxQueueSize;

    public Vector3 averageVelocity {
        get {
            Vector3 average = Vector3.zero;
            foreach(Vector3 v in velocityHistory) {
                average += v;
            }
            average.y = 0;
            return average / velocityHistory.Count;
        }
    }

    private void Awake() {
        controller = GetComponent<CharacterController>();
        MaxQueueSize = Mathf.CeilToInt(1f / collectionInterval * collectionDuration);
        velocityHistory = new Queue<Vector3>(MaxQueueSize);
    }

    private void Update() {
        if(LastPosTime + collectionInterval <= Time.time) {
            if(velocityHistory.Count == MaxQueueSize) {
                velocityHistory.Dequeue();
            }
            velocityHistory.Enqueue(controller.velocity);
            LastPosTime = Time.time;
        }
    }
}
