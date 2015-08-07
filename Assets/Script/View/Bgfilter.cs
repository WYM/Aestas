using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bgfilter : MonoBehaviour {

    Image filterImage;

    float timer;
    bool isFade = false;

	// Use this for initialization
	void Start () 
    {
        filterImage = this.gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer > 0)
        {
            if (isFade)
            {
                filterImage.CrossFadeAlpha(0.5f, 1.8f, false);
                isFade = false;
            }
            else
            {
                filterImage.CrossFadeAlpha(0.1f, 1.8f, false);
                isFade = true;
            }
            timer -= 2;
        }
	}
}
