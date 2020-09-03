using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private ParticleSystem confetti;
    private void Awake()
    {
        confetti?.Stop();
        LevelManager.Instance.PrepareLevel(0);
    }
    public void GoToNextSector()
    {
        Debug.Log("GoToNextSector");
        Hole.Instance.DisableHole();
        HoleState.Instance.GoToPos(1,()=> {
            Hole.Instance.EnableHole();
            GateState.Instance.Open(() =>
            {
                HoleState.Instance.GoToPos(2);
                CameraState.Instance.GoToNext();
            });
        });

    }

    public void LevelCompleted()
    {
        Debug.Log("LevelCompleted");
        Hole.Instance.DisableHole();
        confetti?.Play();
        HoleState.Instance.GoToPos(2, () => {
            CameraState.Instance.GoToNext();
            HoleState.Instance.GoToPos(0,()=> { 
                LevelManager.Instance.NextLevel();
                GateState.Instance.Close(()=> { 
                    Hole.Instance.EnableHole();
                    confetti?.Stop();
                });
            });

        });

    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        Hole.Instance.DisableHole();
        CameraState.Instance.ShakeCamera(()=> {

            HoleState.Instance.GoToPos(-1, () =>
            {
                if (CameraState.Instance.currentState != 0)
                    CameraState.Instance.GoToPos(0);
                HoleState.Instance.GoToPos(0, () =>
                {
                    LevelManager.Instance.CurrentLevel();
                    GateState.Instance.Close(() =>
                    {
                        Hole.Instance.EnableHole();
                    });
                });
            });

        });







    }
}
