using UnityEngine;
using UnityEngine.UI;

public class IsaacBoss : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private static readonly int SetHealth = Animator.StringToHash("health");
    private Animator anim;
    private enum Phase
    {
        Phase1,
        Phase2,
        Phase3
    }
    private Phase currentPhase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value -= 0.001f;
        
        anim.SetFloat(SetHealth, healthBar.value);
        if (healthBar.value <= 0.7)
        {
            currentPhase = Phase.Phase2;
        }
        if (healthBar.value <= 0.3)
        {
            currentPhase = Phase.Phase3;
        }
        

        if (currentPhase == Phase.Phase2)
        {
            print("yeaowuw now im in second phase");
        }
        if (currentPhase == Phase.Phase3)
        {
            print("yeaowuw now im in third phase");
        }
    }
}
