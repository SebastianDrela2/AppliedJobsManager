using System.IO;

namespace AppliedJobsManager.JsonProcessing
{
    public abstract class JsonDirectory
    {
        public void CreateDirectoryIfDoesntExist(string path)
        {
            var aboveDir = Path.GetDirectoryName(path);

            if (!Directory.Exists(aboveDir))
            {
                Directory.CreateDirectory(aboveDir!);
            }
        }
    }
}
