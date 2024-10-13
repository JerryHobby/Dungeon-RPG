using System.Numerics;

public class GameConstants
{
    // Movement Constants
    public const float DEFAULT_GRAVITY = 9.8f;
    public const float DEFAULT_SPEED = 5;


    // Input Constants
    public const string INPUT_MOVE_LEFT = "MoveLeft";
    public const string INPUT_MOVE_RIGHT = "MoveRight";
    public const string INPUT_MOVE_FORWARD = "MoveForward";
    public const string INPUT_MOVE_BACKWARD = "MoveBackward";


    // Animation Constants
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_MOVE = "Move";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_DASH = "Dash";
    public const string ANIM_DEATH = "Death";

    public const string ANIM_KICK = "Kick";
    public const string ANIM_RESET = "RESET";
}