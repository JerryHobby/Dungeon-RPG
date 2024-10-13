using System.Numerics;

public class GameConstants
{
    public const float DEFAULT_GRAVITY = 9.8f;
    public const float DEFAULT_SPEED = 5;
    public const float DEFAULT_JUMP_FORCE = 5;

    public const string MOVE_LEFT = "MoveLeft";
    public const string MOVE_RIGHT = "MoveRight";
    public const string MOVE_FORWARD = "MoveForward";
    public const string MOVE_BACKWARD = "MoveBackward";

    public const string ANIM_IDLE = "Idle";
    public const string ANIM_DIE = "Dying";
    public const string ANIM_WALK = "Walk";
    public const string ANIM_KICK = "Kicking";
    public const string ANIM_RESET = "RESET";
    public const string ANIM_SLASH = "Slashing";
    public const string ANIM_SLIDE = "Sliding";


    public static readonly Godot.Vector3 FACE_LEFT = new Godot.Vector3(-1, 1, 1);
    public static readonly Godot.Vector3 FACE_RIGHT = new Godot.Vector3(1, 1, 1);

}