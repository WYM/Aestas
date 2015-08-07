using UnityEngine;
using System.Collections;

public class MemoriesList : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Back_OnClick()
    {
        Application.LoadLevel("Title");
    }
}
