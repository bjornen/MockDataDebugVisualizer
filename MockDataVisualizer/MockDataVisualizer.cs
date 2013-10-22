using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows.Forms;
using MockDataDebugVisualizer;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(MockDataVisualizer),
typeof(MockDataObjectSource),
Target = typeof(WeakReference),
Description = "Mock Data Visualizer")]
namespace MockDataDebugVisualizer
{
    public class MockDataVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var dataStream = objectProvider.GetData();

            string dump;

            using (var reader = new StreamReader(dataStream, Encoding.UTF8))
            {
                dump = reader.ReadToEnd();
            }

            Clipboard.SetText(dump);

            MessageBox.Show(dump);
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            //var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DebuggerSide));
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(MockDataVisualizer), typeof(MockDataObjectSource));
            
            visualizerHost.ShowVisualizer();
        }
    }
}
