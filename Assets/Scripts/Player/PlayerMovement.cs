using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private PlayerData m_data;
     private float m_friction;
    private float m_maxSpeed;
    private float m_stepAcceleration;
    private float m_currentSpeed;
    private Rigidbody2D m_rigidbody;
    private InputActions m_inputs;
    private Vector2 m_direction;
    private Vector2 m_velocity;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        if (m_inputs == null)
        {
            m_inputs = new InputActions();
        }
        if (m_data != null)
        {
            m_maxSpeed = m_data.SpeedData.Speed;
            m_stepAcceleration = m_data.SpeedData.Acceleration;
            m_friction = m_data.SpeedData.Friction;
        }
    }
    void Start()
    {
        m_currentSpeed = 0;
    }
    void OnEnable()
    {
        m_inputs.Player.Enable();
        m_inputs.Player.Move.performed += ctx => m_direction = ctx.ReadValue<Vector2>();
        m_inputs.Player.Move.canceled += ctx => m_direction = Vector2.zero;
    }
    void OnDisable()
    {
        m_inputs.Player.Disable();
        m_inputs.Player.Move.performed -= ctx => m_direction = ctx.ReadValue<Vector2>();
        m_inputs.Player.Move.canceled -= ctx => m_direction = Vector2.zero;
    }
    void Update()
    {
        if (m_direction != Vector2.zero)
        {
            m_currentSpeed = Acceleration(m_currentSpeed, m_maxSpeed, m_stepAcceleration);
            m_velocity = SetVelocity(m_direction, m_currentSpeed);
        }
        else
        {
            m_velocity = Friction(m_velocity, Vector2.zero, m_friction);
        }
    }
    void FixedUpdate()
    {
        MoveObject();
    }
    private void MoveObject()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + m_velocity * Time.fixedDeltaTime);
    }
    private Vector2 SetVelocity(Vector2 direction, float speed)
    {
        return direction * speed;
    }
    private float Acceleration(float currentValue, float maxValue, float step)
    {
        return Mathf.MoveTowards(currentValue, maxValue, step * Time.deltaTime);
    }
    private Vector2 Friction(Vector2 currentValue, Vector2 toValue, float step)
    {
        return Vector2.MoveTowards(currentValue, toValue, step * Time.deltaTime);
    }
}
