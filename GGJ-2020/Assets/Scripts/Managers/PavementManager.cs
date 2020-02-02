using UnityEngine;
using System.Collections.Generic;

public class PavementManager : MonoBehaviour
{
    [SerializeField] private CurrentLevelData levelData = null;

    [SerializeField] private Pavement pavementModulePrefab = null;
    [SerializeField] private Transform pavementParent = null;
    private List<Pavement> pavements = new List<Pavement>();
    private float displacementCounter = 0f;
    private int sideSwitcher = 1;


    private void Awake()
    {
        for (int i = 0; i < levelData.levelData.pavementModulesNumber; i++)
        {
            pavements.Add(Instantiate(pavementModulePrefab, pavementParent));
        }

        if (pavements.Count == 1)
        {
            pavements[0].transform.position = new Vector3(0f, pavements[0].transform.position.y, pavements[0].transform.position.z);
            return;
        }
        else if (pavements.Count % 2 == 0)
        {
            displacementCounter = 2.5f;
            int counter = 0;
            foreach (Pavement pavement in pavements)
            {
                pavement.transform.position = new Vector3(displacementCounter * sideSwitcher, pavement.transform.position.y, pavement.transform.position.z);
                counter++;
                sideSwitcher *= -1;
                if (counter == 2)
                {
                    counter = 0;
                    displacementCounter += 2.5f;
                }
            }
        }
        else
        {
            pavements[0].transform.position = new Vector3(0f, pavements[0].transform.position.y, pavements[0].transform.position.z);
            displacementCounter = 5f;
            int counter = 0;
            for (int i = 1; i < pavements.Count; i++)
            {
                pavements[i].transform.position = new Vector3(displacementCounter * sideSwitcher, pavements[i].transform.position.y, pavements[i].transform.position.z);
                counter++;
                sideSwitcher *= -1;
                if (counter == 2)
                {
                    counter = 0;
                    displacementCounter += 5f;
                }
            }
        }

    }
}
