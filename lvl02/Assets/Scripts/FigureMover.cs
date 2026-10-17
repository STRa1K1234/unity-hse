using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Движение всей фигуры на WASD. Висит на корневом объекте фигуры,
/// поэтому вместе с ним едут и центральный куб, и орбита с детьми, и камера.
/// </summary>
public class FigureMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        float horizontal = 0f;
        float vertical = 0f;

        if (keyboard.aKey.isPressed) horizontal -= 1f;
        if (keyboard.dKey.isPressed) horizontal += 1f;
        if (keyboard.sKey.isPressed) vertical -= 1f;
        if (keyboard.wKey.isPressed) vertical += 1f;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // по диагонали скорость не должна быть выше, чем по прямой
        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }

        transform.position += direction * (speed * Time.deltaTime);
    }
}
