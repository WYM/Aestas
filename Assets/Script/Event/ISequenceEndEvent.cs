using UnityEngine.EventSystems;
public interface ISequenceEndEvent : IEventSystemHandler
{
    void onSequenceEnd();
}