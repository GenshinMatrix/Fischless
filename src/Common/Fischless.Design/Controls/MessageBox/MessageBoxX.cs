using Fischless.Native;
using ModernWpf;
using ModernWpf.Controls;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using static Fischless.Design.Controls.LocalizedDialogCommands;

namespace Fischless.Design.Controls;

[TemplatePart(Name = nameof(OKButton), Type = typeof(Button))]
[TemplatePart(Name = nameof(YesButton), Type = typeof(Button))]
[TemplatePart(Name = nameof(NoButton), Type = typeof(Button))]
[TemplatePart(Name = nameof(CancelButton), Type = typeof(Button))]
[SuppressMessage("WpfAnalyzers.TemplatePart", "WPF0132:")]
public partial class MessageBoxX : Window
{
    public MessageBoxResult Result;

    private Button OKButton { get; set; }
    private Button YesButton { get; set; }
    private Button NoButton { get; set; }
    private Button CancelButton { get; set; }

    static MessageBoxX()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxX), new FrameworkPropertyMetadata(typeof(MessageBoxX)));
    }

    public MessageBoxX()
    {
        SetValue(TemplateSettingsPropertyKey, new MessageBoxTemplateSettings());
        var handler = new RoutedEventHandler((sender, e) =>
            WindowBackdrop.ApplyBackdrop(this, WindowBackdropType.Mica));
        ThemeManager.AddActualThemeChangedHandler(this, handler);
        Loaded += OnLoaded;
    }

    #region MessageBoxIcon

    public MessageBoxIcon MessageBoxIcon
    {
        get { return (MessageBoxIcon)GetValue(MessageBoxIconProperty); }
        set { SetValue(MessageBoxIconProperty, value); }
    }

    public static readonly DependencyProperty MessageBoxIconProperty = DependencyProperty.Register("MessageBoxIcon", typeof(MessageBoxIcon), typeof(MessageBoxX), new(MessageBoxIcon.Info));

    #endregion

    #region UseMica

    public static readonly DependencyProperty UseMicaProperty =
        DependencyProperty.Register(
            nameof(UseMica),
            typeof(bool),
            typeof(MessageBoxX),
            new PropertyMetadata(true, OnUseMicaPropertyChanged));

    public bool UseMica
    {
        get => (bool)GetValue(UseMicaProperty);
        set => SetValue(UseMicaProperty, value);
    }

    #endregion

    #region Caption

    public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register(
            nameof(Caption),
            typeof(object),
            typeof(MessageBoxX));

    public object Caption
    {
        get => GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }

    #endregion

    #region CaptionTemplate

    public static readonly DependencyProperty CaptionTemplateProperty =
        DependencyProperty.Register(
            nameof(CaptionTemplate),
            typeof(DataTemplate),
            typeof(MessageBoxX));

    public DataTemplate CaptionTemplate
    {
        get => (DataTemplate)GetValue(CaptionTemplateProperty);
        set => SetValue(CaptionTemplateProperty, value);
    }

    #endregion

    #region OKButtonText

    public static readonly DependencyProperty OKButtonTextProperty =
        DependencyProperty.Register(
            nameof(OKButtonText),
            typeof(string),
            typeof(MessageBoxX),
            new PropertyMetadata(string.Empty, OnButtonTextChanged));

    public string OKButtonText
    {
        get => (string)GetValue(OKButtonTextProperty);
        set => SetValue(OKButtonTextProperty, value);
    }

    #endregion

    #region OKButtonCommand

    public static readonly DependencyProperty OKButtonCommandProperty =
        DependencyProperty.Register(
            nameof(OKButtonCommand),
            typeof(ICommand),
            typeof(MessageBoxX),
            null);

    public ICommand OKButtonCommand
    {
        get => (ICommand)GetValue(OKButtonCommandProperty);
        set => SetValue(OKButtonCommandProperty, value);
    }

    #endregion

    #region OKButtonCommandParameter

    public static readonly DependencyProperty OKButtonCommandParameterProperty =
        DependencyProperty.Register(
            nameof(OKButtonCommandParameter),
            typeof(object),
            typeof(MessageBoxX),
            null);

    public object OKButtonCommandParameter
    {
        get => GetValue(OKButtonCommandParameterProperty);
        set => SetValue(OKButtonCommandParameterProperty, value);
    }

    #endregion

    #region OKButtonStyle

    public static readonly DependencyProperty OKButtonStyleProperty =
        DependencyProperty.Register(
            nameof(OKButtonStyle),
            typeof(Style),
            typeof(MessageBoxX),
            null);

    public Style OKButtonStyle
    {
        get => (Style)GetValue(OKButtonStyleProperty);
        set => SetValue(OKButtonStyleProperty, value);
    }

    #endregion

    #region YesButtonText

    public static readonly DependencyProperty YesButtonTextProperty =
        DependencyProperty.Register(
            nameof(YesButtonText),
            typeof(string),
            typeof(MessageBoxX),
            new PropertyMetadata(string.Empty, OnButtonTextChanged));

    public string YesButtonText
    {
        get => (string)GetValue(YesButtonTextProperty);
        set => SetValue(YesButtonTextProperty, value);
    }

    #endregion

    #region YesButtonCommand

    public static readonly DependencyProperty YesButtonCommandProperty =
        DependencyProperty.Register(
            nameof(YesButtonCommand),
            typeof(ICommand),
            typeof(MessageBoxX),
            null);

    public ICommand YesButtonCommand
    {
        get => (ICommand)GetValue(YesButtonCommandProperty);
        set => SetValue(YesButtonCommandProperty, value);
    }

    #endregion

    #region YesButtonCommandParameter

    public static readonly DependencyProperty YesButtonCommandParameterProperty =
        DependencyProperty.Register(
            nameof(YesButtonCommandParameter),
            typeof(object),
            typeof(MessageBoxX),
            null);

    public object YesButtonCommandParameter
    {
        get => GetValue(YesButtonCommandParameterProperty);
        set => SetValue(YesButtonCommandParameterProperty, value);
    }

    #endregion

    #region YesButtonStyle

    public static readonly DependencyProperty YesButtonStyleProperty =
        DependencyProperty.Register(
            nameof(YesButtonStyle),
            typeof(Style),
            typeof(MessageBoxX),
            null);

    public Style YesButtonStyle
    {
        get => (Style)GetValue(YesButtonStyleProperty);
        set => SetValue(YesButtonStyleProperty, value);
    }

    #endregion

    #region NoButtonText

    public static readonly DependencyProperty NoButtonTextProperty =
        DependencyProperty.Register(
            nameof(NoButtonText),
            typeof(string),
            typeof(MessageBoxX),
            new PropertyMetadata(string.Empty, OnButtonTextChanged));

    public string NoButtonText
    {
        get => (string)GetValue(NoButtonTextProperty);
        set => SetValue(NoButtonTextProperty, value);
    }

    #endregion

    #region NoButtonCommand

    public static readonly DependencyProperty NoButtonCommandProperty =
        DependencyProperty.Register(
            nameof(NoButtonCommand),
            typeof(ICommand),
            typeof(MessageBoxX),
            null);

    public ICommand NoButtonCommand
    {
        get => (ICommand)GetValue(NoButtonCommandProperty);
        set => SetValue(NoButtonCommandProperty, value);
    }

    #endregion

    #region NoButtonCommandParameter

    public static readonly DependencyProperty NoButtonCommandParameterProperty =
        DependencyProperty.Register(
            nameof(NoButtonCommandParameter),
            typeof(object),
            typeof(MessageBoxX),
            null);

    public object NoButtonCommandParameter
    {
        get => GetValue(NoButtonCommandParameterProperty);
        set => SetValue(NoButtonCommandParameterProperty, value);
    }

    #endregion

    #region NoButtonStyle

    public static readonly DependencyProperty NoButtonStyleProperty =
        DependencyProperty.Register(
            nameof(NoButtonStyle),
            typeof(Style),
            typeof(MessageBoxX),
            null);

    public Style NoButtonStyle
    {
        get => (Style)GetValue(NoButtonStyleProperty);
        set => SetValue(NoButtonStyleProperty, value);
    }

    #endregion

    #region CancelButtonText

    public static readonly DependencyProperty CancelButtonTextProperty =
        DependencyProperty.Register(
            nameof(CancelButtonText),
            typeof(string),
            typeof(MessageBoxX),
            new PropertyMetadata(string.Empty, OnButtonTextChanged));

    public string CancelButtonText
    {
        get => (string)GetValue(CancelButtonTextProperty);
        set => SetValue(CancelButtonTextProperty, value);
    }

    #endregion

    #region CancelButtonCommand

    public static readonly DependencyProperty CancelButtonCommandProperty =
        DependencyProperty.Register(
            nameof(CancelButtonCommand),
            typeof(ICommand),
            typeof(MessageBoxX),
            null);

    public ICommand CancelButtonCommand
    {
        get => (ICommand)GetValue(CancelButtonCommandProperty);
        set => SetValue(CancelButtonCommandProperty, value);
    }

    #endregion

    #region CancelButtonCommandParameter

    public static readonly DependencyProperty CancelButtonCommandParameterProperty =
        DependencyProperty.Register(
            nameof(CancelButtonCommandParameter),
            typeof(object),
            typeof(MessageBoxX),
            null);

    public object CancelButtonCommandParameter
    {
        get => GetValue(CancelButtonCommandParameterProperty);
        set => SetValue(CancelButtonCommandParameterProperty, value);
    }

    #endregion

    #region CancelButtonStyle

    public static readonly DependencyProperty CancelButtonStyleProperty =
        DependencyProperty.Register(
            nameof(CancelButtonStyle),
            typeof(Style),
            typeof(MessageBoxX),
            null);

    public Style CancelButtonStyle
    {
        get => (Style)GetValue(CancelButtonStyleProperty);
        set => SetValue(CancelButtonStyleProperty, value);
    }

    #endregion

    #region CornerRadius

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(MessageBoxX));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    #endregion

    #region IconSource

    public IconSource IconSource
    {
        get => (IconSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public static readonly DependencyProperty IconSourceProperty =
        DependencyProperty.Register(
            nameof(IconSource),
            typeof(IconSource),
            typeof(MessageBoxX),
            new PropertyMetadata(OnIconSourcePropertyChanged));

    private static void OnIconSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        ((MessageBoxX)sender).OnIconSourcePropertyChanged(args);
    }

    #endregion

    #region MessageBoxButtons

    public MessageBoxButton MessageBoxButtons
    {
        get => (MessageBoxButton)GetValue(MessageBoxButtonsProperty);
        set => SetValue(MessageBoxButtonsProperty, value);
    }

    public static readonly DependencyProperty MessageBoxButtonsProperty =
        DependencyProperty.Register(
            nameof(MessageBoxButtons),
            typeof(MessageBoxButton),
            typeof(MessageBoxX),
            new PropertyMetadata(OnMessageBoxButtonsPropertyChanged));

    private static void OnMessageBoxButtonsPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        ((MessageBoxX)sender).UpdateMessageBoxButtonState();
    }

    #endregion

    #region DefaultResult

    public MessageBoxResult DefaultResult
    {
        get => (MessageBoxResult)GetValue(DefaultResultProperty);
        set => SetValue(DefaultResultProperty, value);
    }

    public static readonly DependencyProperty DefaultResultProperty =
        DependencyProperty.Register(
            nameof(DefaultResult),
            typeof(MessageBoxResult),
            typeof(MessageBoxX));

    #endregion

    #region TemplateSettings

    private static readonly DependencyPropertyKey TemplateSettingsPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(TemplateSettings),
            typeof(MessageBoxTemplateSettings),
            typeof(MessageBoxX),
            null);

    public static readonly DependencyProperty TemplateSettingsProperty =
        TemplateSettingsPropertyKey.DependencyProperty;

    public MessageBoxTemplateSettings TemplateSettings
    {
        get => (MessageBoxTemplateSettings)GetValue(TemplateSettingsProperty);
    }

    #endregion

    public event TypedEventHandler<MessageBoxX, MessageBoxOpenedEventArgs> Opened;

    public new event TypedEventHandler<MessageBoxX, MessageBoxClosingEventArgs> Closing;

    public new event TypedEventHandler<MessageBoxX, MessageBoxClosedEventArgs> Closed;

    public event TypedEventHandler<MessageBoxX, MessageBoxButtonClickEventArgs> OKButtonClick;

    public event TypedEventHandler<MessageBoxX, MessageBoxButtonClickEventArgs> YesButtonClick;

    public event TypedEventHandler<MessageBoxX, MessageBoxButtonClickEventArgs> NoButtonClick;

    public event TypedEventHandler<MessageBoxX, MessageBoxButtonClickEventArgs> CancelButtonClick;

    private static void OnButtonTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((MessageBoxX)d).UpdateButtonTextState();
    }

    private static void OnUseMicaPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((MessageBoxX)d).UpdateMica();
    }

    void OnIconSourcePropertyChanged(DependencyPropertyChangedEventArgs args)
    {
        if (args.NewValue is IconSource iconSource)
        {
            TemplateSettings.IconElement = iconSource.MakeIconElementFrom();
        }
        else
        {
            TemplateSettings.ClearValue(MessageBoxTemplateSettings.IconElementProperty);
        }
        UpdateIconState();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (OKButton != null)
        {
            OKButton.Click -= OnButtonClick;
        }

        if (YesButton != null)
        {
            YesButton.Click -= OnButtonClick;
        }

        if (NoButton != null)
        {
            NoButton.Click -= OnButtonClick;
        }

        if (CancelButton != null)
        {
            CancelButton.Click -= OnButtonClick;
        }

        OKButton = GetTemplateChild(nameof(OKButton)) as Button;
        YesButton = GetTemplateChild(nameof(YesButton)) as Button;
        NoButton = GetTemplateChild(nameof(NoButton)) as Button;
        CancelButton = GetTemplateChild(nameof(CancelButton)) as Button;

        if (OKButton != null)
        {
            OKButton.Click += OnButtonClick;
        }

        if (YesButton != null)
        {
            YesButton.Click += OnButtonClick;
        }

        if (NoButton != null)
        {
            NoButton.Click += OnButtonClick;
        }

        if (CancelButton != null)
        {
            CancelButton.Click += OnButtonClick;
            CancelButton.IsCancel = true;
        }

        UpdateIconState();
        UpdateMessageState();
        UpdateButtonTextState();
        UpdateMessageBoxButtonState();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        InvalidateMeasure();
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender == OKButton)
        {
            HandleButtonClick(
                OKButtonClick,
                OKButtonCommand,
                OKButtonCommandParameter,
                MessageBoxResult.OK);
        }
        else if (sender == YesButton)
        {
            HandleButtonClick(
                YesButtonClick,
                YesButtonCommand,
                YesButtonCommandParameter,
                MessageBoxResult.Yes);
        }
        else if (sender == NoButton)
        {
            HandleButtonClick(
                NoButtonClick,
                NoButtonCommand,
                NoButtonCommandParameter,
                MessageBoxResult.No);
        }
        else if (sender == CancelButton)
        {
            HandleButtonClick(
                CancelButtonClick,
                CancelButtonCommand,
                CancelButtonCommandParameter,
                MessageBoxResult.Cancel);
        }
    }

    private void HandleButtonClick(
        TypedEventHandler<MessageBoxX, MessageBoxButtonClickEventArgs> handler,
        ICommand command,
        object commandParameter,
        MessageBoxResult result)
    {
        if (handler != null)
        {
            var args = new MessageBoxButtonClickEventArgs();

            var deferral = new MessageBoxButtonClickDeferral(() =>
            {
                if (!args.Cancel)
                {
                    TryExecuteCommand(command, commandParameter);
                    Close(result);
                }
            });

            args.SetDeferral(deferral);

            args.IncrementDeferralCount();
            handler(this, args);
            args.DecrementDeferralCount();
        }
        else
        {
            TryExecuteCommand(command, commandParameter);
            Close(result);
        }
    }

    private void UpdateButtonTextState()
    {
        var templateSettings = TemplateSettings;
        templateSettings.OKButtonText = string.IsNullOrEmpty(OKButtonText) ? GetString(DialogBoxCommand.IDOK) : OKButtonText;
        templateSettings.YesButtonText = string.IsNullOrEmpty(YesButtonText) ? GetString(DialogBoxCommand.IDYES) : YesButtonText;
        templateSettings.NoButtonText = string.IsNullOrEmpty(NoButtonText) ? GetString(DialogBoxCommand.IDNO) : NoButtonText;
        templateSettings.CancelButtonText = string.IsNullOrEmpty(CancelButtonText) ? GetString(DialogBoxCommand.IDCANCEL) : CancelButtonText;
    }

    private void UpdateMessageState()
    {
        string stateName = Caption == null || (Caption is string && string.IsNullOrEmpty((string)Caption)) ? TitleCollapsedState : TitleVisibleState;
        VisualStateManager.GoToState(this, stateName, true);
    }

    private void UpdateIconState()
    {
        string stateName = TemplateSettings.IconElement == null ? IconCollapsedState : IconVisibleState;
        VisualStateManager.GoToState(this, stateName, true);
    }

    private void UpdateMessageBoxButtonState()
    {
        string stateName;

        MessageBoxButton button = MessageBoxButtons;

        switch (button)
        {
            case MessageBoxButton.OK:
                stateName = OKVisibleState;
                OKButton?.Focus();
                break;
            case MessageBoxButton.OKCancel:
                stateName = OKCancelVisibleState;
                OKButton?.Focus();
                break;
            case MessageBoxButton.YesNoCancel:
                stateName = YesNoCancelVisibleState;
                YesButton?.Focus();
                break;
            case MessageBoxButton.YesNo:
                stateName = YesNoVisibleState;
                YesButton?.Focus();
                break;
            default:
                stateName = OKVisibleState;
                if (OKButton != null) { OKButton.Focus(); }
                break;
        }

        VisualStateManager.GoToState(this, stateName, true);

        stateName = button switch
        {
            MessageBoxButton.OK => OKAsDefaultButtonState,
            MessageBoxButton.OKCancel => OKAsDefaultButtonState,
            MessageBoxButton.YesNoCancel => YesAsDefaultButtonState,
            MessageBoxButton.YesNo => YesAsDefaultButtonState,
            _ => OKAsDefaultButtonState,
        };
        VisualStateManager.GoToState(this, stateName, true);
    }

    /// <summary>
    /// Opens a Message Box and returns only when the newly opened window is closed.
    /// </summary>
    /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
    public new MessageBoxResult ShowDialog()
    {
        base.ShowDialog();
        return Result;
    }

    public void Close(MessageBoxResult result)
    {
        var closing = Closing;
        if (closing != null)
        {
            var args = new MessageBoxClosingEventArgs(result);

            var deferral = new MessageBoxClosingDeferral(() =>
            {
                if (!args.Cancel)
                {
                    Result = result;
                    Close();
                    Closed?.Invoke(this, new MessageBoxClosedEventArgs(result));
                }
            });

            args.SetDeferral(deferral);

            args.IncrementDeferralCount();
            closing(this, args);
            args.DecrementDeferralCount();
        }
        else
        {
            Result = result;
            Close();
            Closed?.Invoke(this, new MessageBoxClosedEventArgs(result));
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateMica();
        WindowBackdrop.ApplyBackdrop(this, WindowBackdropType.Mica);
        NativeMethods.HideAllWindowButton(new WindowInteropHelper(this).Handle);
        Opened?.Invoke(this, new MessageBoxOpenedEventArgs());
    }

    private static void TryExecuteCommand(ICommand command, object parameter)
    {
        if (command != null && command.CanExecute(parameter))
        {
            command.Execute(parameter);
        }
    }

    private void UpdateMica()
    {
        if (UseMica)
        {
            WindowBackdrop.ApplyBackdrop(this, WindowBackdropType.Mica, Native.ApplicationTheme.Dark);
        }
        else
        {
            WindowDarkMode.RemoveBackground(this);
        }
    }

    private const string OKVisibleState = "OKVisible";
    private const string OKCancelVisibleState = "OKCancelVisible";
    private const string YesNoCancelVisibleState = "YesNoCancelVisible";
    private const string YesNoVisibleState = "YesNoVisible";

    private const string OKAsDefaultButtonState = "OKAsDefaultButton";
    private const string YesAsDefaultButtonState = "YesAsDefaultButton";

    private const string IconVisibleState = "IconVisible";
    private const string IconCollapsedState = "IconCollapsed";

    private const string TitleVisibleState = "TitleVisible";
    private const string TitleCollapsedState = "TitleCollapsed";
}

