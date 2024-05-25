using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class TankBehaviour : MonoBehaviour
{
    [Header("Tank Properties")]
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] public float moveSpeed = 12f;                      // скорость движения танка
    [SerializeField] protected float maxSpeed = 12f;                    // максимальная скорость движения танка
    [SerializeField, Min(1f)] protected int maxHealth = 100;            // максимальное значение здоровья танка
    [SerializeField] ContactFilter2D contactFilter;                     // нужен для обнаружения столкновений с объектами
    [SerializeField] float stabilizationForce = 0.1f;                   // сила стабилизации кручения танка (чтобы он старался не переворачиваться)
    [SerializeField] float moveRotationMultiplier = 0.05f;              // множитель переворота при нажатии на кнопку движения (поднимается перед)
    [SerializeField] GroundChecker groundChecker;                       // для обнаружения столкновения с землей



    [Header("Muzzle Properties")]
    [SerializeField] protected Transform muzzleTransform;               // объект дула
    [SerializeField] protected float muzzleRotationSpeed = 180f;        // скорость поворота дула
    [SerializeField] protected float minMuzzleRotation = 10f;           // минимальный поворот дула
    [SerializeField] protected float maxMuzzleRotation = 100f;          // максимальный поворот дула
    [SerializeField] float muzzleSpawnDistance = 0.7f;                  // дистанция от начала координаты стрельбы


    [Header("Bullet Properties")]
    [SerializeField] protected GameObject projectilePrefab;             // префаб снаряда
    [SerializeField] protected Transform bulletsContainer;              // контейнер, где будут храниться объекты снарядов (чтобы не засоряло основную ветку)
    [SerializeField] protected float minBulletSpeed = 3f;               // минимальная скорость снаряда
    [SerializeField] protected float maxBulletSpeed = 15f;              // максимальная скорость снаряда
    [SerializeField] protected float bulletSpeedMultiplier = 10f;       // множитель скорости снаряда при зажатии кнопки
    [SerializeField] protected float maxBulletDamage = 75f;             // максимальный урон от снаряда
    [SerializeField] protected float recoilMultiplier;                  // множитель отдачи от выстрела
    [SerializeField] ProjectileProperties projectileProperties;         // настройки снаряда



    protected float health;
    protected bool _hasShield = false;
    protected float _bulletSpeed = 0f;
    protected float _bulletDamage = 0f;

    protected float moveInput;
    protected float rotationInput;
    protected float fireInput;


    public bool IsFireReady => fireInput == 1;

    protected bool _isFirePressed = false;



    bool IsGrounded => _rigidbody.IsTouching(contactFilter) && groundChecker.IsGrounded;



    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        SetHealth(maxHealth);
    }

    protected virtual void FixedUpdate()
    {
        #region Движение танка
        Move();
        StabilizeTankRotation();

        MuzzleTurn();
        #endregion
    }

    protected virtual void Update()
    {
        // Если нажата кнопка атаки
        if (IsFireReady)
        {
            // Проверяем была ли нажата кнопка атаки только что
            if (!_isFirePressed)
            {
                _bulletSpeed = minBulletSpeed; // устанавливаем минимальную скорость пули

                _isFirePressed = true; // разрешаем огонь при отпускании кнопки атаки
            }

            // Просчитываем изменение увеличения скорости пули с момента прошлого кадра
            float speedIncrease = bulletSpeedMultiplier * Time.deltaTime;
            _bulletSpeed = Mathf.Min(_bulletSpeed + speedIncrease, maxBulletSpeed);  // ограничиваем скорость до максимального значения
            _bulletDamage = maxBulletDamage * _bulletSpeed / maxBulletSpeed;
        }
        // Если отпущена кнопка атаки
        else
        {
            // Проверяем была ли отпущена кнопка атаки только что
            if (_isFirePressed)
            {
                Fire(); // производим выстрел

                _isFirePressed = false; // запрещаем повторный выстрел после отпускания кнопки атаки
            }

        }
    }

    private void SetHealth(int value)
    {
        health = value;
    }

    internal virtual void IncreaseHealth(int value)
    {
        health = Mathf.Min(maxHealth, health + value);
    }

    internal virtual float GetHealth()
    {
        return health;
    }

    internal virtual void TakeDamage(int value)
    {
        if (!_hasShield)
        {
            health -= value;
            
            if (health <= 0)
            {
                _rigidbody.isKinematic = false;
                _rigidbody.freezeRotation = false;

                enabled = false;
            }
        }
        else
        {
            _hasShield = false;
        }
    }


    internal virtual void Freeze(float freezeDuration)
    {
        StartCoroutine(FreezeCoroutine(freezeDuration));
    }

    private IEnumerator FreezeCoroutine(float freezeTime)
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.freezeRotation = true;

        enabled = false;


        _isFirePressed = false;
        fireInput = 0;

        yield return new WaitForSeconds(freezeTime);

        if (health > 0)
        {
            _rigidbody.isKinematic = false;
            enabled = true;
            _rigidbody.freezeRotation = false;
        }
    }

    protected virtual void Move()
    {
        if (IsGrounded)
        {
            // Добавляем силу в направлении танка для увеличения скорости
            _rigidbody.AddForce(transform.right * moveInput * moveSpeed);
        }

        // Ограничение скорости
        float clampedVelocityX = Mathf.Clamp(_rigidbody.velocity.x, -maxSpeed, maxSpeed);

        _rigidbody.velocity = new Vector2(clampedVelocityX, _rigidbody.velocity.y);

        // Добавляем крутящий момент для поднятия переда при движении вперёд
        _rigidbody.AddTorque(moveInput * moveRotationMultiplier);
    }


    // Стабилизация переворота танка, чтобы он не упал на голову
    private void StabilizeTankRotation()
    {
        // Получаем угол наклона танка
        float angle = Mathf.DeltaAngle(transform.eulerAngles.z, 0f);

        // Проверяем, наклонен ли танк
        if (angle != 0)
        {
            // Вычисляем силу угла наклона
            float stabilizationStrength = stabilizationForce * angle;

            // Применяем силу
            _rigidbody.AddTorque(stabilizationStrength);
        }
    }


    // Поворот дула
    protected virtual void MuzzleTurn()
    {
        float rotationAmount = rotationInput * muzzleRotationSpeed * Time.deltaTime;
        RotateMuzzle(rotationAmount);
    }

    protected void RotateMuzzle(float value)
    {
        float muzzleAngle = muzzleTransform.localEulerAngles.z % 360;
        if (muzzleAngle > 180) muzzleAngle -= 360;

        float muzzleRotation = Mathf.Clamp(muzzleAngle + value, minMuzzleRotation, maxMuzzleRotation);

        muzzleTransform.localRotation = Quaternion.Euler(0, 0, muzzleRotation);
    }

    protected virtual void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Префаб снаряда не установлен!");
            return;
        }

        Projectile projectile = CreateProjectile();

        // Сила выстрела (также скорость увеличивается или уменьшается при движении по координате x)
        float bulletForce = _bulletSpeed + (_rigidbody.velocity.x * transform.localScale.x) / 2;
        // Направление выстрела
        Vector2 bulletDirection = muzzleTransform.right * transform.localScale.x;

        // Выстрел снаряда
        projectile.Fire(bulletDirection, bulletForce);
        // Отдача от выстрела
        _rigidbody.AddForce(-bulletDirection * recoilMultiplier * _bulletSpeed);
    }
    protected virtual Projectile CreateProjectile()
    {
        // Позиция снаряда
        Vector2 projectilePosition = muzzleTransform.position + muzzleTransform.right // если танк отзеркален,
                                     * transform.localScale.x * muzzleSpawnDistance;  // transform.localScale.x имеет отрицательное значение


        // Создаём новый объект снаряда на основе префаба
        GameObject projectileGameObject = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity, bulletsContainer);

        Projectile projectile = projectileGameObject.AddComponent<Projectile>();
        projectile.properties = projectileProperties;
        projectile.properties.damage = (int)_bulletDamage;

        return projectile;
    }

    public virtual void ActivateShield()
    {
        _hasShield = true;
    }
}