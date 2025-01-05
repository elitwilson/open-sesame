using UnityEngine;

public interface IOpenable
{
    bool IsOpen { get; }
    bool ToggleOpen();
}

public interface ISwingable
{
    void Open();
    void Open(float normalizedAmount);   
}
