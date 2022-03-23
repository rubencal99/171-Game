using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponRenderer : MonoBehaviour
{
    [SerializeField]
    protected int playerSortingOrder = 0;

    protected SpriteRenderer weaponRenderer;

    private void Awake()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
    }

    // Flips weapon
    public void FlipSprite(bool val)
    {
        //weaponRenderer.flipX = val;
        Debug.Log("In flip sprite");
        weaponRenderer.flipX = val;
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
