using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public List<GameObject> gameObjectsPoint;
    [SerializeField] private Transform parentListObjPoint;
    private bool canCheck = true;
    public void Start()
    {
        canCheck = true;
        foreach (Transform tr in parentListObjPoint)
        {
            if (tr.gameObject.activeSelf)
            {
                gameObjectsPoint.Add(tr.gameObject);
                GameManager.Instance.idCur++;
            }

        }
    }

    public void RemoveObject(GameObject obj)
    {
        gameObjectsPoint.Remove(obj);
        if (canCheck)
        {
            if (gameObjectsPoint.Count == 0)
            {
                GameManager.Instance.CheckLevelUp();
                canCheck = false;
            }
        }
    }
}
