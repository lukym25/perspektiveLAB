using UnityEngine;
using UnityEngine.Assertions;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private PlayerMain playerMain;
    [SerializeField] private CameraMovement cameraMovement;
    
    [SerializeField] private InputEvents inputEvents;
    [SerializeField] private GameInfo gameInfo;

    private void Awake()
    {
        Assert.IsNotNull(inputEvents, "The inputEvents is null");
        Assert.IsNotNull(gameInfo, "The gameInfo is null");
    }

    private void Update()
    {
        if (gameInfo.gameState == GameStateEnum.InGame)
        {
            cameraMovement.MoveCameraInGame();
        }
        else
        {
            cameraMovement.MoveCameraInMenu();
        }
    }

    private void FixedUpdate()
    {
        if(gameInfo.gameState != GameStateEnum.InGame) {return;}
        
        playerMain.Attack();
        playerMain.Move();
    }

    private void Jump()
    {
        if(gameInfo.gameState != GameStateEnum.InGame) {return;}
        
        playerMain.Jump();
    }

    public void ChangeToGameMode()
    {
        playerMain.GameStarted();
    }
    
    private void OnEnable()
    {
        inputEvents.SpacePressed += Jump;
    }

    private void OnDisable()
    {
        inputEvents.SpacePressed -= Jump;
    }

    public void AddNewPlayer(PlayerMain newPlayerMain)
    {
        playerMain = newPlayerMain;
    }
}
