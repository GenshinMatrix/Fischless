using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Fischless.ModelViewer;

public partial class HelixViewer : UserControl
{
    public string ModelPath
    {
        get => (string)GetValue(ModelPathProperty);
        set => SetValue(ModelPathProperty, value);
    }
    public static readonly DependencyProperty ModelPathProperty = DependencyProperty.Register(nameof(ModelPath), typeof(string), typeof(HelixViewer), new(null!, OnModelPathChanged));

    public Model3DGroup Models
    {
        get => Model.Content as Model3DGroup;
        set => Model.Content = value;
    }

    public ModelLoader Loader = new();
    public Func<string[], Task<string>> Selector = null;

    public HelixViewer()
    {
        InitializeComponent();
    }

    private static void OnModelPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is HelixViewer self)
        {
            self.LoadModel((e.NewValue as string)!, self.Selector);
        }
    }

    public async void LoadModel(string path, Func<string[], Task<string>> selector = null!)
    {
        Model3DGroup model = await Loader.LoadModel(path, selector);

        Models?.Children.Clear();
        Models = model;
        ResetCamera();
    }

    public void CancelLoadModel()
    {
        ModelPath = string.Empty;
        Models = null;
        Loader?.Dispose();
    }

    public void ResetCamera()
    {
        Camera.FieldOfView = 45d;
        Camera.FarPlaneDistance = 30000d;
        Camera.LookDirection = new(0d, 0d, 49.867532564294045d);
        Camera.NearPlaneDistance = 0.1;
        Camera.Position = new(-2.384185791015625E-07d, 9.745553016662598d, -49.222887469422524d);
        Camera.UpDirection = new(0d, 1d, 0d);
    }
}
