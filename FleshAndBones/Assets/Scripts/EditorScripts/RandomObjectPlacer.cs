using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RandomObjectPlacer : ScriptableWizard
{
    public List<GameObject> objectsToPlace;
    public Transform mapBottomLeftCorner;
    public Transform mapTopRightCorner;
    public int numberOfObjectsToPlace;
    public bool randomRotation;

    [MenuItem("Custom Tools/Random Object Placer")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<RandomObjectPlacer>("Place Random Objects", "Place");
    }

    void OnWizardCreate()
    {
        if (objectsToPlace.Count == 0)
        {
            Debug.LogError("No objects to place");
            return;
        }

        for (int i = 0; i < numberOfObjectsToPlace; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(mapBottomLeftCorner.position.x, mapTopRightCorner.position.x),
                Random.Range(mapBottomLeftCorner.position.y, mapTopRightCorner.position.y)
            );

            GameObject objectToPlace = objectsToPlace[Random.Range(0, objectsToPlace.Count)];
            GameObject newObject = null;
            if (randomRotation)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                newObject = Instantiate(objectToPlace, randomPosition, rotation);
            }
            else
                newObject = Instantiate(objectToPlace, randomPosition, Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(newObject, "Create " + newObject.name);
        }
    }
}
