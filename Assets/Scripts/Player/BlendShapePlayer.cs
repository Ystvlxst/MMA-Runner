using UnityEngine;
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BlendShapePlayer : MonoBehaviour
{
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private float _currentBlendShapeValue;
    private float _blendShapeFactor = 5f;

    private void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _skinnedMeshRenderer.SetBlendShapeWeight(0, 0f);
    }

    public void GaintMuscles()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0, _currentBlendShapeValue += _blendShapeFactor);
    }

    public void ReductionMuscle()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0, _currentBlendShapeValue -= _blendShapeFactor);
    }
}
