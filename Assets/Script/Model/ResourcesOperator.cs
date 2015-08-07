using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;

public class ResourcesOperator : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    //载入Sprite
    public static Sprite LoadSprite(string path)
    {
        return Resources.Load(path, typeof(Sprite)) as Sprite;
    }

    //从WWW载入Sprite
    public static Sprite LoadSpriteWWW(string path)
    {
        return Resources.Load(path) as Sprite;
    }

    //载入Material
    public static Material LoadMaterial(string path)
    {
        return Resources.Load(path, typeof(Material)) as Material;
    }

    //载入AudioClip
    public static AudioClip LoadAudioClip(string path)
    {
        return Resources.Load(path, typeof(AudioClip)) as AudioClip;
    }

    //载入Streaming CSV
    public static ArrayList LoadTextStream(string path)
    {
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path);
        }
        catch (Exception ex)
        {
            Debug.Log("[RO] Open file error.");
            return null;
        }

        string line;
        ArrayList arrlist = new ArrayList();

        while ((line = sr.ReadLine()) != null)
        {
            if (line != "")
            {
                arrlist.Add(line);
            }
        }

        sr.Close();
        sr.Dispose();
        return arrlist;
    }

    //载入AGC脚本
    public static AgcParser LoadAgc(string filename, bool isScript = false)
    {
        return new AgcParser(filename, isScript, G.agcEncryptKey);
    }

    public static AudioClip LoadSFX(string sfx)
    {
        return Resources.Load(G.sfxPath + sfx, typeof(AudioClip)) as AudioClip;
    }

}