file static class UIExtensions
{
    public static IconElement MakeIconElementFrom(this IconSource iconSource)
    {
        if (iconSource is FontIconSource fontIconSource)
        {
            FontIcon fontIcon = new()
            {
                Glyph = fontIconSource.Glyph,
                FontSize = fontIconSource.FontSize,
            };
            var newForeground = fontIconSource.Foreground;
            if (newForeground != null)
            {
                fontIcon.Foreground = newForeground;
            }

            if (fontIconSource.FontFamily != null)
            {
                fontIcon.FontFamily = fontIconSource.FontFamily;
            }

            fontIcon.FontWeight = fontIconSource.FontWeight;
            fontIcon.FontStyle = fontIconSource.FontStyle;

            return fontIcon;
        }
        else if (iconSource is SymbolIconSource symbolIconSource)
        {
            SymbolIcon symbolIcon = new()
            {
                Symbol = symbolIconSource.Symbol,
            };
            var newForeground = symbolIconSource.Foreground;
            if (newForeground != null)
            {
                symbolIcon.Foreground = newForeground;
            }
            return symbolIcon;
        }
        else if (iconSource is BitmapIconSource bitmapIconSource)
        {
            BitmapIcon bitmapIcon = new();

            if (bitmapIconSource.UriSource != null)
            {
                bitmapIcon.UriSource = bitmapIconSource.UriSource;
            }

            bitmapIcon.ShowAsMonochrome = bitmapIconSource.ShowAsMonochrome;
            var newForeground = bitmapIconSource.Foreground;
            if (newForeground != null)
            {
                bitmapIcon.Foreground = newForeground;
            }
            return bitmapIcon;
        }
        else if (iconSource is PathIconSource pathIconSource)
        {
            PathIcon pathIcon = new();

            if (pathIconSource.Data != null)
            {
                pathIcon.Data = pathIconSource.Data;
            }
            var newForeground = pathIconSource.Foreground;
            if (newForeground != null)
            {
                pathIcon.Foreground = newForeground;
            }
            return pathIcon;
        }
        return null;
    }
}
