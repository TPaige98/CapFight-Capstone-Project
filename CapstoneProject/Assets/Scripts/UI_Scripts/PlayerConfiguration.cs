//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerConfiguration : MonoBehaviour
//{
//    private List<PlayerConfigs> playerConfigs;

//    [SerializeField] private int MaxPlayers = 2;

//    public static PlayerConfiguration Instance { get; private set; }

//    private void Awake()
//    {
//        if (Instance != null)
//        {
//            Debug.Log("SINGLETON = Trying to create another instance of singelton!!");
//        }
//        else
//        {
//            Instance = this;
//            DontDestroyOnLoad(Instance);
//            playerConfigs = new List<PlayerConfigs>();
//        }
//    }

//    public void SetPlayerColor(int index, Material color)
//    {

//    }
//}

//public class PlayerConfigs
//{
//    public PlayerConfigs(PlayerInput pi)
//    {
//        PlayerIndex = pi.playerIndex;
//        Input = pi;
//    }

//    public PlayerInput Input { get; set; }
//    public int PlayerIndex { get; set; }
//    public bool IsReady { get; set; }

//    public Material PlayerMaterial { get; set; }
//}
