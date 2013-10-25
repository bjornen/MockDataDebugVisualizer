using System;
using MockDataDebugVisualizer.DebugVisualizers.InitCode;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(InitCodeObjectSource),
Target = typeof(WeakReference),
Description = "Mock data init code")]
namespace MockDataDebugVisualizer.DebugVisualizers.InitCode
{
    public class InitCodeVisualizer : AbstractMockDataVisualizer
    {

    }
}
