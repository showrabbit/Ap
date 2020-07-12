using System;

using UnityEngine;

public class CameraShadow : Ap.Core.MonoBehaviourEx
{
    public GameObject LightObj;
    public Camera Camera;

    public RenderTexture RenderTexture;

    public MeshRenderer PlaneRenderer;


    public void Start()
    {
        Camera.SetReplacementShader(Shader.Find("Test/ShadowDepth"), "RenderType");
        //this.transform.rotation = LightObj.transform.rotation;
        //this.transform.position = LightObj.transform.position;


        //PlaneRenderer.material.SetTexture("_DepthTex", Camera.targetTexture);
        Matrix4x4 worldToView = Camera.worldToCameraMatrix;
        Matrix4x4 projection = GL.GetGPUProjectionMatrix(Camera.projectionMatrix, false);
        //Shader.SetGlobalMatrix("_LightProject", worldToView * projection);
        PlaneRenderer.material.SetMatrix("_LightProject", projection * worldToView );
    }
    
    

}

