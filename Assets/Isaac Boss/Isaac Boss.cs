using UnityEngine;
using UnityEngine.UI;

public class IsaacBoss : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float healthDrain = 0.01f;

    [SerializeField]
    private Slider healthBar;

    private static readonly int SetHealth = Animator.StringToHash("health");

    private enum Phase
    {
        Phase1,
        Phase2,
        Phase3
    }

    private Phase _currentPhase = Phase.Phase1;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        healthBar.maxValue = maxHealth;
        healthBar.value    = healthBar.maxValue;
    }

    void Update()
    {
        healthBar.value -= healthDrain;

        _animator.SetFloat(SetHealth, healthBar.value / 100f);

        switch (_currentPhase)
        {
            case Phase.Phase1:
                Phase1();
                break;
            case Phase.Phase2:
                Phase2();
                break;
            case Phase.Phase3:
                Phase3();
                break;
        }
    }

    private void Phase1()
    {
        if (healthBar.value <= maxHealth * 0.7f)
        {
            _currentPhase = Phase.Phase2;
        }
    }

    private void Phase2()
    {
        if (healthBar.value <= maxHealth * 0.3f)
        {
            _currentPhase = Phase.Phase3;
        }
    }

    private void Phase3()
    {
    }
}
