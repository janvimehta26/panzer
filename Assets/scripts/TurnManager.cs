using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [Header("Player Turret Scripts")]
    [SerializeField] private TurretController player1Turret;
    [SerializeField] private TurretController player2Turret;

    private bool isPlayer1Turn = true;
    private bool isTurnResolving = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        isTurnResolving = false;

        // Enable script for active player, disable for inactive player
        if (player1Turret != null) player1Turret.enabled = isPlayer1Turn;
        if (player2Turret != null) player2Turret.enabled = !isPlayer1Turn;

        Debug.Log($"[TurnManager] Turn Started: {(isPlayer1Turn ? "Player 1 (Purple)" : "Player 2 (Yellow)")}");
    }

    public void OnFire()
    {
        isTurnResolving = true;

        // Lock both turrets while shell is airborne
        if (player1Turret != null) player1Turret.enabled = false;
        if (player2Turret != null) player2Turret.enabled = false;
    }

    public void EndTurn()
    {
        if (!isTurnResolving) return;

        // Swap turn flag
        isPlayer1Turn = !isPlayer1Turn;

        // 1-second delay for smooth transition
        Invoke(nameof(StartTurn), 1.0f);
    }

    public bool IsTurnResolving() => isTurnResolving;
}