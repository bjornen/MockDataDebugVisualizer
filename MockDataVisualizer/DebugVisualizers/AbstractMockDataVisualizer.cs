using System.IO;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows.Forms;

namespace MockDataDebugVisualizer.DebugVisualizers
{
    public abstract class AbstractMockDataVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var dataStream = objectProvider.GetData();

            string dump, message;

            using (var reader = new StreamReader(dataStream, Encoding.UTF8))
            {
                dump = reader.ReadToEnd();
                message = "The initilization code is now in the clipboard!";
            }

            if (string.IsNullOrWhiteSpace(dump))
            {
                dump = "Unable to create initilization code.";
                message = dump;
            }

            Clipboard.SetText(dump);

            MessageBox.Show(message);
        }
    }
}
