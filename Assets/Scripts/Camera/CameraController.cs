using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;

    [Header("Camera Follow Properties")] 

    public float innerFollowX; // I have absolutely no clue what to call this and how to explain it lol

    private void Update() {
        float targetDiff = target.position.x - transform.position.x;
        Debug.Log(targetDiff);
        if (targetDiff < -innerFollowX || targetDiff > innerFollowX) {
            float newX = transform.position.x + (targetDiff * Time.deltaTime);
            transform.position = new Vector3(newX, 0, -10);
        }
    }
}