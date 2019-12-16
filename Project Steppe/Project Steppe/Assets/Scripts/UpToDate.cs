using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpToDate : MonoBehaviour
{
    public bool upToDate = false;

    void Start()
    {
        upToDate = false;
    }

    void Update()
    {
        if (!upToDate) // is lookup texture not up to date? 
        {
            upToDate = true;
            var texture = new Texture2D(16, 16);
            texture.filterMode = FilterMode.Bilinear;
            texture.wrapMode = TextureWrapMode.Clamp;

            //GetComponent(SkinnedMeshRenderer).sharedMaterial.SetTexture("_LookupTexture", texture);  
            for (int j = 0; j < texture.height; j++)
            {
                for (int i = 0; i < texture.width; i++)
                {
                    //  float x = (i + 0.5) / texture.width;
                    // float y = (j + 0.5) / texture.height;
                    //  Color color = (0.0, 0.0, 0.0, (1.0 - x) * (1.0 - x));
                    // texture.SetPixel(i, j, color);
                }
            }
            texture.Apply(); // apply all the texture.SetPixel(...) commands
        }
    }
}
