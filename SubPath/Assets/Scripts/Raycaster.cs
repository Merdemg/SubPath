using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour {

	public float rayRange = 50f;

	private Transform origin;
	private Camera mainCamera;

    private manager gameManager;

	void Start () {
		mainCamera = GetComponent<Camera> ();
        gameManager = FindObjectOfType<manager>();
	}
	
	void Update () {
		
	}

    public void castRay()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //Vector3 rayOrigin = mainCamera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0f));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, rayRange))
        {
            // Ray hit something solid. Bread or topping
            Debug.Log("Ray hit" + hit.transform.name + " "+ hit.point);
            gameManager.spawnItem(hit);
        }

    }
}
