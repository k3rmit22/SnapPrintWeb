using Microsoft.EntityFrameworkCore;
using SnapPrintWeb.Data;

namespace SnapPrintWeb.Data
{
    public class SnapPrintDbContext:DbContext
    {
        public SnapPrintDbContext(DbContextOptions<SnapPrintDbContext> options) 
            : base(options)
        {
        }
        // DbSet for the uploaded_files table
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UploadedFile>()
                .HasKey(u => u.FileId);
        }

    }
}
