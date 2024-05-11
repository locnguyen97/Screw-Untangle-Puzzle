using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TouchPoint : MonoBehaviour
{
    private bool isSelected = false;
    [SerializeField] List<GameObject> particleVFXs;
    private Vector3 startPos;

    private void OnEnable()
    {
        startPos = transform.position;
    }

    public void Move()
    {
        int i = GameManager.Instance.GetCurLevel().gameObjectsPoint.IndexOf(gameObject);

    }

    IEnumerator Move(Transform target,int index)
    {
        var dis = Vector2.Distance(target.position , transform.position);
        var dir = target.position - transform.position;
        while (dis > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            transform.position = transform.position + dir * 0.1f;
            dis = Vector2.Distance(target.position , transform.position);
        }
        gameObject.SetActive(false);
        GameManager.Instance.EnableDrag();
    }
}