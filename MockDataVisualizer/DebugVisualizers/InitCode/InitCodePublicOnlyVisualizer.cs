using System;
using Microsoft.VisualStudio.DebuggerVisualizers;
using MockDataDebugVisualizer.DebugVisualizers.InitCode;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(InitCodePublicOnlyVisualizer),
typeof(InitCodePublicOnlyObjectSource),
Target = typeof(WeakReference),
Description = "Object init code public only")]
namespace MockDataDebugVisualizer.DebugVisualizers.InitCode
{
    public class InitCodePublicOnlyVisualizer : AbstractMockDataVisualizer
    {
        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(InitCodePublicOnlyVisualizer), typeof(InitCodePublicOnlyObjectSource));

            visualizerHost.ShowVisualizer();
        }
    }
}
