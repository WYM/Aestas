using UnityEngine;
using System.Collections;

public class TalkLetter : MonoBehaviour {

    public enum TalkLetterType{Single, Double, Triple};

    public TalkLetterType type;
    public float offset;
    
	void Start ()
    {
        offset = 50;
	}
	
	void Update ()
    {
	
	}

    public void SwitchTo(TalkLetterType t)
    {
        type = t;
        if (t == TalkLetterType.Single)
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 64);
            offset = 20;
        }
        else if(t == TalkLetterType.Double)
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(48, 64);
            offset = 50;
        }
    }
}
