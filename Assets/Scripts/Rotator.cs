using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void Update()
    {
        Vector3 rotationPerSecond = new Vector3(15f, 30f, 45f);
        transform.Rotate(rotationPerSecond * Time.deltaTime);
    }
}