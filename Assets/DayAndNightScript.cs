using UnityEngine;

public class DayAndNightScript : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    float degpersec = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Sun"))
        {
            rot.x = degpersec * Time.deltaTime;
            transform.Rotate(rot, Space.World);
        }
        // rot.x = degpersec * Time.deltaTime;
        // transform.Rotate(rot, Space.World);

        
    }
}
