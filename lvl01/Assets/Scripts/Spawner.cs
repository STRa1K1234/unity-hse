using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Спавнер: при запуске игры размещает number копий префаба
/// в настраиваемой области и вызывает у каждой WriteLog(i).
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("Что и сколько спавнить")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int number = 10;

    [Header("Область спавна (центр - сам спавнер)")]
    [SerializeField] private Vector3 areaSize = new Vector3(10f, 0f, 10f);

    // Ссылки на созданные объекты, чтобы потом обойти их все
    private readonly List<SpawnedObject> spawned = new List<SpawnedObject>();

    private void Start()
    {
        SpawnAll();
        WriteLogToAll();
    }

    private void SpawnAll()
    {
        if (prefab == null)
        {
            Debug.LogError("Не назначен префаб в инспекторе спавнера", this);
            return;
        }

        for (int i = 0; i < number; i++)
        {
            Vector3 position = GetRandomPosition();

            // Quaternion.identity - "без поворота"
            // transform третьим аргументом делает спавнер родителем, чтобы
            // иерархия не превращалась в кашу. Можно и без него.
            GameObject instance = Instantiate(prefab, position, Quaternion.identity, transform);

            SpawnedObject logger = instance.GetComponent<SpawnedObject>();
            if (logger != null)
            {
                spawned.Add(logger);
            }
            else
            {
                Debug.LogError("На префабе нет скрипта SpawnedObject", instance);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Random.Range(float, float) включает обе границы.
        // Делим на 2, потому что areaSize - это полный размер области,
        // а спавнер стоит в её центре.
        float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float y = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);
        float z = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);

        return transform.position + new Vector3(x, y, z);
    }

    private void WriteLogToAll()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            spawned[i].WriteLog(i);
        }
    }

    // Рисует область спавна жёлтой рамкой, когда спавнер выделен в сцене.
    // В игре не видно, нужно только для удобной настройки.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
