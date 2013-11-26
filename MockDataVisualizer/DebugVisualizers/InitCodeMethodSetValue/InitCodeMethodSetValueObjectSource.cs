using MockDataDebugVisualizer.InitCodeDumper;

namespace MockDataDebugVisualizer.DebugVisualizers.InitCodeMethodSetValue
{
    public class InitCodeMethodAndSetValueObjectSource : AbstractMockDataObjectSource 
    {
        public override string Dump(object objectToDump)
        {
            return DumperBase.DumpCode(objectToDump, DumpMode.WrappedCodeAndMethod, Visibility.PrivateAndPublic);
        }
    }
}
