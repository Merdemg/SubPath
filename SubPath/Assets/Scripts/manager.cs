using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour {
    private float errorMessageDuration = 1.0f;

    [SerializeField] private GameObject errorPanel;
    bool hasBread = false;

    //switch later
    public GameObject prefabToSpawn;
    private GameObject Bread;
    private GameObject ChosenBreadPrefab;

	// Use this for initialization
	void Start () {
        errorPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void spawnItem(RaycastHit hit)
    {
        if(prefabToSpawn.tag == "Bread" && !hasBread)
        {
            // Put bread
            hasBread = true;
            Bread = spawn(hit);
            ChosenBreadPrefab = prefabToSpawn;
        } else if(prefabToSpawn.tag == "Bread" && hasBread)
        {
            // You can't have 2 buns!
            error("You already have a bun!");

        }else if (prefabToSpawn.tag == "Topping" && !hasBread)
        {
            //You need a bun first silly!
            error("Choose the bread type first!");

        }else if (prefabToSpawn.tag == "Topping" && hasBread)
        {
            if(hit.collider.tag == "Bread" || hit.collider.tag == "Topping")
            {
                // Put topping
                spawn(hit).transform.SetParent(Bread.transform);
            }else
            {
                // Oops you missed the bun! Don't put the topping on the table
                error("Can't put the topping on the table!");
            }
            
        }
        


    }

    public void done()
    {
        if (hasBread)
        {
            Vector3 pos = Bread.transform.position + new Vector3(0, 1, 0);
            (Instantiate(ChosenBreadPrefab, pos, Bread.transform.rotation)).transform.SetParent(Bread.transform);

            GameObject.FindGameObjectWithTag("MainPanel").SetActive(false);

            pos = Bread.transform.position + new Vector3(0, 0, -4);
            
            GameObject.FindGameObjectWithTag("MainCamera").transform.SetPositionAndRotation(pos, this.transform.rotation);

        }
        else
        {
            // You haven't even started yet?!
            error("You don't have a sandwich yet!");
        }
    }

    public void error(string errorMessage)
    {
        errorPanel.GetComponentInChildren<Text>().text = errorMessage;
        Debug.Log("Error: " + errorMessage);
        StartCoroutine(handleErrorUI());
    }

    public void setPrefab(GameObject prefab)
    {
        prefabToSpawn = prefab;
    }

    private GameObject spawn(RaycastHit hit)
    {
        
        Vector3 pos = hit.point + new Vector3(0, 1, 0);
        return Instantiate(prefabToSpawn, pos, prefabToSpawn.transform.rotation);
    }

    private IEnumerator handleErrorUI()
    {
        errorPanel.SetActive(true);

        yield return new WaitForSeconds(errorMessageDuration);
        errorPanel.SetActive(false);

    }
    
}
