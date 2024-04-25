using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Tank Properties")]
    public int playerNumber = 1;              // номер игрока
    [SerializeField] float moveSpeed = 12f;             // скорость движения танка
    [SerializeField] float maxSpeed = 12f;              // максимальная скорость движения танка
    [SerializeField] int maxHealth = 100;               // максимальное значение здоровья танка

    [Header("Muzzle Properties")]
    [SerializeField] Transform muzzleTransform;         // объект дула
    [SerializeField] float rotationSpeed = 180f;        // скорость поворота дула
    [SerializeField] float minMuzzleRotation = 10;      // минимальный поворот дула
    [SerializeField] float maxMuzzleRotation = 100;     // максимальный поворот дула

    [Header("Bullet Properties")]
    [SerializeField] GameObject projectilePrefab;       // префаб снаряда
    [SerializeField] Transform bulletsContainer;        // контейнер, где будут храниться объекты снарядов (чтобы не засоряло основную ветку)
    [SerializeField] float bulletSpeed = 10;            // скорость снаряда
    [SerializeField] float muzzleSpawnDistance = 0.7f;  // множитель дистанции от начала координаты пушки
    [SerializeField] float recoilMultiplier = 10f;

    [Space]

    [SerializeField] ProjectileProperties projectileProperties;

    private int health;

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
        // Присваиваем названия осей для ввода (Horizontal1/2, Vertical1/2, Fire1/2), где 1/2 - один из номеров игрока
        _movementAxisName = "Horizontal" + playerNumber;
        _rotateAxisName = "Vertical" + playerNumber;
        _fireAxisName = "Fire" + playerNumber;

        health = maxHealth;
    }


    private void FixedUpdate()
    {
        #region Движение танка
        Move();
        MuzzleTurn();
        #endregion

        #region Стрельба
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
        #endregion
    }

    #region Движение танка
    // Перемещение танка
    private void Move()
    {
        float moveInput = Input.GetAxis(_movementAxisName); // движение влево (-1), вправо (1), или стоим на месте (0)

        // Добавляем силу для плавного увеличения скорости
        _rb.AddForce(new Vector2(moveInput * moveSpeed, 0));

        // Ограничение скорости
        //_rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
        float clampedVelocityX = Mathf.Clamp(_rb.velocity.x, -maxSpeed, maxSpeed);

        _rb.velocity = new Vector2(clampedVelocityX, _rb.velocity.y);
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

    #endregion

    #region Боевка
    private void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Префаб снаряда не установлен!");
            return;
        }


        Projectile projectile = CreateProjectile();

        // Сила выстрела (также скорость увеличивается или уменьшается при движении по координате x)
        float bulletForce = bulletSpeed + (_rb.velocity.x * transform.localScale.x) / 2;
        // Направление выстрела
        Vector2 bulletDirection = muzzleTransform.right * transform.localScale.x;

        // Выстрел снаряда
        projectile.Fire(bulletDirection, bulletForce);
        // Отдача от выстрела
        _rb.AddForce(-bulletDirection * recoilMultiplier * 10);


        Debug.Log($"Fire\nPlayer{playerNumber}");
    }

    private Projectile CreateProjectile()
    {
        // Позиция снаряда
        Vector2 projectilePosition = muzzleTransform.position + muzzleTransform.right // если танк отзеркален,
                                     * transform.localScale.x * muzzleSpawnDistance;  // transform.localScale.x имеет отрицательное значение


        // Создаём новый объект снаряда на основе префаба
        GameObject projectileGameObject = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity, bulletsContainer);

        Projectile projectile = projectileGameObject.AddComponent<Projectile>();
        projectile.properties = projectileProperties;

        return projectile;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        //bar.fillAmount = health / 100;
    }

    #endregion
}

