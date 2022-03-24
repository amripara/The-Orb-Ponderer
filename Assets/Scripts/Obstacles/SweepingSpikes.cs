using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingSpikes : MonoBehaviour
{
    public GameObject baseObj, spike, upperFlap, lowerFlap;
    public GameObject leftPoint, rightPoint;
    public float spikeWaitTime = 0.2f;
    public float displacementVal = 0.6f;
    public bool oppositeOrientation = false;
    // for testing purposes
    public bool playOnAwake = false;

    private Transform player;
    private Vector3 baseStartPos, spikeStartPos, upperFlapStartPos, lowerFlapStartPos;
    private bool activated = false;
    private float oppositeOrientationVal;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseStartPos = baseObj.transform.position;
        spikeStartPos = spike.transform.position;
        upperFlapStartPos = upperFlap.transform.position;
        lowerFlapStartPos = lowerFlap.transform.position;
        if (oppositeOrientation) {
            oppositeOrientationVal = 180f;
        } else {
            oppositeOrientationVal = 0f;
        }
        if (playOnAwake) {
            Debug.Log("activating sweeping spikes");
            activated = true;
            StartCoroutine(SweepingSpikesObstacle());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated) {
            Debug.Log("activating sweeping spikes");
            activated = true;
            StartCoroutine(SweepingSpikesObstacle());
        }
    }

    private IEnumerator SweepingSpikesObstacle()
    {
        // short delay before playing upon entering play mode
        if (playOnAwake) {
            yield return new WaitForSeconds(0.5f);
        }
        
        // ensure the spike will sweep towards the player
        bool closerToRightPoint = false;
        
        float distanceToLeftPoint = Vector3.Distance(player.position, leftPoint.transform.position);
        float distanceToRightPoint = Vector3.Distance(player.position, rightPoint.transform.position);

        if (distanceToLeftPoint > distanceToRightPoint) {
            closerToRightPoint = true;
            spike.transform.eulerAngles = new Vector3(0, 80 + oppositeOrientationVal, 0);
        } else {
            closerToRightPoint = false;
            spike.transform.eulerAngles = new Vector3(0, -80 + oppositeOrientationVal, 0);
        }
        
        // move sweeping spike out from wall
        upperFlap.GetComponent<GlideController>().SetDestination(new Vector3(upperFlapStartPos.x, upperFlapStartPos.y + 0.55f, upperFlapStartPos.z));
        lowerFlap.GetComponent<GlideController>().SetDestination(new Vector3(lowerFlapStartPos.x, lowerFlapStartPos.y - 0.55f, lowerFlapStartPos.z));
        yield return new WaitForSeconds(0.2f);
        baseObj.GetComponent<GlideController>().SetDestination(new Vector3(baseStartPos.x + displacementVal, baseStartPos.y, baseStartPos.z));
        spike.GetComponent<GlideController>().SetDestination(new Vector3(spikeStartPos.x + displacementVal, spikeStartPos.y, spikeStartPos.z));
        
        // wait time until spike sweeps
        yield return new WaitForSeconds(spikeWaitTime);
        
        // rotate spike in sweeping motion
        Quaternion startAngle = spike.transform.rotation;
        Quaternion targetAngle = spike.transform.rotation;
        if (closerToRightPoint) {
            targetAngle = Quaternion.Euler(0, -80 + oppositeOrientationVal, 0); 
        } else {
            targetAngle = Quaternion.Euler(0, 80 + oppositeOrientationVal, 0); 
        }
        float startTime = Time.time; 
        while (Time.time - startTime < 1.0f) { 
            spike.transform.rotation = Quaternion.Lerp(startAngle, targetAngle, Time.time - startTime);
            yield return null; 
        } 
        spike.transform.rotation = targetAngle;

        // retract sweeping spike back into wall
        baseObj.GetComponent<GlideController>().SetDestination(new Vector3(baseStartPos.x, baseStartPos.y, baseStartPos.z));
        spike.GetComponent<GlideController>().SetDestination(new Vector3(spikeStartPos.x, spikeStartPos.y, spikeStartPos.z));
        yield return new WaitForSeconds(0.25f);
        upperFlap.GetComponent<GlideController>().SetDestination(new Vector3(upperFlapStartPos.x, upperFlapStartPos.y, upperFlapStartPos.z));
        lowerFlap.GetComponent<GlideController>().SetDestination(new Vector3(lowerFlapStartPos.x, lowerFlapStartPos.y, lowerFlapStartPos.z));

        activated = false;
    }
}
