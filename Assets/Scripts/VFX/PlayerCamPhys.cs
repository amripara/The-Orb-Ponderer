using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using Cinemachine.PostFX;

using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class PlayerCamPhys : MonoBehaviour
{

    [Range(0, 100), SerializeField]
    float min_v, max_v;

    [SerializeField]
    CinemachineVirtualCamera cam;
    [SerializeField]
    CinemachineVolumeSettings cvs;

    [Range(0, 1), SerializeField, SaveDuringPlay]
    float ca0, ld0, vi0, fg0;

    public bool manual_mode;
    [Range(0, 1)]
    public float manual_debug_vfx_t;

    [SerializeField]
    Material mat;

    enum TLerpFormula
    {
        linear,
        ease_in,
        ease_out,
        ease_in_out
    }

    [SerializeField]
    TLerpFormula lerp_formula;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // somehow c# doesn't accept the version without the extra lines
        float t = (manual_mode || PlayerController.Instance == null) ?
            manual_debug_vfx_t :
            (PlayerController.Instance.speed - min_v) / (max_v - min_v);

        t = Mathf.Clamp01(t);

        // https://www.febucci.com/2018/08/easing-functions/
        // the following uses quadratic (polynomial) lerps to reinterpret the t value
        // switching to higher powers will cause more drastic shifts
        switch (lerp_formula)
        {
            case TLerpFormula.linear:
                break;
            case TLerpFormula.ease_in:
                t *= t;
                break;
            case TLerpFormula.ease_out:
                t = 1 - (1 - t) * (1 - t);
                break;
            case TLerpFormula.ease_in_out:
                t = Mathf.Lerp(t * t, 1 - (1 - t) * (1 - t), t);
                break;
        }

        ChromaticAberration ca;
        cvs.m_Profile.TryGet<ChromaticAberration>(out ca);
        if (ca != null)
        {
            ca.intensity.value = t * ca0;
        }

        LensDistortion ld;
        cvs.m_Profile.TryGet<LensDistortion>(out ld);
        if (ld != null)
        {
            ld.intensity.value = t * ld0;
        }

        Vignette vi;
        cvs.m_Profile.TryGet<Vignette>(out vi);
        if (vi != null)
        {
            vi.intensity.value = t * vi0;
        }

        FilmGrain fg;
        cvs.m_Profile.TryGet<FilmGrain>(out fg);
        if (fg != null)
        {
            fg.intensity.value = (1f - t) * fg0;
        }


        mat.SetFloat("_T", t);
    }

}
