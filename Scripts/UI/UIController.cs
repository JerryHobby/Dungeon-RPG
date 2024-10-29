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
        containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;

        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
        GameEvents.OnReward += HandleReward;
    }


    public override void _Input(InputEvent @event)
    {
        if (canPause && Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            HandlePausePressed();
        }
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = false;
        GameEvents.RaiseStartGame();

        containers[ContainerType.Stats].Show();
        containers[ContainerType.Start].Hide();
    }

    private void HandlePausePressed()
    {
        GetTree().Paused = !GetTree().Paused;

        containers[ContainerType.Stats].Visible = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleRewardPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Stats].Show();
        containers[ContainerType.Reward].Hide();
    }

    private void HandleEndGame()
    {
        canPause = false;
        GetTree().Paused = true;

        containers[ContainerType.Stats].Hide();
        containers[ContainerType.Defeat].Show();
    }

    private void HandleVictory()
    {
        canPause = false;
        GetTree().Paused = true;

        containers[ContainerType.Stats].Hide();
        containers[ContainerType.Victory].Show();
    }

    private void HandleReward(RewardResource reward)
    {
        canPause = false;
        GetTree().Paused = true;

        // Customize the reward container
        containers[ContainerType.Reward].TextureNode.Texture = reward.SpriteTexture;
        containers[ContainerType.Reward].LabelNode.Text = reward.Description;

        containers[ContainerType.Stats].Hide();
        containers[ContainerType.Reward].Show();
    }
}
