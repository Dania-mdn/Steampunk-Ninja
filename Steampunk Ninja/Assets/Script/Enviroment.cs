using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public GameObject Cube;
    public GameObject NextCube;
    public GameObject[] PrefabCube;

    private Vector3 SpawnPosition;

    private void OnEnable()
    {
        EventManager.Stop += GenerationNewCube;
    }
    private void OnDisable()
    {
        EventManager.Stop -= GenerationNewCube;
    }
    private void GenerationNewCube()
    {
        Cube = NextCube;
        SpawnPosition = new Vector3(Cube.transform.position.x + Random.Range(3f, 4.5f), Cube.transform.position.y + Random.Range(-2f, 2f), transform.position.z);
        NextCube = Instantiate(PrefabCube[Random.Range(0, PrefabCube.Length)], SpawnPosition, Quaternion.identity, transform);
    }
}
