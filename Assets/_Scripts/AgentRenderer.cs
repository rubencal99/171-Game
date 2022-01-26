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
    public bool enraged = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update(){
        if (isDying){
            fade -= Time.deltaTime;
            if (fade <= 0f){
                fade = 0f;
                isDying = false;
            }

            material.SetFloat("_Fade", fade);
        }
        if (enraged)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.red, 0.1f);
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
