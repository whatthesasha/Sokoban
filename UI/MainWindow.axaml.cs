using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Sokoban.UI;

public partial class MainWindow : Window
{
    private static readonly string[] Levels = Directory
         .GetFiles(@$"{Environment.CurrentDirectory}\\Levels\\", "*.txt")
         .Select(filePath => File.ReadAllText(filePath)).ToArray();


    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (!GameFrame.IsVisible || Game.IsOver)
            return;

        Game.KeyPressed = e.Key;
        GameFrame.Act();

        UpdateMovesCounter();
        if (Game.IsOver) {
            if (CurrentLevelIndex + 1 == 11)
            {
                GameClearNotification.IsVisible = true;
                return;
            }
                LevelClearNotification.IsVisible = true;
        }
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
       var levelIndex = int.Parse((string)((Button)sender).Tag);
        StartGame(levelIndex);
    }

    private void Start_Button(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainMenu.IsVisible = false;
        LevelMenu.IsVisible = true;
    }

    private int CurrentLevelIndex;
    private void StartGame(int levelIndex)
    {
        GamePanel.IsVisible = true;
        LevelMenu.IsVisible = false;

        Game.KeyPressed = default;
        Game.Moves = 0;
        Game.CreateMap(Levels[levelIndex]);
        GameFrame.Initialize();

        CurrentLevelIndex = levelIndex;
        CurrentLevel.Text = $"Уровень {levelIndex + 1}";
        UpdateMovesCounter();
    }

    private void UpdateMovesCounter()
    {
        MovesCounter.Text = $"Шаги: {Game.Moves}";
    }

    private void RestartLevel (object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        StartGame(CurrentLevelIndex);
    }
    
    private void BackToTheMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        GamePanel.IsVisible = false;
        MainMenu.IsVisible = false;
        LevelMenu.IsVisible = true;
    }

    private void BackToTheMainMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainMenu.IsVisible = true;
        LevelMenu.IsVisible = false;
        GamePanel.IsVisible = false;
        GameClearNotification.IsVisible = false;
    }

    private void NextLevel(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        LevelClearNotification.IsVisible = false;
        StartGame(CurrentLevelIndex + 1);
    }
}