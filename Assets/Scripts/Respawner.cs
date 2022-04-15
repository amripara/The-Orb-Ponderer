using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private GameObject crystalObject;
    private float Timer;
    private bool isCrystalGone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > 0 && isCrystalGone) 
        {
            Timer -= Time.deltaTime;
        } else if (Timer < 0)
        {
            isCrystalGone = false;
            crystalObject.SetActive(true);
        }
    }

    public void ResetTimer()
    {
        Timer = 2;
        isCrystalGone = true;
    }
}
