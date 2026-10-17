using UnityEngine;

/// <summary>
/// Скрипт висит на префабе (кубе). Спавнер вызывает у него WriteLog
/// после того, как разместит все объекты.
/// </summary>
public class SpawnedObject : MonoBehaviour
{
    // Метод из задания: сигнатура ровно такая, как просили - void WriteLog(int i)
    public void WriteLog(int i)
    {
        // Второй аргумент Debug.Log - "контекст": по клику на сообщение
        // в консоли Unity подсветит в иерархии именно этот объект.
        Debug.Log($"{name}: i = {i}", this);
    }
}
