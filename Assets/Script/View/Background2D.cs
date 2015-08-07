using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Background2D : MonoBehaviour 
{

    public GameObject Back;
    public GameObject Mask;
    public GameObject BackgroundCamera;

    Image BackImg;
    Image MaskImg;

	void Start () 
    {
        BackImg = Back.GetComponent<Image>();
        MaskImg = Mask.GetComponent<Image>();
        ChangeImg("background");
        ApplyFilter("Ex");
	}

    public void ChangeImg(string imgName,bool isCG = false) 
    {

        Debug.Log(G.bgPath + imgName);
        BackImg.overrideSprite = ResourcesOperator.LoadSprite(G.bgPath + imgName);
    }

    public void ApplyFilter(string filter)
    {
        switch (filter)
        {
            case "blur":
                BackImg.material = ResourcesOperator.LoadMaterial("Shader/ImageBlurMat");
                break;
            case "PureRed":
            case "PureGreen":
            case "PureBlue":
            case "Transparent50":
            case "Ex": //测试用EX材质
                BackImg.material = ResourcesOperator.LoadMaterial("Shader/" + filter + "Mat");
                break;
            case "clean":
            case "default":
                BackImg.material = ResourcesOperator.LoadMaterial("Shader/NormalMat");
                break;
            default:
                break;
        }
    }

	void Update () 
    {
	
	}
}
