using UnityEngine;
using UnityEngine.U2D;

public class EnvironmentGenerator : MonoBehaviour
{
    public static EnvironmentGenerator instance;

    [SerializeField] SpriteShapeController spriteShapeController;

    [SerializeField, Range(3, 100)] int levelLength = 50;
    [SerializeField, Range(1, 50)] float xMultiplier = 2;
    [SerializeField, Range(1, 50)] float yMultiplier = 2;
    [SerializeField, Range(0, 1)] float curveSmoothness = 0.5f;
    [SerializeField] bool randomGenerate = true;
    public float noiseStep = 0.5f;
    [SerializeField] float bottom = 10;

    private Vector3 _lastPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (randomGenerate)
        {
            noiseStep = Random.Range(0, 20f);
        }

        Generate();
    }

    private void Generate()
    {
        spriteShapeController.spline.Clear();

        for (int i = 0; i < levelLength; i++)
        {
            _lastPos = transform.position + new Vector3(xMultiplier * i, Mathf.PerlinNoise(0, i * noiseStep) * yMultiplier);
            spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != levelLength - 1)
            {
                spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteShapeController.spline.SetLeftTangent(i, Vector3.left * xMultiplier * curveSmoothness);
                spriteShapeController.spline.SetRightTangent(i, Vector3.right * xMultiplier * curveSmoothness);
            }
        }

        spriteShapeController.spline.InsertPointAt(levelLength, new Vector3(_lastPos.x, transform.position.y - bottom));

        spriteShapeController.spline.InsertPointAt(levelLength + 1, new Vector3(transform.position.x, transform.position.y - bottom));
    }
}
