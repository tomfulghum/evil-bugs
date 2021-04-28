using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] GameObject fireParticlePrefab;
    [SerializeField] float radius = 1f;
    [SerializeField] float tilt = 110f;
    [SerializeField] int amount = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++) {
            float angle = i * Mathf.PI * 2f / amount;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, 1f, Mathf.Sin(angle) * radius);
            Quaternion rot = Quaternion.Euler(tilt, (Mathf.Rad2Deg * -angle) + 90f, 0f);
            var go = Instantiate(fireParticlePrefab, transform.TransformPoint(pos), rot, transform);
            var meshRenderer = go.GetComponent<MeshRenderer>();
            meshRenderer.material.SetFloat("RandomSeed", Random.Range(0f, 1000f));
            go.transform.localScale = new Vector3(0.2f, 0.2f, Random.Range(0.15f, 0.25f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
