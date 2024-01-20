using System;
using UnityEngine;

public class Clicer : MonoBehaviour
{
    public float ClickPointsPerSecond = 10f;
    public float SaveIntervalSeconds = 5f;

    private float gameDurationSeconds;
    private float clickTimer;
    private float saveTimer;
    private int points;

    private void Start()
    {
        LoadGameData();
    }

    private void Update()
    {
        gameDurationSeconds += Time.deltaTime;
        clickTimer += Time.deltaTime;
        saveTimer += Time.deltaTime;

        if (saveTimer >= SaveIntervalSeconds)
        {
            SaveGameData();
            saveTimer = 0f;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 30), "Время: " + FormatTime(gameDurationSeconds));
        GUI.Label(new Rect(10, 50, 200, 30), "Очки: " + points);

        if (GUI.Button(new Rect(10, 90, 100, 30), "Клик"))
        {
            points += Mathf.RoundToInt(clickTimer * ClickPointsPerSecond);
            clickTimer = 0f;
        }
    }

    private string FormatTime(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    private void SaveGameData()
    {
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetFloat("GameDuration", gameDurationSeconds);
        PlayerPrefs.Save();

        Debug.Log("Сохранилось");
    }

    private void LoadGameData()
    {
        points = PlayerPrefs.GetInt("Points");
        gameDurationSeconds = PlayerPrefs.GetFloat("GameDuration");

        Debug.Log("Старт игры");
    }
}