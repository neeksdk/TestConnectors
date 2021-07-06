using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    [SerializeField] private Transform planeArea;
    [SerializeField] private GameObject connetablePrefab;
    public static float Radius = 10;
    private ConnectableContainer[] _connectables;

    private void Start() {
        SetVisualAreaOfPrefabGeneration();
        SetUpCameraPosition();
        GenerateCollectablesOnScene();

        TestMenu.OnUIPressed += OnUIPressed;
    }

    private void OnDestroy() {
        TestMenu.OnUIPressed -= OnUIPressed;
    }

    private void GenerateCollectablesOnScene() {
        _connectables = new ConnectableContainer[Constants.GENERATED_PREFABS_COUNT];
        
        for (int i = 0; i < Constants.GENERATED_PREFABS_COUNT; i++) {
            GameObject ConnectableGameObject = Instantiate(connetablePrefab,
                planeArea.position, planeArea.rotation, transform);
            ConnectableGameObject.name = $"ConnectablePrefab_{i:00}";
            _connectables[i] = ConnectableGameObject.GetComponent<ConnectableContainer>();
        }
        
        RandomizeCollectablePositions();
    }

    private void RandomizeCollectablePositions() {
        Vector3 PlaneAreaNormal = planeArea.TransformDirection(-Vector3.up);
        foreach (ConnectableContainer ConnectableInterface in _connectables) {
            ConnectableInterface.SetPosition(RandomPointOnPlane(planeArea.position, PlaneAreaNormal, Radius));
        }
    }

    private void SetVisualAreaOfPrefabGeneration() {
        float PlaneAreaScale = 0.2f + 0.2f * Radius;
        planeArea.transform.localScale = new Vector3(PlaneAreaScale, 1, PlaneAreaScale);
    }

    private void SetUpCameraPosition() {
        Camera MainCamera = Camera.main;

        if (MainCamera is null) return;
        
        MainCamera.transform.rotation = planeArea.transform.rotation;
        MainCamera.transform.position = planeArea.transform.position;
        
        MainCamera.transform.Translate(new Vector3(0, Radius * 2, -Radius));
        MainCamera.transform.LookAt(planeArea);
    }
    
    private void OnUIPressed(TestMenu.UICommandType commandType) {
        if (commandType == TestMenu.UICommandType.Randomize) RandomizeCollectablePositions();
    }
    
    private static Vector3 RandomPointOnPlane(Vector3 position, Vector3 normal, float radius)
    {
        Vector3 RandomPoint = Vector3.Cross(Random.insideUnitSphere, normal);
        RandomPoint.Normalize();
        RandomPoint *= Random.Range(0f, radius);
        RandomPoint += position;
       
        return RandomPoint;
    }
}
