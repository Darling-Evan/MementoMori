using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MaterializeController : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    [SerializeField] private float materializationRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.0250f;

    [SerializeField] private VisualEffect particleEffect;

    [ContextMenu("ManualStart")]
    private void Start() {
        Initialize();
    }


    public void Initialize() {
        GetMats(gameObject);
        foreach (Transform child in transform) {
            GetMats(child.gameObject);
        }
    }



    [ContextMenu("ResetState")]
    public void ResetState() {
        foreach (Material mat in materials) {
            mat.SetFloat("_DissolveAmount", 1);
        }
    }

    [ContextMenu("StartMaterialize")]
    public void StartMaterialize() {
        StartCoroutine(Materialize());
    }


    private void GetMats(GameObject go) {
        if (go.GetComponent<MeshRenderer>() != null) {
            foreach (Material mat in go.GetComponent<MeshRenderer>().materials) {
                materials.Add(mat);
            }
        }

        if (go.GetComponent<SkinnedMeshRenderer>() != null) {
            foreach (Material mat in go.GetComponent<SkinnedMeshRenderer>().materials) {
                materials.Add(mat);
            }
        }
    }


    private IEnumerator Materialize() {
        if (particleEffect != null) {
            particleEffect.Play();
        }
        if (materials.Count > 0) {
            float counter = 1;

            while (materials[0].GetFloat("_DissolveAmount") > 0) {
                counter -= materializationRate;

                foreach (Material mat in materials) {
                    mat.SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
