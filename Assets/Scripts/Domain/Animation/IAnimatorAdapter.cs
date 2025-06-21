public interface IAnimatorAdapter
{
    void SetBool(string param, bool value);
    void SetFloat(string param, float value);
    void SetTrigger(string param);
    bool GetBool(string param);
    float GetFloat(string param);
}
