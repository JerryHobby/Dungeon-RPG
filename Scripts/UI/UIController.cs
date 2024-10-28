using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren().Where(
            (element) => element is UIContainer)
            .Cast<UIContainer>().ToDictionary(
                (element) => element.container
                );

        foreach (var container in containers.Values)
        {
            container.Hide();
        }
        containers[ContainerType.Start].Show();

        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;

        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
    }

    public override void _Input(InputEvent @event)
    {
        if (canPause && Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            HandlePausePressed();
        }
    }

    private void HandlePausePressed()
    {
        GetTree().Paused = !GetTree().Paused;

        containers[ContainerType.Stats].Visible = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }


    private void HandleStartPressed()
    {
        canPause = true;
        containers[ContainerType.Start].Hide();
        containers[ContainerType.Stats].Show();

        GetTree().Paused = false;
        GameEvents.RaiseStartGame();
    }

    private void HandleEndGame()
    {
        canPause = false;
        containers[ContainerType.Stats].Hide();
        containers[ContainerType.Defeat].Show();

        GetTree().Paused = true;
    }

    private void HandleVictory()
    {
        canPause = false;
        containers[ContainerType.Stats].Hide();
        containers[ContainerType.Victory].Show();

        GetTree().Paused = true;
    }
}
