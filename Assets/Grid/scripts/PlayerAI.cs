using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] PathFinding _pathfinder;
    public bool isMoving = false;
    public Vector3 oriPos,targetPos;
    public float timeToMove = 0.2f;
    public int pathCount = 2;
    
    public void Start()
    {
        //pathCount = _pathfinder.path.Count - 1;
        transform.position = new Vector3(_pathfinder.startX, 0 , -_pathfinder.startY);
    }

    public void Update()
    {
       /* Input.GetMouseButtonDown(0);
        {
            StartCoroutine(MovePlayer());
        }*/

    }

    public IEnumerator MovePlayer()
    {
        isMoving = true;

        float elapsedTime = 0;

        oriPos = transform.position;
        targetPos = oriPos + _pathfinder.path[pathCount].transform.position;

        print(_pathfinder.path[pathCount].transform.position);

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(oriPos,targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    
}
