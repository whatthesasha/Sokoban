using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.VisualTree;
using Sokoban.Architecture;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        if (!GameFrame.IsVisible || GameState.IsOver)
            return;

        Game.KeyPressed = e.Key;
        GameFrame.Act();

        UpdateMovesCounter();
        if (GameState.IsOver) {
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
       var a = int.Parse((string)((Button)sender).Tag);

        StartGame(a - 1);
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