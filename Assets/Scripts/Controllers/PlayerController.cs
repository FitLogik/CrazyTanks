using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Tank Properties")]
    public int playerNumber;
    [SerializeField] bool setPlayerPrefsColor = false;      // будет ли применяться цвет из реестра
    [SerializeField] Color tankColor;                       // цвет танка
    [SerializeField] float moveSpeed = 12f;                 // скорость движения танка
    [SerializeField] ContactFilter2D contactFilter;         // нужен для обнаружения столкновений с объектами
    [SerializeField] float maxSpeed = 12f;                  // максимальная скорость движения танка
    [SerializeField, Min(1f)] int maxHealth = 100;          // максимальное значение здоровья танка
    [SerializeField] float stabilizationForce = 0.1f;       // сила стабилизации кручения танка (чтобы он старался не переворачиваться)
    [SerializeField] float moveRotationMultiplier = 0.05f;  // множитель переворота при нажатии на кнопку движения (поднимается перед)

    [Header("Muzzle Properties")]
    [SerializeField] Transform muzzleTransform;             // объект дула
    [SerializeField] float muzzleRotationSpeed = 180f;      // скорость поворота дула
    [SerializeField] float minMuzzleRotation = 10f;         // минимальный поворот дула
    [SerializeField] float maxMuzzleRotation = 100f;        // максимальный поворот дула

    [Header("Bullet Properties")]
    [SerializeField] GameObject projectilePrefab;           // префаб снаряда
    [SerializeField] Transform bulletsContainer;            // контейнер, где будут храниться объекты снарядов (чтобы не засоряло основную ветку)
    [SerializeField] float minBulletSpeed = 3f;             // минимальная скорость снаряда
    [SerializeField] float maxBulletSpeed = 15f;            // максимальная скорость снаряда
    [SerializeField] float bulletSpeedMultiplier = 10f;     // множитель скорости снаряда при зажатии кнопки
    [SerializeField] float maxBulletDamage = 75f;           // максимальный урон от снаряда
    

    [SerializeField] float muzzleSpawnDistance = 0.7f;      // дистанция от начала координаты стрельбы
    [SerializeField] float recoilMultiplier = 10f;          // множитель отдачи от выстрела

    [Space]

    [SerializeField] ProjectileProperties projectileProperties;     // настройки снаряда

    [Header("Misc")]
    [SerializeField] GameObject bulletSpeedCanvas;          // Холст, который имеет элемент, показывающий ускорение снаряда перед выстрелом
    [SerializeField] Image bulletSpeedBarImage;             // Прогрессбар ускорения снаряда перед выстрелом
    [SerializeField] Gradient bulletSpeedBarGradient;       // Градиент, цвета которого устанавливаются от 0 до 100
    [SerializeField] Image healthBarImage;                  // Прогрессбар здоровья игрока

    float health;

    string _movementAxisName;
    string _rotateAxisName;
    string _fireAxisName;
    Rigidbody2D _rb;
    SkinManager _skinManager;





    bool isGrounded => _rb.IsTouching(contactFilter);


    bool _isFirePressed = false;

    private float _bulletSpeed = 0f;
    private float _bulletDamage = 0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _skinManager = GetComponent<SkinManager>();
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

        SetColor();

        SetPosition(playerNumber);
    }

    private void SetColor()
    {
        if (setPlayerPrefsColor == true)
        {
            tankColor = PrefsManager.GetPlayerColor(playerNumber);
        }

        _skinManager.SetColor(tankColor);
    }

    void SetPosition(int playerNumber)
    {
        Vector2 border = GameManager.GetBorders();
        Vector2 playerTransform = new Vector2(border.x - border.x / 5, -0.75f);
        if (playerNumber == 1)
        {
            playerTransform.x *= -1;
        }

        transform.position = playerTransform;
    }

    private void FixedUpdate()
    {
        #region Движение танка
        Move();
        StabilizeRotate();

        MuzzleTurn();
        #endregion
    }

    // Для более плавного изменения значений применяем Update вместо FixedUpdate
    private void Update()
    {
        #region Стрельба

        // Если нажата кнопка атаки
        if (Input.GetAxisRaw(_fireAxisName) == 1)
        {
            // Проверяем была ли нажата кнопка атаки только что
            if (!_isFirePressed)
            {
                _bulletSpeed = minBulletSpeed; // устанавливаем минимальную скорость пули
               
                // Включаем объект с холстом прогрессбара
                bulletSpeedCanvas.SetActive(true);

                _isFirePressed = true; // разрешаем огонь при отпускании кнопки атаки
            }

            // Просчитываем изменение увеличения скорости пули с момента прошлого кадра
            float speedIncrease = bulletSpeedMultiplier * Time.deltaTime;
            _bulletSpeed = Mathf.Min(_bulletSpeed + speedIncrease, maxBulletSpeed);  // ограничиваем скорость до максимального значения
            _bulletDamage = maxBulletDamage * _bulletSpeed / maxBulletSpeed;
            UpdateBulletSpeedBar(_bulletSpeed); // обновляем progressbar со скоростью пули
        }
        // Если отпущена кнопка атаки
        else
        {
            // Проверяем была ли отпущена кнопка атаки только что
            if (_isFirePressed)
            {
                Fire(); // производим выстрел

                // Выключаем объект с холстом прогрессбара
                bulletSpeedCanvas.SetActive(false);

                _isFirePressed = false; // запрещаем повторный выстрел после отпускания кнопки атаки

            }

        }

        #endregion

        // Чтобы находящийся внутри объекта progressbar не переворачивался вместе с ним
        bulletSpeedCanvas.transform.rotation = Quaternion.identity;
    }

    #region UI
    private void UpdateBulletSpeedBar(float speed)
    {
        float speedAmount = (speed - minBulletSpeed) / (maxBulletSpeed - minBulletSpeed);
        bulletSpeedBarImage.fillAmount = speedAmount;
        bulletSpeedBarImage.color = bulletSpeedBarGradient.Evaluate(speedAmount);
    }
    #endregion

    #region Движение танка
    // Перемещение танка
    private void Move()
    {
        // GetAxis - плавное изменение состояния кнопки
        float moveInput = Input.GetAxis(_movementAxisName); // движение влево (-1), вправо (1), или стоим на месте (0)

        // Проверка, находится ли танк на поверхности
        if (isGrounded)
        {
            // Добавляем силу в направлении танка для увеличения скорости
            _rb.AddForce(transform.right * moveInput * moveSpeed);
        }


        // Добавляем крутящий момент для поднятия переда при движении вперёд
        _rb.AddTorque(moveInput * moveRotationMultiplier);

        // Ограничение скорости
        float clampedVelocityX = Mathf.Clamp(_rb.velocity.x, -maxSpeed, maxSpeed);

        _rb.velocity = new Vector2(clampedVelocityX, _rb.velocity.y);
    }

    // Поворот дула
    private void MuzzleTurn()
    {
        float rotationInput = Input.GetAxisRaw(_rotateAxisName); // поворот дула вверх (1), вниз (-1), или нет поворота (0)

        float muzzleAngle = muzzleTransform.localEulerAngles.z % 360;
        if (muzzleAngle > 180) muzzleAngle -= 360;

        float rotationAmount = rotationInput * muzzleRotationSpeed * Time.deltaTime;
        float muzzleRotation = Mathf.Clamp(muzzleAngle + rotationAmount, minMuzzleRotation, maxMuzzleRotation);

        muzzleTransform.localRotation = Quaternion.Euler(0, 0, muzzleRotation);
    }

    // Стабилизация переворота танка, чтобы он не упал на голову
    private void StabilizeRotate()
    {
        // Получаем угол наклона танка
        float angle = Mathf.DeltaAngle(transform.eulerAngles.z, 0f);

        // Проверяем, наклонен ли танк
        if (angle != 0)
        {
            // Вычисляем силу угла наклона
            float stabilizationStrength = stabilizationForce * angle;

            // Применяем силу
            _rb.AddTorque(stabilizationStrength);
        }
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
        float bulletForce = _bulletSpeed + (_rb.velocity.x * transform.localScale.x) / 2;
        // Направление выстрела
        Vector2 bulletDirection = muzzleTransform.right * transform.localScale.x;

        // Выстрел снаряда
        projectile.Fire(bulletDirection, bulletForce);
        // Отдача от выстрела
        _rb.AddForce(-bulletDirection * recoilMultiplier * _bulletSpeed);


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
        projectile.properties.damage = (int)_bulletDamage;


        return projectile;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarImage.fillAmount = health / maxHealth; // float написан чтобы вернуло значение float, а не int

        if (health <= 0)
        {
            RoundManager.instance.EndRound(playerNumber);
        }
    }

    #endregion
}

