using System;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [Header("Tank Settings")]
    [SerializeField] int playerNumber = 1;
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] Transform muzzleTransform;

    [Header("Muzzle Settings")]
    [SerializeField] float minMuzzleRotation = 10;
    [SerializeField] float maxMuzzleRotation = 100;
    [SerializeField] GameObject projectilePrefab;

    [Header("Bullet Settings")]
    [SerializeField] Transform bulletsContainer;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float muzzleSpawnDistance = 0.7f; // множитель дистанции от начала координаты пушки




    private string _movementAxisName;
    private string _rotateAxisName;
    private string _fireAxisName;
    private Rigidbody2D _rb;

    private bool _isFirePressed = false;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        _rb.isKinematic = false;
    }


    private void OnDisable()
    {
        _rb.isKinematic = true;
    }


    private void Start()
    {
        _movementAxisName = "Horizontal" + playerNumber;
        _rotateAxisName = "Vertical" + playerNumber;
        _fireAxisName = "Fire" + playerNumber;
    }


    private void FixedUpdate()
    {
        Move();
        MuzzleTurn();

        if (Input.GetAxisRaw(_fireAxisName) == 1)
        {
            if (!_isFirePressed)
            {
                Fire();
            }
            _isFirePressed = true;
        }
        else
        {
            _isFirePressed = false;
        }
    }


    // Перемещение танка
    private void Move()
    {
        float moveInput = Input.GetAxis(_movementAxisName); // движение влево (-1), вправо (1), или стоим на месте (0)

        _rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, _rb.velocity.y);
    }


    // Поворот дула
    private void MuzzleTurn()
    {
        float rotationInput = Input.GetAxisRaw(_rotateAxisName); // поворот дула вверх (1), вниз (-1), или нет поворота (0)

        float muzzleAngle = muzzleTransform.localEulerAngles.z % 360;
        if (muzzleAngle > 180) muzzleAngle -= 360;

        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;

        float muzzleRotation = Mathf.Clamp(muzzleAngle + rotationAmount, minMuzzleRotation, maxMuzzleRotation);

        muzzleTransform.localRotation = Quaternion.Euler(0, 0, muzzleRotation);
    }

    private void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Префаб снаряда не установлен!");
            return;
        }

        // Позиция снаряда
        Vector2 projectilePosition = muzzleTransform.position + muzzleTransform.right * muzzleSpawnDistance;

        // Создаём новый объект снаряда на основе префаба
        GameObject projectileGameObject = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity, bulletsContainer);

        Rigidbody2D projectileRB = projectileGameObject.AddComponent<Rigidbody2D>(); // добавляем Rigidbody компонент (чтобы двигался)

        projectileRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        projectileRB.AddForce(muzzleTransform.right * bulletSpeed, ForceMode2D.Impulse);



        Debug.Log($"Fire\nPlayer{playerNumber}");
    }
}