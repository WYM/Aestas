using UnityEngine;
using System.Collections;

public class MoviePlay : MonoBehaviour {

    public MovieTexture movTexture_StudioASC;
    public AudioSource audio_StudioASC;
    public float timer = 0;

	// Use this for initialization
    void Start()
    {
        movTexture_StudioASC.Play();
        audio_StudioASC.Play();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > 17 || Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture_StudioASC, ScaleMode.ScaleToFit);
    }

    void NextScene()
    {
        movTexture_StudioASC.Stop();
        audio_StudioASC.Stop();
        Application.LoadLevel("Title2");
    }
}