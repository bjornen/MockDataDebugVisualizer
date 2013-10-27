using System;
using Microsoft.VisualStudio.DebuggerVisualizers;
using MockDataDebugVisualizer.DebugVisualizers.InitCodeMethodSetValue;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(InitCodeMethodAndSetValueVisualizer),
typeof(InitCodeMethodAndSetValueObjectSource),
Target = typeof(WeakReference),
Description = "Object create method and SetValue method for private memebers")]
namespace MockDataDebugVisualizer.DebugVisualizers.InitCodeMethodSetValue
{
    public class InitCodeMethodAndSetValueVisualizer : AbstractMockDataVisualizer
    {
        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(InitCodeMethodAndSetValueVisualizer), typeof(InitCodeMethodAndSetValueObjectSource));

            visualizerHost.ShowVisualizer();
        }
    }
}
