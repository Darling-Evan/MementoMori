using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSync : MonoBehaviour
{
    [SerializeField] private Player player;

    void Update()
    {
        transform.position = player.transform.position + Vector3.up;
    }
}
