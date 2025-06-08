using UnityEngine;
using UnityEditor;

public class AddCanMoveKeyframe : MonoBehaviour
{
    [MenuItem("Tools/Add canMove to knight_attack")]
    static void AddKey()
    {
        Animator animator = Selection.activeGameObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Selecciona el GameObject con el Animator primero.");
            return;
        }

        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(
            "Assets/Characters/Enemies/KnightEnemy/knight_attack.anim"
        );

        if (clip == null)
        {
            Debug.LogError("No se encontró la animación knight_attack.");
            return;
        }

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, 0f); // canMove = false

        clip.SetCurve("", typeof(Animator), "canMove", curve);
        Debug.Log("Parámetro 'canMove' agregado a la animación.");
    }
}

