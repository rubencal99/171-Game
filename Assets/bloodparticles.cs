using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodparticles : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particles;
    void Start()
    {
        particles = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    public void SpawnBlood() {
        particles.Play();
        StartCoroutine(DespawnAfterDelay());
        this.transform.parent = null;
    }

    private IEnumerator DespawnAfterDelay() {
        yield return new WaitForSeconds(90);
        Destroy(this);
    }
}
