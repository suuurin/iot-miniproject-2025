﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WpfMqttSubApp.Models;

public partial class IotDbContext : DbContext
{
    public IotDbContext()
    {
    }

    public IotDbContext(DbContextOptions<IotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=miniproject;uid=root;pwd=12345;charset=utf8", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.PrcIdx).HasName("PRIMARY");

            entity.ToTable("processes");

            entity.HasIndex(e => e.SchIdx, "fk_processes_schedules_idx");

            entity.HasIndex(e => e.PrcCd, "prcCd_UNIQUE").IsUnique();

            entity.Property(e => e.PrcIdx).HasColumnName("prcIdx");
            entity.Property(e => e.ModDt)
                .HasColumnType("datetime")
                .HasColumnName("modDt");
            entity.Property(e => e.PrcCd)
                .HasMaxLength(43)
                .IsFixedLength()
                .HasComment("공정처리 ID(UK)\nyyyyMMdd-NewGuid(36)")
                .HasColumnName("prcCd");
            entity.Property(e => e.PrcDate).HasColumnName("prcDate");
            entity.Property(e => e.PrcEndTime)
                .HasColumnType("time")
                .HasColumnName("prcEndTime");
            entity.Property(e => e.PrcFacilityId)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("prcFacilityId");
            entity.Property(e => e.PrcLoadTime).HasColumnName("prcLoadTime");
            entity.Property(e => e.PrcResult).HasColumnName("prcResult");
            entity.Property(e => e.PrcStartTime)
                .HasColumnType("time")
                .HasColumnName("prcStartTime");
            entity.Property(e => e.RegDt)
                .HasColumnType("datetime")
                .HasColumnName("regDt");
            entity.Property(e => e.SchIdx).HasColumnName("schIdx");

            entity.HasOne(d => d.SchIdxNavigation).WithMany(p => p.Processes)
                .HasForeignKey(d => d.SchIdx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_processes_schedules");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.SchIdx).HasName("PRIMARY");

            entity.ToTable("schedules");

            entity.Property(e => e.SchIdx).HasColumnName("schIdx");
            entity.Property(e => e.LoadTime)
                .HasComment("로드타임(초)")
                .HasColumnName("loadTime");
            entity.Property(e => e.ModDt)
                .HasComment("수정일")
                .HasColumnType("datetime")
                .HasColumnName("modDt");
            entity.Property(e => e.PlantCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("공장코드")
                .HasColumnName("plantCode");
            entity.Property(e => e.RedDt)
                .HasComment("작성일")
                .HasColumnType("datetime")
                .HasColumnName("redDt");
            entity.Property(e => e.SchAmount)
                .HasComment("계획목표수량")
                .HasColumnName("schAmount");
            entity.Property(e => e.SchDate).HasColumnName("schDate");
            entity.Property(e => e.SchEndTime)
                .HasComment("계획 종료시간")
                .HasColumnType("time")
                .HasColumnName("schEndTime");
            entity.Property(e => e.SchFacilityId)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("생산설비ID")
                .HasColumnName("schFacilityId");
            entity.Property(e => e.SchStartTime)
                .HasComment("계획 시작시간")
                .HasColumnType("time")
                .HasColumnName("schStartTime");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.BasicCode).HasName("PRIMARY");

            entity.ToTable("settings");

            entity.Property(e => e.BasicCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("basicCode");
            entity.Property(e => e.CodeDesc)
                .HasComment("코드설명")
                .HasColumnName("codeDesc");
            entity.Property(e => e.CodeName)
                .HasMaxLength(100)
                .HasColumnName("codeName");
            entity.Property(e => e.ModDt)
                .HasColumnType("datetime")
                .HasColumnName("modDt");
            entity.Property(e => e.RegDt)
                .HasColumnType("datetime")
                .HasColumnName("regDt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
