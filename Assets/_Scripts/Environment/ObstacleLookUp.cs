using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public static class ObstacleLookUp
{
    // Dictionary to map a string to each Obstacle object.
    private static Dictionary<string, GameObject> ObstacleDictionary;

    // We'll build our dictionary in the static constructor.
    static ObstacleLookUp()
    {
        // We can load all the Obstacles from that folder.
        var Obstacles = Resources.LoadAll<GameObject>("Prefabs/Obstacle");
        ObstacleDictionary =
            new Dictionary<string, GameObject>(Obstacles.Length);

        foreach (GameObject Obstacle in Obstacles)
        {
            ObstacleDictionary.Add(Obstacle.name, Obstacle);
        }
    }

    public static void SpawnObstacle(string ObstacleName, float x, float y)
    {
        if (ObstacleDictionary.ContainsKey(ObstacleName))
        {
            GameObject drop = Object.Instantiate(
                ObstacleDictionary[ObstacleName],
                new Vector3(x, y, 0f), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Obstacle with " + ObstacleName + "could not be " +
                "found and spawned.");
        }
    }
}

