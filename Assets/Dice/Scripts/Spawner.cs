using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform point1, point2;

    public Vector3 GetSpawnPosition(){
        float x = Random.Range(point1.position.x, point2.position.x);
        float y = Random.Range(point1.position.y, point2.position.y);
        float z = Random.Range(point1.position.z, point2.position.z);

        return new Vector3(x,y,z);
    }
}
