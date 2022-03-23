using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


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

    public Color baseColor;
    public Color baseColor2;
    public Color generatedColor;
    private Color originalColor;
    private Color deathColor;
    public Color currentColor;
    public Light2D light2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = GetComponent<SpriteRenderer>().material;
        light2D = transform.parent.GetComponentInChildren<Light2D>();
        SetSkinTone();
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
        currentColor = spriteRenderer.color;
    }

    void AdjustColors()
    {
        if (isEnraged)
        {
            Enrage();
        }
        if (isBuffed)
        {
            Buff();
        }
        else{
            Normal();
        }
    }

    public void Enrage()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.red, 0.1f);
        light2D.intensity = Mathf.Lerp(light2D.intensity, 1, 0.1f);
        light2D.color = Color.Lerp(light2D.color, Color.red, 0.1f);
    }

    public void Buff()
    {
        //spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.green, 0.1f);
        light2D.intensity = Mathf.Lerp(light2D.intensity, 10, 0.1f);
        light2D.color = Color.Lerp(light2D.color, Color.green, 0.1f);
    }

    public void Normal()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, 0.1f);
        light2D.intensity = Mathf.Lerp(light2D.intensity, 0, 0.1f);
        //light.color = Color.Lerp(light.color, Color.red, 0.1f);
    }

    void SetSkinTone()
    {
        material.SetColor("BaseColor", baseColor);
        material.SetColor("BaseColor2", baseColor2);

        float r = Random.Range(150, 255);
        float g = Random.Range(150, 255);
        float b = Random.Range(150, 255);

        Color skinTone = new Color(r/255f, g/255f, b/255f);
        generatedColor = skinTone;
        material.SetColor("SkinTone", skinTone);
    }
    
    public void FaceDirection(Vector3 pointerInput)
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
