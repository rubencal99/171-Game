using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AgentRenderer : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    protected Material material;
    float fade = 1f;
    public bool isDying = false;
    public bool isEnraged = false;
    /*public bool Enrage
    {
        get {return isEnraged;}
        set {
            if (isEnraged == value) return;
            isEnraged = value;
            if (RevertColor != null){
                RevertColor(spriteRenderer.color);
            }
        }
    }*/

    public bool isBuffed = false;

    private Color originalColor;
    private Color deathColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = GetComponent<SpriteRenderer>().material;
        originalColor = spriteRenderer.color;
        // Debug.Log("Original Color: " + originalColor);
        deathColor = new Color(originalColor.r/2, originalColor.g/2, originalColor.b/2, 1);
    }

    void Update(){
        if (isDying){
            fade -= Time.deltaTime / 10;
            if (fade <= 0f){
                fade = 0f;
                // isDying = false;
            }

            material.SetFloat("_Fade", fade);
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, deathColor, 0.01f);
        }
        else
        {
            AdjustColors();
        }
    }

    void AdjustColors()
    {
        if (isEnraged)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.red, 0.1f);
        }
        if (isBuffed)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.green, 0.1f);
        }
        else{
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, 0.1f);
        }
    }
    
    public void FaceDirection(Vector2 pointerInput)
    {
        var direction = (Vector3)pointerInput - transform.position;

        // This calculation is to determine if we're looking left or right
        var result = Vector3.Cross(Vector2.up, direction);
        if(result.z > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(result.z < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
