using System.Threading.Tasks;
using System.Windows;

namespace Fischless.Design.Controls;

public partial class MessageBoxX
{
    public static MessageBoxResult Info(string messageBoxText) =>
        Show(messageBoxText, Mui("MessageBoxCaptionInfo"), MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);

    public static MessageBoxResult Info(string messageBoxText, string caption) =>
        Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);

    public static MessageBoxResult Info(string messageBoxText, string caption, MessageBoxButton button) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Information, MessageBoxResult.None);

    public static MessageBoxResult Info(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Information.ToSymbol(), defaultResult);

    public static MessageBoxResult Info(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Information, MessageBoxResult.None);

    public static MessageBoxResult Info(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Information.ToSymbol(), defaultResult);

    public static MessageBoxResult Warn(string messageBoxText) =>
        Show(messageBoxText, Mui("MessageBoxCaptionWarn"), MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);

    public static MessageBoxResult Warn(string messageBoxText, string caption) =>
        Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);

    public static MessageBoxResult Warn(string messageBoxText, string caption, MessageBoxButton button) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Warning, MessageBoxResult.None);

    public static MessageBoxResult Warn(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Warning.ToSymbol(), defaultResult);

    public static MessageBoxResult Warn(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Warning, MessageBoxResult.None);

    public static MessageBoxResult Warn(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Warning.ToSymbol(), defaultResult);

    public static MessageBoxResult Question(string messageBoxText) =>
        Show(messageBoxText, Mui("MessageBoxCaptionQuestion"), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None);

    public static MessageBoxResult Question(string messageBoxText, string caption) =>
        Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None);

    public static MessageBoxResult Question(string messageBoxText, string caption, MessageBoxButton button) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Question, MessageBoxResult.None);

    public static MessageBoxResult Question(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Question.ToSymbol(), defaultResult);

    public static MessageBoxResult Question(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Question, MessageBoxResult.None);

    public static MessageBoxResult Question(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Question.ToSymbol(), defaultResult);

    public static MessageBoxResult Error(string messageBoxText) =>
        Show(messageBoxText, Mui("MessageBoxCaptionError"), MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);

    public static MessageBoxResult Error(string messageBoxText, string caption) =>
        Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);

    public static MessageBoxResult Error(string messageBoxText, string caption, MessageBoxButton button) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Error, MessageBoxResult.None);

    public static MessageBoxResult Error(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(messageBoxText, caption, button, MessageBoxImage.Error.ToSymbol(), defaultResult);

    public static MessageBoxResult Error(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Error, MessageBoxResult.None);

    public static MessageBoxResult Error(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        Show(owner, messageBoxText, caption, button, MessageBoxImage.Error.ToSymbol(), defaultResult);

    public static Task<MessageBoxResult> InfoAsync(string messageBoxText) =>
        ShowAsync(messageBoxText, Mui("MessageBoxCaptionInfo"), MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);

    public static Task<MessageBoxResult> InfoAsync(string messageBoxText, string caption) =>
        ShowAsync(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);

    public static Task<MessageBoxResult> InfoAsync(string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(messageBoxText, caption, button, MessageBoxImage.Information, MessageBoxResult.None);

    public static Task<MessageBoxResult> InfoAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, MessageBoxImage.Information, defaultResult);

    public static Task<MessageBoxResult> InfoAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Information, MessageBoxResult.None);

    public static Task<MessageBoxResult> InfoAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Information.ToSymbol(), defaultResult);

    public static Task<MessageBoxResult> WarnAsync(string messageBoxText) =>
        ShowAsync(messageBoxText, Mui("MessageBoxCaptionWarn"), MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);

    public static Task<MessageBoxResult> WarnAsync(string messageBoxText, string caption) =>
        ShowAsync(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);

    public static Task<MessageBoxResult> WarnAsync(string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(messageBoxText, caption, button, MessageBoxImage.Warning, MessageBoxResult.None);

    public static Task<MessageBoxResult> WarnAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, MessageBoxImage.Warning, defaultResult);

    public static Task<MessageBoxResult> WarnAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Warning, MessageBoxResult.None);

    public static Task<MessageBoxResult> WarnAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Warning.ToSymbol(), defaultResult);

    public static Task<MessageBoxResult> QuestionAsync(string messageBoxText) =>
        ShowAsync(messageBoxText, Mui("MessageBoxCaptionQuestion"), MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.None);

    public static Task<MessageBoxResult> QuestionAsync(string messageBoxText, string caption) =>
        ShowAsync(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None);

    public static Task<MessageBoxResult> QuestionAsync(string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(messageBoxText, caption, button, MessageBoxImage.Question, MessageBoxResult.None);

    public static Task<MessageBoxResult> QuestionAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, MessageBoxImage.Question, defaultResult);

    public static Task<MessageBoxResult> QuestionAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Question, MessageBoxResult.None);

    public static Task<MessageBoxResult> QuestionAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Question.ToSymbol(), defaultResult);

    public static Task<MessageBoxResult> ErrorAsync(string messageBoxText) =>
        ShowAsync(messageBoxText, Mui("MessageBoxCaptionError"), MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);

    public static Task<MessageBoxResult> ErrorAsync(string messageBoxText, string caption) =>
        ShowAsync(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);

    public static Task<MessageBoxResult> ErrorAsync(string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(messageBoxText, caption, button, MessageBoxImage.Error, MessageBoxResult.None);

    public static Task<MessageBoxResult> ErrorAsync(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(GetActiveWindow(), messageBoxText, caption, button, MessageBoxImage.Error, defaultResult);

    public static Task<MessageBoxResult> ErrorAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Error, MessageBoxResult.None);

    public static Task<MessageBoxResult> ErrorAsync(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult) =>
        ShowAsync(owner, messageBoxText, caption, button, MessageBoxImage.Error.ToSymbol(), defaultResult);
}
