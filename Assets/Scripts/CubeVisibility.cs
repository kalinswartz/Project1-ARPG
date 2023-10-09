using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVisibility : MonoBehaviour
{
    [SerializeField] private List<GameObject> models;
    private void Update()
    {
        float minimumAngle = float.MaxValue;
        if (Vector3.Angle(transform.up, Vector3.up) < minimumAngle)  //up
        {
            SetActive(models[5]);//barbarian
            minimumAngle = Vector3.Angle(transform.up, Vector3.up);
        }
        if (Vector3.Angle(-transform.up, Vector3.up) < minimumAngle) //down
        {
            SetActive(models[1]);//wizard
            minimumAngle = Vector3.Angle(-transform.up, Vector3.up);
        }

        if (Vector3.Angle(transform.right, Vector3.up) < minimumAngle) //right
        {
            SetActive(models[2]);//cleric
            minimumAngle = Vector3.Angle(transform.right, Vector3.up);
        }
        if (Vector3.Angle(-transform.right, Vector3.up) < minimumAngle) //left
        {
            SetActive(models[4]);//bard
            minimumAngle = Vector3.Angle(-transform.right, Vector3.up);
        }

        if (Vector3.Angle(transform.forward, Vector3.up) < minimumAngle) //right
        {
            SetActive(models[0]);//paladin
            minimumAngle = Vector3.Angle(transform.forward, Vector3.up);
        }
        if (Vector3.Angle(-transform.forward, Vector3.up) < minimumAngle) //left
        {
            SetActive(models[3]);//rogue
        }
    }
    private void SetActive(GameObject character)
    {
        foreach(GameObject obj in models)
        {
            obj.SetActive(false);
        }
        character.SetActive(true);
    }
}
