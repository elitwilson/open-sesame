using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class SwingingDoorController : MonoBehaviour
{
    #region Properties
    public enum PivotAxis { X, Y, Z }

    [SerializeField]
    private PivotAxis hingeAxis = PivotAxis.Y;

    [HideInInspector] 
    [Tooltip("The door's hinge point. If not set, the door will rotate around its own pivot.")]
    [Header("Custom Behavior")] 
    public bool UseCustomPivot = false;    

    [SerializeField]  // Allow the editor to serialize this private field
    [HideInInspector] 
    private Transform _pivotPoint;
    
    [Range(0f, 1f)]
    [Tooltip("The current normalized open angle of the door. 0 is fully closed, 1 is fully open.")]
    public float CurrentOpenAmount = 0f;
    
    public bool IsOpen = false;
    public bool IsLocked = false;
    public bool OnlyOpenOnce = false;

    [Tooltip("The angle (in degrees) representing the fully closed state of the door.")]
    public float ClosedStateAngle = 0f;

    [Tooltip("The angle (in degrees) of the fully open state of the door. Positive values open outward, negative values open inward.")]
    public float OpenStateAngle = 90f;

    private Coroutine lerpCoroutine;
    #endregion

    #region Events
    public event Action OnLerpStart;
    public event Action OnDoorFullyOpen;
    public event Action OnDoorFullyClosed;
    #endregion

    #region Lifecycle
    void Awake()
    {
        if (UseCustomPivot && _pivotPoint == null) Debug.LogError("'UseCustomPivot' is true but the Pivot point transform is not set for the swinging door gameObject: " + gameObject.name);
    }

    void OnValidate()
    {
        // Evaluate();
        SetDoorAngle();
    }

    void Update() 
    {
        // if (Application.isPlaying) return;
        // Evaluate();
        SetDoorAngle();
    }
    #endregion

    #region Public Methods
    public void LerpToNormalizedValue(float targetNormalizedValue, float duration)
    {
        // Check for the lock state
        if (IsLocked) return;
        if (!Application.isPlaying) return;

        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);  // Stop any existing lerp
        }
        lerpCoroutine = StartCoroutine(LerpDoor(targetNormalizedValue, duration));
    }
    #endregion

    #region Private Methods
    private IEnumerator LerpDoor(float targetNormalizedValue, float duration)
    {    
        OnLerpStart?.Invoke();

        float startNormalizedValue = CurrentOpenAmount;  // Current state
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            CurrentOpenAmount = Mathf.Lerp(startNormalizedValue, targetNormalizedValue, elapsedTime / duration);
            float currentAngle = Mathf.Lerp(ClosedStateAngle, OpenStateAngle, CurrentOpenAmount);
            // transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);
            RotateAboutPivot(currentAngle, hingeAxis);
            yield return null;  // Wait for the next frame
        }

        // Snap to the exact value at the end to avoid float imprecision
        CurrentOpenAmount = targetNormalizedValue;
        float finalAngle = Mathf.Lerp(ClosedStateAngle, OpenStateAngle, CurrentOpenAmount);
        // transform.localRotation = Quaternion.Euler(0f, finalAngle, 0f);
        RotateAboutPivot(finalAngle, hingeAxis);
        lerpCoroutine = null;

        // Fire events based on final normalized value
        if (Mathf.Approximately(CurrentOpenAmount, 1.0f))
        {
            OnDoorFullyOpen?.Invoke();
            Debug.Log("Door is fully open!");
        }
        else if (Mathf.Approximately(CurrentOpenAmount, 0.0f))
        {
            OnDoorFullyClosed?.Invoke();
            Debug.Log("Door is fully closed!");
        }
    }

    private void RotateAboutPivot(float targetAngle, PivotAxis axis)
    {
        switch (axis)
        {
            case PivotAxis.X:
                transform.localRotation = Quaternion.Euler(targetAngle, 0f, 0f);
                break;
            case PivotAxis.Y:
                transform.localRotation = Quaternion.Euler(0f, targetAngle, 0f);
                break;
            case PivotAxis.Z:
                transform.localRotation = Quaternion.Euler(0f, 0f, targetAngle);
                break;
        }
    }

    // private float CalculateNormalizedOpenAmount(float currentAngle, float closedStateAngle, float maxOpenAngle)
    // {
    //     // Ensure angles are within -180 to 180 degrees
    //     if (currentAngle > 180f) currentAngle -= 360f;
    //     if (closedStateAngle > 180f) closedStateAngle -= 360f;
    //     if (maxOpenAngle > 180f) maxOpenAngle -= 360f;

    //     // Calculate angle difference
    //     float angleDifference = Mathf.Abs(currentAngle - closedStateAngle);
    //     float maxAngleDifference = Mathf.Abs(maxOpenAngle - closedStateAngle);

    //     // Normalize the open amount
    //     var res = Mathf.Clamp01(angleDifference / maxAngleDifference);
    //     return res;
    // }

    private void SetDoorAngle()
    {
        if (IsLocked) return;
        float currentAngle = Mathf.Lerp(ClosedStateAngle, OpenStateAngle, CurrentOpenAmount);
        RotateAboutPivot(currentAngle, hingeAxis);
    }
    #endregion
}
