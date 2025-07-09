using UnityEngine;

public class CurveControl : MonoBehaviour
{
    [Range(-1, 1)] public float sidewaysStrength = 0f;
    [Range(-1, 1)] public float backwardsStrength = 0f;

    public Material[] materials;

    void Update()
    {
        foreach (var mat in materials)
        {
            mat.SetFloat(Shader.PropertyToID("_SidewaysStrength"), sidewaysStrength);
            mat.SetFloat(Shader.PropertyToID("_BackwardsStrength"), backwardsStrength);
        }
    }
}
