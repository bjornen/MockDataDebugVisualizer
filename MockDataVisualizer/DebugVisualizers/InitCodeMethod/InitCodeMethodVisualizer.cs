using System;
using MockDataDebugVisualizer.DebugVisualizers.InitCodeMethod;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(InitCodeMethodVisualizer),
typeof(InitCodeMethodObjectSource),
Target = typeof(WeakReference),
Description = "Mock data init method")]
namespace MockDataDebugVisualizer.DebugVisualizers.InitCodeMethod
{
    public class InitCodeMethodVisualizer : AbstractMockDataVisualizer
    {

    }
}
