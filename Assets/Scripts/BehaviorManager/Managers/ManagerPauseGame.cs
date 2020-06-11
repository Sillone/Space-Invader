using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerPauseGame", menuName = "Manager/Pause Game ")]
public class ManagerPauseGame : ManagerBase
{
    private bool isPause = false;
    public void SetPause(bool _isPause)
    {
        if (isPause == _isPause)
        {
            return;
        }
        else
        {
            isPause = _isPause;
            ToolBox.Get<ManagerEvent>().SendMessage(MessageType.Pause, new GamePause(isPause));
            ToolBox.Get<ManagerCorountine>().CoroutineComponent.StartCoroutine(FreezTime(isPause));
        }


    }
    IEnumerator FreezTime(bool isFreez)
    {

        if (isFreez)
        {
            Time.timeScale = 0;
        }
        else
            while (Time.timeScale != 1f)
            {
                Time.timeScale += (1 / 0.02f) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                yield return new WaitForSeconds(1f);
            }

        yield break;
    }
}
