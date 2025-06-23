/// <summary>
/// IAnimatorAdapter is an interface that defines methods for interacting with an animator component.
/// This interface allows for setting and getting animation parameters such as booleans, floats, and triggers.
/// User-defined parameters can be used to control animations in a flexible way.
/// </summary>
public interface IAnimatorAdapter
{
    void SetBool(string param, bool value);
    void SetFloat(string param, float value);
    void SetTrigger(string param);
    bool GetBool(string param);
    float GetFloat(string param);
}
