using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
   LineRenderer line;

    [SerializeField] LayerMask grapplableMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;

    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector3 target;

    private void Start() {
        line = GetComponent<LineRenderer>();
    }

    private void Update() {
        if (Input.GetButtonDown("Hook") && !isGrappling && PlayerAugmentations.AugmentationList["HookShot"] == true) {
            StartGrapple();
        }

        if (retracting) {
            Vector3 grapplePos = Vector3.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position);

            if (Vector3.Distance(transform.position, target) < 0.5f) {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }
        }
    }

    private void StartGrapple() {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null) {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple() {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position); 

        Vector3 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime) {
            newPos = Vector3.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }
        
        line.SetPosition(1, target);
        retracting = true;
    }
}
