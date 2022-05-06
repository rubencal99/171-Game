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
        var Obstacles = Resources.LoadAll<GameObject>("Obstacles");
        //Debug.Log("Obstacles length: " + Obstacles.Length);
        ObstacleDictionary =
            new Dictionary<string, GameObject>(Obstacles.Length);

        foreach (GameObject Obstacle in Obstacles)
        {
            ObstacleDictionary.Add(Obstacle.name, Obstacle);
        }
        //Debug.Log("Obstacles Dictionary: " + ObstacleDictionary);
    }

    public static void SpawnObstacle(string ObstacleName, float x, float z)
    {
        //Debug.Log("In spawn obstacle");
        if (ObstacleDictionary.ContainsKey(ObstacleName))
        {
            GameObject drop = Object.Instantiate(
                ObstacleDictionary[ObstacleName],
                new Vector3(x, 1f, z), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Obstacle with " + ObstacleName + " could not be " +
                "found and spawned.");
        }
    }
}

