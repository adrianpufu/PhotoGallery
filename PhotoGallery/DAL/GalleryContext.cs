using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PhotoGallery.Models;

namespace PhotoGallery.DAL
{
    public class GalleryContext : DbContext
    {
        public GalleryContext() : base("GalleryContext")
        {
        }
        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}