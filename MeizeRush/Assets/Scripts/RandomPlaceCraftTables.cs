using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaceCraftTables : MonoBehaviour
{
  public GameObject craftTablePrefab;
  public int minObjects;
  public int maxObjects;
  public float minDistance = 5.0f;
  public Vector3 areaSize = new Vector3(50, 50, 0);

  private bool isOnRange;

  void Start() { InstantiateObjectsRandomly(); }

  void InstantiateObjectsRandomly()
  {
    int objectsCount = Random.Range(minObjects, maxObjects + 1);
    List<Vector3> potentialPositions = GeneratePotentialPositions();
    List<Vector3> chosenPositions = new List<Vector3>();

    for (int i = 0; i < objectsCount; i++)
    {
      if (potentialPositions.Count == 0)
        break;

      // Select a random position from the potential positions
      int index = Random.Range(0, potentialPositions.Count);
      Vector3 position = potentialPositions[index];
      chosenPositions.Add(position);
      potentialPositions.RemoveAt(index);

      // Remove nearby positions to enforce minimum distance
      potentialPositions.RemoveAll(p => Vector3.Distance(p, position) <
                                        minDistance);

      Instantiate(craftTablePrefab, position, Quaternion.identity);
    }
  }

  List<Vector3> GeneratePotentialPositions()
  {
    List<Vector3> positions = new List<Vector3>();
    int gridSize =
        Mathf.CeilToInt(Mathf.Max(areaSize.x, areaSize.y) / minDistance);

    for (int x = 0; x < gridSize; x++)
    {
      for (int y = 0; y < gridSize; y++)
      {
        Vector3 pos = new Vector3(
            -areaSize.x / 2 + (x * minDistance + minDistance / 2), -areaSize.y / 2 + (y * minDistance + minDistance / 2), 0);

        if (pos.x <= areaSize.x / 2 && pos.y <= areaSize.y / 2)
          positions.Add(pos);
      }
    }

    return positions;
  }
}
