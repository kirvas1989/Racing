using System;
using UnityEngine;

public class RaceCompletion : SingletonBase<RaceCompletion>
{
    const string filename = "completion.dat";
    public static string Filename => filename;

    [Serializable]
    private class RaceTime
    {
        public RaceInfo RaceInfo;
        public RaceInfo Next;
        public float PlayerTime;
        public bool IsLocked;
    }

    public static void SaveRaceResult(string sceneName, float playerRecordTime, bool isLocked)
    {
        Instance.SaveResult(sceneName, playerRecordTime, isLocked);
    }

    private void SaveResult(string sceneName, float playerRecordTime, bool isLocked)
    {
        if (completionData != null && completionData.Length > 0)
        {
            foreach (var item in completionData)
            {
                if (item.RaceInfo.SceneName == sceneName)
                {
                    if (playerRecordTime > item.PlayerTime)
                    {
                        item.PlayerTime = playerRecordTime;
                        item.IsLocked = isLocked;
                        Saver<RaceTime[]>.Save(filename, completionData);

                        foreach (var itemNext in completionData)
                        {
                            if (item.Next != null)
                            {
                                if (itemNext.RaceInfo.SceneName == item.Next.SceneName)
                                {
                                    itemNext.IsLocked = isLocked;
                                    Saver<RaceTime[]>.Save(filename, completionData);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    [SerializeField] private RaceTime[] completionData;

    private new void Awake()
    {
        base.Awake();
        
        Saver<RaceTime[]>.TryLoad(filename, ref completionData);
    }

    public bool TryIndex(int id, out RaceInfo raceInfo, out float playerRecordTime, out bool isLocked)
    {
        if (id >= 0 && id < completionData.Length)
        {
            raceInfo = completionData[id].RaceInfo;
            isLocked = completionData[id].IsLocked;
            playerRecordTime = completionData[id].PlayerTime;
            return true;
        }
        else
        {
            raceInfo = null;
            isLocked = false;
            playerRecordTime = 0;
            return false;
        }
    }
}
