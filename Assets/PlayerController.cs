// PlayerController.cs
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action<int> OnPlayerMoved;
    
    private Transform[] platforms;
    private int currentPlatformIndex = 7; // Bottom middle
    private bool canMove = true;
    
    public void SetPlatforms(Transform[] platformsArray)
    {
        platforms = platformsArray;
    }
    
    void Update()
    {
        if (!canMove) return;
        
        int newIndex = currentPlatformIndex;
        bool moved = false;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && currentPlatformIndex > 2)
        {
            newIndex = currentPlatformIndex - 3;
            moved = true;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentPlatformIndex < 6)
        {
            newIndex = currentPlatformIndex + 3;
            moved = true;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentPlatformIndex % 3 != 0)
        {
            newIndex = currentPlatformIndex - 1;
            moved = true;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentPlatformIndex % 3 != 2)
        {
            newIndex = currentPlatformIndex + 1;
            moved = true;
        }
        
        if (moved)
        {
            currentPlatformIndex = newIndex;

            Vector3 targetPosition = platforms[currentPlatformIndex].position;
            targetPosition.y = transform.position.y; 
            transform.position = targetPosition;

            OnPlayerMoved?.Invoke(currentPlatformIndex);
            
            canMove = false;

            Invoke("EnableMovement", 2f);
        }
    }
    
    void EnableMovement()
    {
        canMove = true;
    }
}