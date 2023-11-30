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
        GetMats(gameObject);
        foreach(Transform child in transform) {
            GetMats(child.gameObject);
        }
    }

    [ContextMenu("StartDissolve")]
    public void StartDissolve() {
        StartCoroutine(Dissolve());
    }

    [ContextMenu("StartMaterialize")]
    public void StartMaterialize() {
        StartCoroutine(Materialize());
    }


    private void GetMats(GameObject go) {
        if(go.GetComponent<MeshRenderer>() != null) {
            foreach (Material mat in go.GetComponent<MeshRenderer>().materials) {
                try {
                    mat.SetFloat("_DissolveAmount", 0);
                } catch {
                    Debug.Log(mat.name + "Is not a dissolve shader");
                }
                
                materials.Add(mat);
            }
        }

        if(go.GetComponent<SkinnedMeshRenderer>() != null) {
            foreach (Material mat in go.GetComponent<SkinnedMeshRenderer>().materials) {
                mat.SetFloat("_DissolveAmount", 0);
                materials.Add(mat);
            }
        }
    }

    private IEnumerator Dissolve() {
        if(dissolveEffect != null) {
            dissolveEffect.Play();
        }

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

    private IEnumerator Materialize() {
        if(materials.Count > 0) {
            float counter = 1;

            while (materials[0].GetFloat("_DissolveAmount") > 0) {
                counter -= dissolveRate;

                foreach (Material mat in materials) {
                    mat.SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

}
