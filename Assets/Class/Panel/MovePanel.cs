using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MovePanel : MonoBehaviour, IPointerClickHandler
{

    public GameObject targetObject;
    public GameObject currentObiect;
    private Vector2 currentPos;
    private Vector2 startPos;

    private void Start()
    {
        startPos = currentObiect.transform.position;
        currentPos = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public void Move()
    {
        iTween.MoveTo(targetObject, currentPos, 1f);
        iTween.MoveTo(currentObiect, startPos, 1f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Move();
    }
}
