using UnityEngine;

/// <summary>
/// Добавляет вращение объекту
/// </summary>
public class Rotator : MonoBehaviour {
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;
    
    private void Update() {
        transform.Rotate(new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime);
    }
}
