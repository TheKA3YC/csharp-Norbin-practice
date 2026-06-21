using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Работа_с_БД_из_кода___Задание.Models;

public partial class NetworkMonitoringDbContext : DbContext
{
    public NetworkMonitoringDbContext()
    {
    }

    public NetworkMonitoringDbContext(DbContextOptions<NetworkMonitoringDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ГлавныйАдминистратор> ГлавныйАдминистраторs { get; set; }

    public virtual DbSet<ЖурналИнцидентов> ЖурналИнцидентовs { get; set; }

    public virtual DbSet<Пользователь> Пользовательs { get; set; }

    public virtual DbSet<СетеваяСлужба> СетеваяСлужбаs { get; set; }

    public virtual DbSet<СетевоеУстройство> СетевоеУстройствоs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-C1SB7TT;Database=NetworkMonitoringDB;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ГлавныйАдминистратор>(entity =>
        {
            entity.HasKey(e => e.IdАдминистратора).HasName("PK__Главный___30173F056AFDE49F");

            entity.ToTable("Главный_администратор");

            entity.HasIndex(e => e.Логин, "UQ__Главный___BC2217D3C33F0642").IsUnique();

            entity.Property(e => e.IdАдминистратора).HasColumnName("ID_Администратора");
            entity.Property(e => e.Логин).HasMaxLength(20);
            entity.Property(e => e.Пароль).HasMaxLength(20);
            entity.Property(e => e.Роль).HasMaxLength(30);
            entity.Property(e => e.Фио)
                .HasMaxLength(100)
                .HasColumnName("ФИО");
        });

        modelBuilder.Entity<ЖурналИнцидентов>(entity =>
        {
            entity.HasKey(e => e.IdОтчёта).HasName("PK__Журнал_и__00DDFBE63B2B9931");

            entity.ToTable("Журнал_инцидентов");

            entity.Property(e => e.IdОтчёта).HasColumnName("ID_Отчёта");
            entity.Property(e => e.IdУстройства).HasColumnName("ID_Устройства");
            entity.Property(e => e.ВремяОтветаМс).HasColumnName("Время_ответа_мс");
            entity.Property(e => e.МеткаВремени)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Метка_времени");
            entity.Property(e => e.ОписаниеИнцидента).HasColumnName("Описание_инцидента");
            entity.Property(e => e.РезультатМониторинга)
                .HasMaxLength(50)
                .HasColumnName("Результат_мониторинга");

            entity.HasOne(d => d.IdУстройстваNavigation).WithMany(p => p.ЖурналИнцидентовs)
                .HasForeignKey(d => d.IdУстройства)
                .HasConstraintName("FK__Журнал_ин__ID_Ус__45F365D3");
        });

        modelBuilder.Entity<Пользователь>(entity =>
        {
            entity.HasKey(e => e.IdПользователя).HasName("PK__Пользова__A1478CAD4545CB48");

            entity.ToTable("Пользователь");

            entity.HasIndex(e => e.Логин, "UQ__Пользова__BC2217D341DCE1AC").IsUnique();

            entity.Property(e => e.IdПользователя).HasColumnName("ID_Пользователя");
            entity.Property(e => e.IdАдминистратора).HasColumnName("ID_Администратора");
            entity.Property(e => e.Логин).HasMaxLength(20);
            entity.Property(e => e.Пароль).HasMaxLength(20);
            entity.Property(e => e.Роль).HasMaxLength(30);
            entity.Property(e => e.Фио)
                .HasMaxLength(100)
                .HasColumnName("ФИО");

            entity.HasOne(d => d.IdАдминистратораNavigation).WithMany(p => p.Пользовательs)
                .HasForeignKey(d => d.IdАдминистратора)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Пользоват__ID_Ад__3B75D760");
        });

        modelBuilder.Entity<СетеваяСлужба>(entity =>
        {
            entity.HasKey(e => e.IdСлужбы).HasName("PK__Сетевая___0E37B0006F4FDDA2");

            entity.ToTable("Сетевая_служба");

            entity.HasIndex(e => e.НазваниеСлужбы, "UQ__Сетевая___19F94F3FCF9B3953").IsUnique();

            entity.Property(e => e.IdСлужбы).HasColumnName("ID_Службы");
            entity.Property(e => e.НазваниеСлужбы)
                .HasMaxLength(50)
                .HasColumnName("Название_Службы");
        });

        modelBuilder.Entity<СетевоеУстройство>(entity =>
        {
            entity.HasKey(e => e.IdУстройства).HasName("PK__Сетевое___809B44C46E4CC4F2");

            entity.ToTable("Сетевое_устройство");

            entity.HasIndex(e => new { e.ИмяХоста, e.IpАдрес }, "UQ_Device_Host_IP").IsUnique();

            entity.Property(e => e.IdУстройства).HasColumnName("ID_Устройства");
            entity.Property(e => e.GuidУстройства)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Guid_Устройства");
            entity.Property(e => e.IdПользователя).HasColumnName("ID_Пользователя");
            entity.Property(e => e.IpАдрес)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IP_адрес");
            entity.Property(e => e.ВерсияПрошивки)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Версия_прошивки");
            entity.Property(e => e.ИмяХоста)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Имя_хоста");
            entity.Property(e => e.Местоположение).HasMaxLength(200);
            entity.Property(e => e.НазваниеУстройства)
                .HasMaxLength(50)
                .HasColumnName("Название_устройства");
            entity.Property(e => e.СрокГарантии).HasColumnName("Срок_гарантии");
            entity.Property(e => e.СтоимостьОбслуживания)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Стоимость_Обслуживания");
            entity.Property(e => e.ТекущийСтатус)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("offline")
                .HasColumnName("Текущий_статус");
            entity.Property(e => e.ФлагМониторинга)
                .HasDefaultValue(true)
                .HasColumnName("Флаг_мониторинга");

            entity.HasOne(d => d.IdПользователяNavigation).WithMany(p => p.СетевоеУстройствоs)
                .HasForeignKey(d => d.IdПользователя)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Сетевое_у__ID_По__3F466844");

            entity.HasMany(d => d.IdСлужбыs).WithMany(p => p.IdУстройстваs)
                .UsingEntity<Dictionary<string, object>>(
                    "УстройствоСлужба",
                    r => r.HasOne<СетеваяСлужба>().WithMany()
                        .HasForeignKey("IdСлужбы")
                        .HasConstraintName("FK__Устройств__ID_Сл__4D94879B"),
                    l => l.HasOne<СетевоеУстройство>().WithMany()
                        .HasForeignKey("IdУстройства")
                        .HasConstraintName("FK__Устройств__ID_Ус__4CA06362"),
                    j =>
                    {
                        j.HasKey("IdУстройства", "IdСлужбы").HasName("PK__Устройст__90783FC48A826678");
                        j.ToTable("Устройство_Служба");
                        j.IndexerProperty<int>("IdУстройства").HasColumnName("ID_Устройства");
                        j.IndexerProperty<int>("IdСлужбы").HasColumnName("ID_Службы");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
