

namespace BtOrmCore
{
    public class DBModelsBaseObject
    {
        private bool IsDirty { get; set; }

        public void SetRecordDirty()
        {
            IsDirty = true;
        }

        public bool IsRecordDirty()
        {
            return IsDirty;
        }
    }
}
