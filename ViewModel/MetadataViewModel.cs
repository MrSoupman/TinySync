using TinySync.Model;
using TinySync.Services;

namespace TinySync.ViewModel
{
    public class MetadataViewModel : BaseViewModel
    {
        private readonly Metadata _metadata;

        public string Origin => _metadata.Origin;

        public string Remote => _metadata.Remote;

        public string Status => _metadata.Status;
        public MetadataViewModel(Metadata metadata)
        {
            _metadata = metadata;
            metadata.Status = ShaSvc.TestSHA(metadata) ? "Up to Date" : "Unsynced";

        }
    }
}
