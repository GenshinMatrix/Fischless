namespace Fischless.GamepadCapture.RawInput;

[Flags]
public enum Locate_DevNode_Flags
{
    CM_LOCATE_DEVNODE_NORMAL = 0,
    CM_LOCATE_DEVNODE_PHANTOM = 1,
    CM_LOCATE_DEVNODE_CANCELREMOVE = 2,
    CM_LOCATE_DEVNODE_NOVALIDATION = 4
}
