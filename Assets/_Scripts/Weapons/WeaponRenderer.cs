using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponRenderer : MonoBehaviour
{
    [SerializeField]
    protected int playerSortingOrder = 0;

    protected SpriteRenderer weaponRenderer;
    GameObject camera;

    private void Awake()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
        //camera = CameraShake.Instance.gameObject;
    }

    /*void Update()
    {
        weaponRenderer.transform.LookAt(camera.transform);
    }*/

    // Flips weapon
    public void FlipSprite(bool val)
    {
        //weaponRenderer.flipX = val;
        //Debug.Log("In flip sprite");
        if(val != weaponRenderer.flipY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y, transform.localPosition.z);
        }
        weaponRenderer.flipY = val;
        
    }

    // Changes sortingOrder of weapon sprite depending on val
    public void RenderBehindHead(bool val)
    {
        if (val)
        {
            weaponRenderer.sortingOrder = playerSortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = playerSortingOrder + 1;
        }
    }
}
