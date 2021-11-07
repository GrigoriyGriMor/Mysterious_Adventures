using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDropObject : MonoBehaviour, IDragHandler //, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)

    {

      transform.position = eventData.pointerCurrentRaycast.screenPosition;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(transform.name);
    }

}
