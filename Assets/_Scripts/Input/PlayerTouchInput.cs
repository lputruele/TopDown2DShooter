using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Events;

public class PlayerTouchInput : MonoBehaviour, IAgentInput
{

    private Camera mainCamera;
    private bool fireButtonDown = false;

    [SerializeField]
    private Vector2 joystickSize;
    [SerializeField]
    private FloatingJoystick rightJoystick, leftJoystick;

    private Finger movementFinger, shootingFinger;
    private Vector2 leftMovementAmount, rightMovementAmount;

    [field: SerializeField]
    public UnityEvent OnFireButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireButtonReleased { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        FloatingJoystick joystick = MovedFinger == movementFinger ? leftJoystick : rightJoystick;
        Vector2 knobPosition;
        float maxMovement = joystickSize.x / 2f;
        ETouch.Touch currentTouch = MovedFinger.currentTouch;

        if (Vector2.Distance(
                currentTouch.screenPosition,
                joystick.RectTransform.anchoredPosition
            ) > maxMovement)
        {
            knobPosition = (
                currentTouch.screenPosition - joystick.RectTransform.anchoredPosition
                ).normalized
                * maxMovement;
        }
        else
        {
            knobPosition = currentTouch.screenPosition - joystick.RectTransform.anchoredPosition;
        }

        joystick.Knob.anchoredPosition = knobPosition;
        if (MovedFinger == movementFinger)
        {
            leftMovementAmount = knobPosition / maxMovement;
        }
        if (MovedFinger == shootingFinger)
            rightMovementAmount = knobPosition / maxMovement;
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        FloatingJoystick joystick = LostFinger == movementFinger ? leftJoystick : rightJoystick;
        joystick.Knob.anchoredPosition = Vector2.zero;
        joystick.gameObject.SetActive(false);
        if (LostFinger == movementFinger)
        {
            leftMovementAmount = Vector2.zero;
            movementFinger = null;
        }
        if (LostFinger == shootingFinger)
        {
            rightMovementAmount = Vector2.zero;
            shootingFinger = null;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (movementFinger == null && TouchedFinger.screenPosition.x <= Screen.width / 2f)
        {
            movementFinger = TouchedFinger;
            leftMovementAmount = Vector2.zero;
            leftJoystick.gameObject.SetActive(true);
            leftJoystick.RectTransform.sizeDelta = joystickSize;
            leftJoystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
        if (shootingFinger == null && TouchedFinger.screenPosition.x > Screen.width / 2f)
        {
            shootingFinger = TouchedFinger;
            rightMovementAmount = Vector2.zero;
            rightJoystick.gameObject.SetActive(true);
            rightJoystick.RectTransform.sizeDelta = joystickSize;
            rightJoystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < joystickSize.x / 2)
        {
            StartPosition.x = joystickSize.x / 2;
        }

        if (StartPosition.y < joystickSize.y / 2)
        {
            StartPosition.y = joystickSize.y / 2;
        }
        else if (StartPosition.y > Screen.height - joystickSize.y / 2)
        {
            StartPosition.y = Screen.height - joystickSize.y / 2;
        }

        return StartPosition;
    }

    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(leftMovementAmount);
    }

    private void GetFireInput()
    {
        if (shootingFinger != null)
        {
            if (!fireButtonDown)
            {
                fireButtonDown = true;
                OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if (fireButtonDown)
            {
                fireButtonDown = false;
                OnFireButtonReleased?.Invoke();
            }
        }
    }

    private void GetPointerInput()
    {
        //var pointerInWorldSpace = mainCamera.ScreenToWorldPoint(transform.position + (Vector3)rightMovementAmount);
        if (shootingFinger != null && rightMovementAmount != Vector2.zero)
        {
            var pointerInWorldSpace = transform.position + (Vector3)rightMovementAmount;
            OnPointerPositionChange?.Invoke(pointerInWorldSpace);
        }
    }
}