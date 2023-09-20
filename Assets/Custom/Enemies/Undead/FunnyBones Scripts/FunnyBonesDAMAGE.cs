using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyBonesDAMAGE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Player")
        {
            var healthComponent = collision.GetComponent<PlayerHealth>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(10);
            }
        }
    }
}
