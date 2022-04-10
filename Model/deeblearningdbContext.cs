using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace webproject2.Model
{
    public partial class deeblearningdbContext : DbContext
    {
        public deeblearningdbContext()
        {
        }

        public deeblearningdbContext(DbContextOptions<deeblearningdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dbn> Dbn { get; set; }
        public virtual DbSet<Prediction> Prediction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SQL5102.site4now.net;Initial Catalog=db_a7cc6b_deeplearning;User Id=db_a7cc6b_deeplearning_admin;Password=Mas@2266#;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dbn>(entity =>
            {
                entity.ToTable("dbn");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datasetfilename)
                    .HasColumnName("datasetfilename")
                    .HasMaxLength(250);

                entity.Property(e => e.Datasetsize).HasColumnName("datasetsize");

                entity.Property(e => e.Dbnname)
                    .HasColumnName("dbnname")
                    .HasMaxLength(250);

                entity.Property(e => e.Hiddenlayerscount).HasColumnName("hiddenlayerscount");

                entity.Property(e => e.Hiddenneuronscount).HasColumnName("hiddenneuronscount");

                entity.Property(e => e.Inputneuronscount).HasColumnName("inputneuronscount");

                entity.Property(e => e.Learningrate)
                    .HasColumnName("learningrate")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumoftrainingEpoch).HasColumnName("numoftrainingEpoch");

                entity.Property(e => e.Outputneuronscount).HasColumnName("outputneuronscount");

                entity.Property(e => e.Trainingset).HasColumnName("trainingset");
            });

            modelBuilder.Entity<Prediction>(entity =>
            {
                entity.ToTable("prediction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Buildingage)
                    .HasColumnName("buildingage")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Buildingarea)
                    .HasColumnName("buildingarea")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Cateringandservices).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Cleaning).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dbnid).HasColumnName("dbnid");

                entity.Property(e => e.Demolitioncost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Endoflifecost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Energycost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Envelopetype)
                    .HasColumnName("envelopetype")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Environmentalcost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Errorratio)
                    .HasColumnName("errorratio")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Floorheight)
                    .HasColumnName("floorheight")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Initialcost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Majorrepairs).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MarketpriceofresultedCo2)
                    .HasColumnName("MarketpriceofresultedCO2")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Nooffloores)
                    .HasColumnName("nooffloores")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OperatingandMaintenance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PeriodicMaintena).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Projectname)
                    .HasColumnName("projectname")
                    .HasMaxLength(250);

                entity.Property(e => e.RentandInsurances).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Salvagevalue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Structureandenvelopematerial).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Structuretype)
                    .HasColumnName("structuretype")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Sustainablegrad)
                    .HasColumnName("sustainablegrad")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Totallcccost)
                    .HasColumnName("TOTALlcccost")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Yearofbuilt)
                    .HasColumnName("yearofbuilt")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Dbn)
                    .WithMany(p => p.Prediction)
                    .HasForeignKey(d => d.Dbnid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_prediction_dbn");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
