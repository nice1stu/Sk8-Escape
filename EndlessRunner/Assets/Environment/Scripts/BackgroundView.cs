using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundView : MonoBehaviour
{
    public float parallaxSpeed = 1f;  // Speed of the parallax effect

    private float offset = 0f;  // Offset of the background from the camera
    private float scale = 1f;   // Scale of the background

    // Set the offset of the background
    public void SetOffset(float offset)
    {
        this.offset = offset;
    }

    // Set the scale of the background
    public void SetScale(float scale)
    {
        this.scale = scale;
    }

    // Update the position and scale of the background
    private void LateUpdate()
    {
        // Calculate the target position and scale of the background
        float targetX = Camera.main.transform.position.x * (1 - parallaxSpeed);
        targetX += offset * parallaxSpeed;
        float targetScale = scale;

        // Apply the target position and scale to the background transform
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(targetScale, targetScale, transform.localScale.z);
    }
}