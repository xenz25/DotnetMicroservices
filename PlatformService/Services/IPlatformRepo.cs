using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Services
{
    public interface IPlatformRepo
    {
        bool SaveChages();

        IEnumerable<Platforms> GetAllPlatform();

        Platforms GetPlatformById(int id);

        void CreatePlatform(Platforms platforms);
    }
}