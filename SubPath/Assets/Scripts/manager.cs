using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour {

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

        }else if (prefabToSpawn.tag == "Topping" && !hasBread)
        {
            //You need a bun first silly!

        }else if (prefabToSpawn.tag == "Topping" && hasBread && hit.collider.tag =="Bread")
        {
            // Put topping
            spawn(hit).transform.SetParent(Bread.transform);
        }
        else
        {
            //Opps you missed the bun!
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
        }
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

    
}
