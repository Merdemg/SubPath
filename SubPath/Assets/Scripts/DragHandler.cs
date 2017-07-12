using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;

    Vector3 startPos;

    public static Raycaster raycaster;

    [SerializeField] private GameObject prefabToSpawn;
    private manager gameManager;

    void Start()
    {
        raycaster = FindObjectOfType<Raycaster>();
        gameManager = FindObjectOfType<manager>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPos = transform.position;

        gameManager.setPrefab(prefabToSpawn);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPos;

        raycaster.castRay();

        itemBeingDragged = null;
    }
}
