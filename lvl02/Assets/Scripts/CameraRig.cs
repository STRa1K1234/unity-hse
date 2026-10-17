using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Висит на пустом объекте в центре фигуры, камера - его дочерний объект.
/// Стрелки вращают этот объект, то есть камера облетает фигуру.
/// Ехать за фигурой камере не нужно отдельно: она дочерняя, поэтому едет сама.
/// </summary>
public class CameraRig : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float startPitch = 20f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 80f;

    private float yaw;
    private float pitch;

    private void Awake()
    {
        pitch = startPitch;
        ApplyRotation();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        float horizontal = 0f;
        float vertical = 0f;

        if (keyboard.leftArrowKey.isPressed) horizontal -= 1f;
        if (keyboard.rightArrowKey.isPressed) horizontal += 1f;
        if (keyboard.downArrowKey.isPressed) vertical -= 1f;
        if (keyboard.upArrowKey.isPressed) vertical += 1f;

        yaw += horizontal * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch - vertical * rotationSpeed * Time.deltaTime, minPitch, maxPitch);

        ApplyRotation();
    }

    // LateUpdate - после всех Update, когда фигура уже сдвинулась за этот кадр
    private void LateUpdate()
    {
        if (target != null && cameraTransform != null)
        {
            cameraTransform.LookAt(target);
        }
    }

    private void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
