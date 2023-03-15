using System;
using System.Diagnostics;

namespace EntiCS.Utility
{
    public static class EntiCSLog
    {
        [Conditional("DEBUG")]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(Build(message));
        }

        [Conditional("DEBUG")]
        public static void Warn(object message)
        {
            UnityEngine.Debug.LogWarning(Build(message));
        }

        [Conditional("DEBUG")]
        public static void Error(object message)
        {
            UnityEngine.Debug.LogError(Build(message));
        }

        [Conditional("DEBUG")]
        public static void Error(object message, Exception e)
        {
            UnityEngine.Debug.LogError($"{Build(message)}\n{e.ToString()}");
        }

        private static string Build(object message)
        {
            return $"[EntiCS] {message.ToString()}";
        }
    }
}
