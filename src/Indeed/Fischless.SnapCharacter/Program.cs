using Fischless.SnapCharacter;
using System.IO;
using Windows.System;

Thread t = new(new ThreadStart(SnapCharacterBenchmark.Action));

t.SetApartmentState(ApartmentState.STA);
t.Start();
t.Join();

while (t.ThreadState == ThreadState.Running)
{
}
_ = await Launcher.LaunchUriAsync(new Uri($"file://{Path.GetFullPath(".")}/SnapCharacterProvider.cs"));
