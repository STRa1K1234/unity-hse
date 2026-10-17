using UnityEngine;

/// <summary>
/// Единственный скрипт вращения в задании. Крутит СВОЙ объект вокруг заданной
/// локальной оси с заданной скоростью. Переиспользуется и для орбиты, и для
/// собственного вращения каждого куба - меняются только ось и скорость в инспекторе.
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 axis = Vector3.up;
    [SerializeField] private float degreesPerSecond = 45f;

    private void Update()
    {
        // Space.Self - вращаемся вокруг собственной оси объекта.
        // Орбита получается сама собой: дети вращающегося объекта едут вместе с ним.
        transform.Rotate(axis, degreesPerSecond * Time.deltaTime, Space.Self);
    }
}
