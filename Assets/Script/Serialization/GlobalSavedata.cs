using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class GlobalSavedata : ISerializable
{

    public int line;

    public GlobalSavedata()
    {
        
    }

    protected GlobalSavedata(SerializationInfo info, StreamingContext context)
    {
        line = info.GetInt32("line");
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("line", line);
    }
}
