using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// https://github.com/Unity-Technologies/PostProcessing/wiki/Writing-Custom-Effects
[System.Serializable]
[CreateAssetMenu(fileName = "Vignette Blur", menuName = "Custom Post Processing Passes/Vignette Blur")]
public class VignetteBlur : ScriptableRendererFeature {
    public Material m_Material;

    VignetteBlitPass m_RenderPass = null;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType == CameraType.Game)
        {
            //Calling ConfigureInput with the ScriptableRenderPassInput.Color argument ensures that the opaque texture is available to the Render Pass
            m_RenderPass.ConfigureInput(ScriptableRenderPassInput.Color);
            m_RenderPass.SetTarget(renderer.cameraColorTarget);
            renderer.EnqueuePass(m_RenderPass);
        }
    }

    public override void Create()
    {
        m_RenderPass = new VignetteBlitPass(m_Material);
    }

    protected override void Dispose(bool disposing)
    {
        CoreUtils.Destroy(m_Material);
    }
}