using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    public static DebugLogger instance;

    //Serialized Variables
    [System.Serializable]
    struct DebugChannels
    {
        public DebugChannels(bool i, Color c, string p, string n = "")
        {
            isActive = i;
            debugColor = c;
            prefix = p;
            name = n;
        }

        public string name;
        public bool isActive;
        public Color debugColor;
        public string prefix;
    }
    static List<DebugChannels> channels = new List<DebugChannels>();
    [SerializeField] List<DebugChannels> ns_channels = new List<DebugChannels>();

    //Private

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        channels = ns_channels;
    }

    private void LateUpdate()
    {
        if (ns_channels != channels)
        {
            ns_channels = channels;
        }
    }

    public static void Log(string message, int channel)
    {
        if (channel > channels.Count)
        {
            for (int i = channels.Count - 1; i < channel; i++)
            {
                channels.Add(new DebugChannels(false, Color.white, ""));
            }
        }
        if (channels[channel - 1].isActive)
            Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
            (byte)(channels[channel - 1].debugColor.r * 255f), (byte)(channels[channel - 1].debugColor.g * 255f), (byte)(channels[channel - 1].debugColor.b * 255f)
            , channels[channel - 1].prefix + message));
    }
}
