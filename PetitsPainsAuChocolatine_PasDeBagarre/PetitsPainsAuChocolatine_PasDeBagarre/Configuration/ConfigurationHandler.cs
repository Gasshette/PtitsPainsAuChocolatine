using System;
using System.IO;

namespace PetitsPainsAuChocolatine_PasDeBagarre
{
    public class ConfigurationHandler
    {
        public string UsersFilePath { get; set; } = 
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PtitsPainsAuChocolatine/users.csv"
            );
    }
}
