using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
   [SerializeField] GameObject pathPart;
    Vector3 lastChildPosition;
    Vector3 halfSizeOfPathPart;
    int childIndex = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        lastChildPosition = this.gameObject.transform.GetChild(transform.childCount - 1).transform.position;
        halfSizeOfPathPart = (this.gameObject.transform.GetChild(transform.childCount - 1).GetComponent<BoxCollider>().bounds.size)/2f;
    }
    public void StarPathManager()
    {
        InvokeRepeating("CreateNewPathPart", 1f, PlayerController.speedMultiplier / 2f);
        InvokeRepeating("DestroyPathPart", 1f, PlayerController.speedMultiplier);
    }
    private void CreateNewPathPart()
    {
        Vector3 spawnPosition;
        float diceRoll = Random.Range(0, 6);
        if (diceRoll <= 3)
        {
            spawnPosition = new Vector3(lastChildPosition.x + halfSizeOfPathPart.x, lastChildPosition.y, lastChildPosition.z + halfSizeOfPathPart.z);
        }
        else
        {
            spawnPosition = new Vector3(lastChildPosition.x - halfSizeOfPathPart.x, lastChildPosition.y, lastChildPosition.z + halfSizeOfPathPart.z);
        }

        GameObject newPathPart = Instantiate(pathPart, spawnPosition,Quaternion.Euler(0,45,0));
        newPathPart.transform.parent = gameObject.transform;

        lastChildPosition = newPathPart.transform.position;

        if(gameObject.transform.childCount % 4 == 0)
        {
            newPathPart.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void DestroyPathPart()
    {
        if(gameObject.transform.childCount > 20)
        {
            gameObject.transform.GetChild(0+childIndex).gameObject.SetActive(false);
            childIndex++;
        }
    }
}
