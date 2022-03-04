using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingedDoor : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private bool isEntryDoor = false;
    private float timer = 0f;
    [SerializeField] private Collider doorCol;
    public GameObject winController;

    public bool IsEntryDoor { get => isEntryDoor;}

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                CloseDoor();
            }
        }
    }

    public void OpenDoor() {
        timer = 1f;
        animator.SetBool("Open", true);
        if (!isEntryDoor)
        {
            Transform tablet = transform.Find("LDoorFrame").Find("fireTabletEmpty");
            tablet.gameObject.SetActive(true);
            doorCol.enabled = false;
            winController.SetActive(true);
        }
    }

    public void CloseDoor() {
        animator.SetBool("Open", false);
    }

    private void OnTriggerEnter(Collider other) {
         timer = 1f;
         if (other.tag == "Player" && isEntryDoor) {
             OpenDoor();
            Debug.Log('l');
         }
     }

}
