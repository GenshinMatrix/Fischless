using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Fischless.ModelViewer.MMD;

// VMDのフォーマットクラス
public class VMDFormat
{
    public string name;
    public string path;
    public string folder;

    public Header header;
    public MotionList motion_list;
    public SkinList skin_list;
    public LightList light_list;
    public CameraList camera_list;
    public SelfShadowList self_shadow_list;
}

public class Header
{
    public string vmd_header; // 30byte, "Vocaloid Motion Data 0002"
    public string vmd_model_name; // 20byte
}

public class MotionList
{
    public uint motion_count;
    public Dictionary<string, List<Motion>> motion;
}

public class Motion
{
    public string bone_name;    // 15byte
    public uint flame_no;
    public Point3D location;
    public Quaternion rotation;
    public byte[] interpolation;    // [4][4][4], 64byte

    // なんか不便になりそうな気がして
    public byte GetInterpolation(int i, int j, int k)
    {
        return this.interpolation[i * 16 + j * 4 + k];
    }

    public void SetInterpolation(byte val, int i, int j, int k)
    {
        this.interpolation[i * 16 + j * 4 + k] = val;
    }
}

/// <summary>
/// 表情リスト
/// </summary>
public class SkinList
{
    public uint skin_count;
    public Dictionary<string, List<SkinData>> skin;
}

public class SkinData
{
    public string skin_name;    // 15byte
    public uint flame_no;
    public float weight;
}

public class CameraList
{
    public uint camera_count;
    public CameraData[] camera;
}

public class CameraData
{
    public uint flame_no;
    public float length;
    public Point3D location;
    public Point3D rotation;    // オイラー角, X軸は符号が反転している
    public byte[] interpolation;    // [6][4], 24byte(未検証)
    public uint viewing_angle;
    public byte perspective;    // 0:on 1:off

    public byte GetInterpolation(int i, int j)
    {
        return this.interpolation[i * 6 + j];
    }

    public void SetInterpolation(byte val, int i, int j)
    {
        this.interpolation[i * 6 + j] = val;
    }
}

public class LightList
{
    public uint light_count;
    public LightData[] light;
}

public class LightData
{
    public uint flame_no;
    public Color rgb;   // αなし, 256
    public Point3D location;
}

public class SelfShadowList
{
    public uint self_shadow_count;
    public SelfShadowData[] self_shadow;
}

public class SelfShadowData
{
    public uint flame_no;
    public byte mode; //00-02
    public float distance;  // 0.1 - (dist * 0.00001)
}
