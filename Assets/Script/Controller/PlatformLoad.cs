using UnityEngine;
using System.Collections;

public class PlatformLoad : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
#if UNITY_ANDROID
        Application.LoadLevel("Title2");
#endif

#if UNITY_IPHONE
        Application.LoadLevel("Title2");
#endif

#if UNITY_STANDALONE_WIN
        Application.LoadLevel("Title2");
#endif
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
