using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCamera : MonoBehaviour
{
    public float speed;
    public new Camera camera;
    public Moveable moveable;
    public const float BOUNDS_FRACTION = 0.30f; 
    private bool isRecentering = false;
    
    public bool InCameraBounds {
        get {
            int pixelWidth = camera.pixelWidth;
            int pixelHeight = camera.pixelHeight;
            
            float startX = pixelWidth * BOUNDS_FRACTION;
            float startY = pixelHeight * BOUNDS_FRACTION;
            float width = pixelWidth - (pixelWidth * BOUNDS_FRACTION * 2);  
            float height = pixelHeight - (pixelHeight * BOUNDS_FRACTION * 2);
            float endX = startX + width;
            float endY = startY + height;

            var moveableScreenPosition = camera.WorldToScreenPoint(moveable.FinalPosition);
            return  moveableScreenPosition.x > startX &&
                    moveableScreenPosition.y > startY &&
                    moveableScreenPosition.x < endX   &&
                    moveableScreenPosition.y < endY;
        }
    }

    public void FixedUpdate()
    {
        if (!InCameraBounds && !isRecentering)
            isRecentering = true;
        
        if (isRecentering)
        {
            // Move the camera towards the moveable
            var desiredCameraPosition = moveable.NextPosition + Vector3.back;
            var step = speed * Time.deltaTime;
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, desiredCameraPosition, speed);
            
            if (camera.transform.position == desiredCameraPosition)
                isRecentering = false;
        }

        
    }
    
    /// <summary>
    /// Moves camera to the character without interpolating it
    /// </summary>
    public void RefreshCamera()
    {
        camera.transform.position = moveable.transform.position + Vector3.back;
    }
}
