using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Services
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platforms platform)
        {
            if(platform == null){
                throw new ArgumentNullException($"Null argument ${nameof(platform)}");
            }

            _context.Add(platform);
        }

        public IEnumerable<Platforms> GetAllPlatform()
        {
            return _context.Platforms.ToList();
        }

        public Platforms GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault<Platforms>(p => p.Id == id);
        }

        public bool SaveChages()
        {
            return _context.SaveChanges() > 0;
        }
    }
}