using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingSpikes : MonoBehaviour
{
    public Transform player;
    public GameObject baseObj, spike, upperFlap, lowerFlap;
    public GameObject leftPoint, rightPoint;
    public float spikeWaitTime = 0.2f;

    private Vector3 baseStartPos, spikeStartPos, upperFlapStartPos, lowerFlapStartPos;
    private bool activated = false;
    
    void Start()
    {
        baseStartPos = baseObj.transform.position;
        spikeStartPos = spike.transform.position;
        upperFlapStartPos = upperFlap.transform.position;
        lowerFlapStartPos = lowerFlap.transform.position;
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
        // ensure the spike will sweep towards the player
        bool closerToRightPoint = false;
        
        float distanceToLeftPoint = Vector3.Distance(player.position, leftPoint.transform.position);
        float distanceToRightPoint = Vector3.Distance(player.position, rightPoint.transform.position);

        if (distanceToLeftPoint > distanceToRightPoint) {
            closerToRightPoint = true;
            spike.transform.eulerAngles = new Vector3(0, 80, 0);
        }
        
        // move sweeping spike out from wall
        upperFlap.GetComponent<GlideController>().SetDestination(new Vector3(upperFlapStartPos.x, upperFlapStartPos.y + 0.6f, upperFlapStartPos.z));
        lowerFlap.GetComponent<GlideController>().SetDestination(new Vector3(lowerFlapStartPos.x, lowerFlapStartPos.y - 0.6f, lowerFlapStartPos.z));
        baseObj.GetComponent<GlideController>().SetDestination(new Vector3(baseStartPos.x + 0.6f, baseStartPos.y, baseStartPos.z));
        spike.GetComponent<GlideController>().SetDestination(new Vector3(spikeStartPos.x + 0.6f, spikeStartPos.y, spikeStartPos.z));
        
        // wait time until spike sweeps
        yield return new WaitForSeconds(spikeWaitTime);
        
        // rotate spike in sweeping motion
        Quaternion startAngle = spike.transform.rotation;
        Quaternion targetAngle = spike.transform.rotation;
        if (closerToRightPoint) {
            targetAngle = Quaternion.Euler(0, -80, 0); 
        } else {
            targetAngle = Quaternion.Euler(0, 80, 0); 
        }
        float startTime = Time.time; 
        while (Time.time - startTime < 1.0f) { 
            spike.transform.rotation = Quaternion.Lerp(startAngle, targetAngle, Time.time - startTime);
            yield return null; 
        } 
        spike.transform.rotation = targetAngle; 

        // retract sweeping spike back into wall
        upperFlap.GetComponent<GlideController>().SetDestination(new Vector3(upperFlapStartPos.x, upperFlapStartPos.y, upperFlapStartPos.z));
        lowerFlap.GetComponent<GlideController>().SetDestination(new Vector3(lowerFlapStartPos.x, lowerFlapStartPos.y, lowerFlapStartPos.z));
        baseObj.GetComponent<GlideController>().SetDestination(new Vector3(baseStartPos.x, baseStartPos.y, baseStartPos.z));
        spike.GetComponent<GlideController>().SetDestination(new Vector3(spikeStartPos.x, spikeStartPos.y, spikeStartPos.z));

        activated = false;
    }
}
