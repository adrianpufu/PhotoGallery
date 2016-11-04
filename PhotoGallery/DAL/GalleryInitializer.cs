using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PhotoGallery.Models;

namespace PhotoGallery.DAL
{
    public class GalleryInitializer : System.Data.Entity.
    DropCreateDatabaseIfModelChanges<GalleryContext>
    {
        protected override void Seed(GalleryContext context)
        {
            var photos = new List<Photo>
            {
            new Photo{Name="Carson1",Description="Alexander1",Path="../photos/photo1.jpg"},
            new Photo{Name="Carson2",Description="Alexander2",Path="../photos/photo2.jpg"},
            new Photo{Name="Carson3",Description="Alexander3",Path="../photos/photo3.jpg"},
            new Photo{Name="Carson4",Description="Alexander4",Path="../photos/photo4.jpg"},
            new Photo{Name="Carson5",Description="Alexander5",Path="../photos/photo5.jpg"}
            };
            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();
        }
    }
}