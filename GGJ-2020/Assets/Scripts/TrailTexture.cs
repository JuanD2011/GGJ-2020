using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTexture : MonoBehaviour
{
    public Shader drawShader;
    private Material drawMaterial;
    public Transform[] rayCastPositions;
    private RaycastHit groundHit;
    private int layerMask;
    [Range(1, 500)] public float brushSize = 1;
    [Range(0, 1)] public float brushStrength = 0;

    // Start is called before the first frame update
    void Start()
    {
        Pavement[] pavements = FindObjectsOfType<Pavement>();
        layerMask = LayerMask.GetMask("Pavement");
        drawMaterial = new Material(drawShader);
        foreach(Pavement pavement in pavements)
        {
            if (!pavement.textureDefined)
            {
                pavement.mMeshRenderer.material.SetTexture("_Splat",
                    pavement.rt = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
                pavement.textureDefined = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rayCastPositions.Length; i++)
        {
            if (Physics.Raycast(rayCastPositions[i].position, -Vector3.up, out groundHit, 10f, layerMask))
            {
                Pavement currentPavement = groundHit.collider.GetComponent<Pavement>();
                drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
                //drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.point.x, groundHit.point.y, groundHit.point.z, 0));
                drawMaterial.SetFloat("_Strength", brushStrength);
                drawMaterial.SetFloat("_Size", brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(currentPavement.rt.width, currentPavement.rt.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(currentPavement.rt, temp);
                Graphics.Blit(temp, currentPavement.rt, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}
