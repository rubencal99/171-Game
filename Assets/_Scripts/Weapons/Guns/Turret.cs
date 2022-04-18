using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    LineRenderer lm;
    AgentWeapon weaponParent;
    public LayerMask layerMask;
    RaycastHit hit;

    void Awake()
    {
        lm = GetComponent<LineRenderer>();
        weaponParent = transform.parent.parent.GetComponent<AgentWeapon>();
    }

    void Update()
    {
        DrawLine();
    }

    void DrawLine()
    {
        Vector3 direction = weaponParent.aimDirection;
        hit = new RaycastHit();
        if(Physics.Raycast(transform.position, direction, out hit, 100, layerMask))
        {
            Draw3DRay(transform.position, hit.transform.position);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
                //Debug.Log("Lazer Hitting Obstacles");
            }
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                Debug.Log("Lazer Hitting Enemy");
                hit.transform.GetComponent<Enemy>().GetHit(Time.deltaTime, gameObject);
            }
        }
    }

    void Draw3DRay(Vector3 start, Vector3 end)
    {
        lm.SetPosition(0, start);
        lm.SetPosition(1, end);
    }
}
