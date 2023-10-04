using Vanara.PInvoke;

namespace Fischless.InputSimulator;

internal interface IInputMessageDispatcher
{
	public void DispatchInput(User32.INPUT[] inputs);
}
