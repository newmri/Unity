using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour {
    public Material mat;
    void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = new Vector3[]
        {
            new Vector3(0.0f,0.0f,0.0f), new Vector3(0.0f,1.0f,0.0f),
            new Vector3(1.0f,1.0f,0.0f), new Vector3(1.0f,0.0f,0.0f)
        };
        mesh.uv = new Vector2[]
        {
            new Vector2(0.0f,0.0f), new Vector2(0.0f,1.0f),
            new Vector2(1.0f,1.0f), new Vector2(1.0f,0.0f)
        };
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        if (mat != null) GetComponent<Renderer>().material = mat;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MakeResizeByImageSize();

    }
    void MakeResizeByImageSize()
    {
        float one_unit = 2.0f / Screen.height;
        float x = one_unit * mat.mainTexture.width;
        float y = one_unit * mat.mainTexture.height;
        Vector3 newSclae = new Vector3(x, y, one_unit);
        transform.localScale = newSclae;
    }
}
