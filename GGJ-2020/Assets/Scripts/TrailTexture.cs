using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTexture : MonoBehaviour
{
    private RenderTexture rt;
    public Shader drawShader;
    private Material drawMaterial;
    private Material mMaterial;
    public GameObject terrain;
    public Transform[] rayCastPositions;
    private RaycastHit groundHit;
    private int layerMask;
    [Range(1, 500)] public float brushSize = 1;
    [Range(0, 1)] public float brushStrength = 0;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Pavement");
        drawMaterial = new Material(drawShader);
        mMaterial = terrain.GetComponent<MeshRenderer>().material;
        mMaterial.SetTexture("_Splat", rt = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rayCastPositions.Length; i++)
        {
            if (Physics.Raycast(rayCastPositions[i].position, -Vector3.up, out groundHit, 1f, layerMask))
            {
                drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Strength", brushStrength);
                drawMaterial.SetFloat("_Size", brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(rt.width, rt.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(rt, temp);
                Graphics.Blit(temp, rt, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }

        }
    }
}
