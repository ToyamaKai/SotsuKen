using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class OutlineEffect : MonoBehaviour
{
    public Shader outlineShader;
    public Color outlineColor = Color.black;
    public float outlineThickness = 1.0f;
    public float depthThreshold = 0.01f;
    public float normalThreshold = 0.1f;

    private Material outlineMaterial;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode |= DepthTextureMode.DepthNormals;

        if (outlineShader != null)
            outlineMaterial = new Material(outlineShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (outlineMaterial != null)
        {
            outlineMaterial.SetColor("_OutlineColor", outlineColor);
            outlineMaterial.SetFloat("_OutlineThickness", outlineThickness);
            outlineMaterial.SetFloat("_DepthThreshold", depthThreshold);
            outlineMaterial.SetFloat("_NormalThreshold", normalThreshold);

            Graphics.Blit(src, dest, outlineMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
