using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPiece : MonoBehaviour
{
    public GameObject Key;
    public GameObject[] KeyPieces;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Key.activeSelf)
        {
            foreach (GameObject keyPiece in KeyPieces)
            {
                keyPiece.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
