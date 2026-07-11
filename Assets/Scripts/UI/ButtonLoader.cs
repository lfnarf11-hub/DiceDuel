using Given.Manager;
using UnityEngine;

public class ButtonLoader : MonoBehaviour
{
    public void LoadScene(int id)
    {
        LoadingManager.Instance.LoadLevelById((id));;
    }
}
