using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class DissolveController : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    [SerializeField] private float dissolveRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.0250f;

    [SerializeField] private VisualEffect dissolveEffect;


    private void Start() {
        foreach(Transform child in transform) {
            Debug.Log(child.name);
            if(child.GetComponent<SkinnedMeshRenderer>()) {
                foreach (Material mat in child.GetComponent<SkinnedMeshRenderer>().materials) {
                    materials.Add(mat);
                }
            }
        }
    }


    public void StartDissolve() {
        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve() {
        dissolveEffect.Play();
        if(materials.Count > 0) {
            float counter = 0;

            while (materials[0].GetFloat("_DissolveAmount") < 1) {
                counter += dissolveRate;

                foreach (Material mat in materials) {
                    mat.SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
            if (materials[0].GetFloat("_DissolveAmount") >= 1) {
                Destroy(gameObject);
            }
        }
    }
}
