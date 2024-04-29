using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] float minMuzzleRotation = 10f;      // минимальный поворот дула
    [SerializeField] float maxMuzzleRotation = 100f;     // максимальный поворот дула

    [Header("Bullet Properties")]
    [SerializeField] GameObject projectilePrefab;       // префаб снаряда
    [SerializeField] Transform bulletsContainer;        // контейнер, где будут храниться объекты снарядов (чтобы не засоряло основную ветку)
    [SerializeField] float minBulletSpeed = 3f;
    [SerializeField] float maxBulletSpeed = 15f;
    [SerializeField] float bulletSpeedMultiplier = 10f;
    [SerializeField] float muzzleSpawnDistance = 0.7f;  // множитель дистанции от начала координаты пушки
    [SerializeField] float recoilMultiplier = 10f;

    [Space]

    [SerializeField] ProjectileProperties projectileProperties;

    [Header("Misc")]
    [SerializeField] GameObject bulletSpeedCanvas;
    [SerializeField] Image bulletSpeedBarImage;
    [SerializeField] Gradient bulletSpeedBarGradient;
    [SerializeField] Image healthBarImage;

    private float health;

    private string _movementAxisName;
    private string _rotateAxisName;
    private string _fireAxisName;
    private Rigidbody2D _rb;

    private bool _isFirePressed = false;
    private float _bulletSpeed = 0f;

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

        Vector2 border = GetBorder();
        Vector2 playerTransform = new Vector2(border.x - border.x / 5, -0.75f);
        if (playerNumber == 1)
        {
            playerTransform.x *= -1;
        }

        transform.position = playerTransform;
    }

    private Vector2 GetBorder()
    {
        Vector2 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        return stageDimensions;
    }


    private void FixedUpdate()
    {
        #region Движение танка
        Move();
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

        return projectile;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarImage.fillAmount = health / maxHealth; // float написан чтобы вернуло значение float, а не int

        if (health <= 0)
        {
            GameManager.instance.EndGame(playerNumber);
        }
    }

    #endregion
}

