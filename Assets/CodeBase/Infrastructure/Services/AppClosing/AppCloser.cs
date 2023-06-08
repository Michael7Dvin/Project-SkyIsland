using UnityEditor;
using UnityEngine;

namespace Infrastructure.Services.AppClosing
{
    public class AppCloser : IAppCloser
    {
        public void Close()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}