using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool canDrag = true;
    private int startIndex = 0;

    private int currentIndex;
    public  List<GameObject> particleVFXs;
    [SerializeField] private List<GameLevel> levels;

    public int idCur;
    private void Awake()
    {
        idCur = -1;
        if (Instance == null)
        {
            Instance = this;
        }
        currentIndex = startIndex;
        levels[currentIndex].gameObject.SetActive(true);
        
    }

    public void CheckLevelUp()
    {
        canDrag = false;
        GameObject explosion = Instantiate(particleVFXs[Random.Range(0,particleVFXs.Count)], transform.position, transform.rotation);
        Destroy(explosion, .75f);
        Invoke(nameof(NextLevel),1.0f);
    }

    void NextLevel()
    {
        idCur = -1;
        levels[currentIndex].gameObject.SetActive(false);
        currentIndex++;
        if (currentIndex >= 3)
        {
            currentIndex = startIndex;
            canDrag = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            levels[currentIndex].gameObject.SetActive(true);
            canDrag = true;
        }
        
    }
    
    
    Vector3 offset;

    void Update()
    {
        if(!canDrag) return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = 0;
        /*if (Input.GetMouseButtonDown(0))
        {
            var targetObject = Physics.OverlapSphere(mousePosition,0.2f);
            booxx.position = mousePosition;
            if (targetObject != null)
            {
                if (targetObject.Length != 0)
                {
                    var tp = targetObject[0].GetComponent<Car>();
                    if (tp != null)
                    {
                        if (tp.index == idxGame)
                        {
                            tp.Move();
                            canDrag = false;
                        }
                    }
                }
            }
        }*/
    }
    
    

    public void EnableDrag()
    {
        canDrag = true;
    }
    public GameLevel GetCurLevel()
    {
        return levels[currentIndex];
    }
}