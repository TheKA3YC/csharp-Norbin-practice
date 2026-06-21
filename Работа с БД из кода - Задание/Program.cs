using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Работа_с_БД_из_кода___Задание.Models;

namespace Работа_с_БД_из_кода__Задание
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Строка подключения: {connectionString}\n");

            Console.WriteLine("Entity Framework:");
            await PerformEntityFrameworkCrudAsync(connectionString);

            Console.WriteLine("\nADO.NET:");
            await PerformAdoNetCrudAsync(connectionString);
        }

        static async Task PerformEntityFrameworkCrudAsync(string connectionString)
        {
            var options = new DbContextOptionsBuilder<NetworkMonitoringDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var db = new NetworkMonitoringDbContext(options);

            // CREATE
            var newDevice = new СетевоеУстройство
            {
                IdПользователя = 1,
                НазваниеУстройства = "TestDeviceEF",
                IpАдрес = "192.168.1.100",
                Местоположение = "Кабинет 512",
                ИмяХоста = "ef-test-host",
                ВерсияПрошивки = "v1.0.0"
            };

            db.СетевоеУстройствоs.Add(newDevice);
            await db.SaveChangesAsync();
            Console.WriteLine("Create: Устройство успешно добавлено.");

            // READ
            var device = await db.СетевоеУстройствоs.FirstOrDefaultAsync(d => d.НазваниеУстройства == "TestDeviceEF");
            if (device != null)
            {
                Console.WriteLine($"Read: Найдено устройство - {device.НазваниеУстройства} (IP: {device.IpАдрес})");
            }

            // UPDATE
            if (device != null)
            {
                device.НазваниеУстройства = "TestDeviceEF_Updated";
                await db.SaveChangesAsync();
                Console.WriteLine("Update: Название устройства обновлено.");
            }

            // DELETE
            if (device != null)
            {
                db.СетевоеУстройствоs.Remove(device);
                await db.SaveChangesAsync();
                Console.WriteLine("Delete: Устройство удалено.");
            }
        }

        static async Task PerformAdoNetCrudAsync(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // CREATE
            string insertSql = @"INSERT INTO Сетевое_устройство 
                (ID_Пользователя, Название_устройства, IP_адрес, Местоположение, Имя_хоста, Версия_прошивки) 
                VALUES (@userId, @name, @ip, @location, @host, @firmware)";

            using (var cmd = new SqlCommand(insertSql, connection))
            {
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = "TestDeviceADO";
                cmd.Parameters.Add("@ip", SqlDbType.VarChar, 100).Value = "10.0.0.1";
                cmd.Parameters.Add("@location", SqlDbType.NVarChar, 200).Value = "Кабинет 413";
                cmd.Parameters.Add("@host", SqlDbType.VarChar, 100).Value = "ado-test-host";
                cmd.Parameters.Add("@firmware", SqlDbType.VarChar, 100).Value = "v2.1";

                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Create: Устройство добавлено.");
            }

            // READ
            string selectSql = "SELECT Название_устройства, IP_адрес FROM Сетевое_устройство WHERE Название_устройства = @name";
            using (var cmd = new SqlCommand(selectSql, connection))
            {
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = "TestDeviceADO";

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    Console.WriteLine($"Read: Найдено устройство - {reader["Название_устройства"]} (IP: {reader["IP_адрес"]})");
                }
            }

            // UPDATE
            string updateSql = "UPDATE Сетевое_устройство SET Название_устройства = @newName WHERE Название_устройства = @oldName";
            using (var cmd = new SqlCommand(updateSql, connection))
            {
                cmd.Parameters.Add("@newName", SqlDbType.NVarChar, 50).Value = "TestDeviceADO_Updated";
                cmd.Parameters.Add("@oldName", SqlDbType.NVarChar, 50).Value = "TestDeviceADO";

                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Update: Устройство обновлено.");
            }

            // DELETE
            string deleteSql = "DELETE FROM Сетевое_устройство WHERE Название_устройства = @name";
            using (var cmd = new SqlCommand(deleteSql, connection))
            {
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = "TestDeviceADO_Updated";

                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Delete: Устройство удалено.");
            }
        }
    }
}