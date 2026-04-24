using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworks;
    [SerializeField] private GameState state = GameState.Playing;
    public static GameManager Instance { get; private set; }
    public GameState State => state;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    

    public void OnWinGame(Player player)
    {
        SetOffFirework();
        state = GameState.Win;
        player.RemoveAllBrick();
        player.RotateToChess();
        player.ChangeAnim("win");
    }
    
    public void OnLoseGame()
    {
        
    }
    private void SetOffFirework()
    {
        foreach (var firework in fireworks)
        {
            firework.Play();
        }
    }
}
