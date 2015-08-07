using UnityEngine;
using System.Collections;
using System.IO;

public class AgcParser{

    public string agcName;
    public string workingPath;
    public bool isScript;
    public CsvParser cps;

    public int nl = 0;

    public AgcParser(string filename, bool isScript = false, string key = "")
    {
        agcName = filename + ".agc";
        string agcFolder = isScript ? "script/" : "config/";
        workingPath = G.tempPath + agcFolder + agcName;

        //解除加密，放入Temp文件夹
        if (key != "")
        {
            Encrypt.DecryptFile(G.agcPath + agcFolder + agcName, workingPath, key);
        }
        else
        {
            File.Copy(G.agcPath + agcFolder + agcName, workingPath);
        }

        cps = new CsvParser(workingPath); //建立CsvParser对象
        cps.Load();
    }

    public string Q(string strName)
    {
        return cps.GetField(nl, strName);
    }
}
