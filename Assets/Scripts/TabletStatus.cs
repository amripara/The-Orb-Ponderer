using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletStatus : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    private Image image;
    private bool topFilled = false;
    private bool rightFilled = false;
    private bool leftFilled = false;
    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }
    public void UpdateImage(GameObject rune)
    {
        if (rune.name == "fireTabletPickup")
        {
            image.sprite = images[0];
        }


        if (!topFilled && !rightFilled && !leftFilled)
        {
            if (rune.name == "fireRunePickup")
            {
                rightFilled = true;
                image.sprite = images[2];
            } else if (rune.name == "cycleRunePickup")
            {
                topFilled = true;
                image.sprite = images[3];
            } else if (rune.name == "hearthRunePickup")
            {
                leftFilled = true;
                image.sprite = images[1];
            }
        } 
        else if (topFilled)
        {
            if (rune.name == "hearthRunePickup")
            {
                leftFilled = true;
                if (rightFilled)
                {
                    image.sprite = images[7];
                } else
                {
                    image.sprite = images[4];
                }
            } else if (rune.name == "fireRunePickup")
            {
                rightFilled= true;
                if (leftFilled)
                {
                    image.sprite = images[7];
                } else
                {
                    image.sprite = images[5];
                }
            }
        }
        else if (rightFilled)
        {
            if (rune.name == "hearthRunePickup")
            {
                leftFilled = true;
                if (topFilled)
                {
                    image.sprite = images[7];
                } else
                {
                    image.sprite = images[6];
                }
            }
            else if (rune.name == "cycleRunePickup")
            {
                topFilled = true;
                if (leftFilled)
                {
                    image.sprite = images[7];
                }
                else
                {
                    image.sprite = images[5];
                }
            }
        } else if (leftFilled)
        {
            if (rune.name == "cycleRunePickup")
            {
                topFilled = true;
                if (rightFilled)
                {
                    image.sprite = images[7];
                }
                else
                {
                    image.sprite = images[4];
                }
            } else if (rune.name == "fireRunePickup")
            {
                rightFilled = true;
                if (topFilled)
                {
                    image.sprite = images[7];
                }
                else
                {
                    image.sprite = images[6];
                }
            }
        }
    }
}
