using UnityEngine;

public class DoorTestLoop : MonoBehaviour
{
    public SwingingDoorController doorController;  // Reference to your DoorController

    private bool isOpening = true;

    void Start()
    {
        // Subscribe to door events
        doorController.OnDoorFullyOpen += OnDoorFullyOpen;
        doorController.OnDoorFullyClosed += OnDoorFullyClosed;

        // Start by opening the door
        Debug.Log("Starting the open/close loop...");
        doorController.LerpToNormalizedValue(1.0f, 2.0f);  // Open the door over 2 seconds
    }

    private void OnDoorFullyOpen()
    {
        Debug.Log("Door fully open! Starting to close...");
        doorController.LerpToNormalizedValue(0.0f, 2.0f);  // Close the door over 2 seconds
    }

    private void OnDoorFullyClosed()
    {
        Debug.Log("Door fully closed! Starting to open...");
        doorController.LerpToNormalizedValue(1.0f, 2.0f);  // Open the door over 2 seconds
    }
}
