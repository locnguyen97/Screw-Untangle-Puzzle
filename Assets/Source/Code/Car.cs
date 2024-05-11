using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    private Vector2 localScale;
    public int id = 0;
    private void Start()
    {
        localScale = transform.localScale;
        id = transform.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.canDrag)
        {
            GameManager.Instance.canDrag = false;
            if (id == GameManager.Instance.idCur)
            {
                transform.DOScale(localScale*1.5f, 0.2f).OnComplete(() =>
                {
                    GameObject explosion = Instantiate(GameManager.Instance.particleVFXs[Random.Range(0,GameManager.Instance.particleVFXs.Count)], transform.position, transform.rotation);
                    Destroy(explosion, .75f);
                    GameManager.Instance.GetCurLevel().RemoveObject(gameObject);
                    Destroy(gameObject);
                    GameManager.Instance.idCur--;
                    GameManager.Instance.EnableDrag();
                });
            }
            else
            {
                transform.DOScale(localScale*1.2f, 0.1f).OnComplete(() =>
                {
                    transform.DOScale(localScale*1f, 0.1f).OnComplete(() =>
                    {
                        GameManager.Instance.EnableDrag();
                    });
                });
            }
        }
    }
    
}
