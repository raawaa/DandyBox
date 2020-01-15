using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace DandyBox.Core.DataModels
{
    public class DataContext : DbContext
    {
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<MovieInfo> MovieInfos        { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=dandybox.db");
    }
}
