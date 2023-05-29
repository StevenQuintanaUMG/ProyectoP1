using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoP1.Clases;

namespace ProyectoP1
{
    internal class VehiculoDB
    {
        public readonly string cadenaConexion = "Data Source=c:\\tmp\\VehiculoDB.db";
        private readonly SQLiteConnection conexion;

        public VehiculoDB()
        {
            conexion = new SQLiteConnection(cadenaConexion);
        }

        public void crearTablas()
        {
            try
            {
                conexion.Open();

                // Tabla Vehiculo
                string sqlVehiculo =
                    @"CREATE TABLE IF NOT EXISTS Vehiculo (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    marca VARCHAR(20),
                    modelo VARCHAR(20),
                    color VARCHAR(40),
                    anio INTEGER,
                    placa VARCHAR(10),
                    tipo VARCHAR(20),
                    velocidadmax INTEGER
                )";
                SQLiteCommand commandVehiculo = new SQLiteCommand(sqlVehiculo, conexion);
                commandVehiculo.ExecuteNonQuery();

                // Tabla PickUp
                string sqlPickUp =
                    @"CREATE TABLE IF NOT EXISTS PickUp (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    es4x4 BOOLEAN,
                    cabina VARCHAR(10),
                    torque INTEGER,
                    carga INTEGER,
                    diesel BOOLEAN,
                    FOREIGN KEY (id) REFERENCES Vehiculo(id)
                )";
                SQLiteCommand commandPickUp = new SQLiteCommand(sqlPickUp, conexion);
                commandPickUp.ExecuteNonQuery();

                // Tabla Sedan
                string sqlSedan =
                    @"CREATE TABLE IF NOT EXISTS Sedan (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    puertas INTEGER,
                    capacidadBaul INTEGER,
                    FOREIGN KEY (id) REFERENCES Vehiculo(id)
               )";
                SQLiteCommand commandSedan = new SQLiteCommand(sqlSedan, conexion);
                commandSedan.ExecuteNonQuery();

                // Tabla SUV
                string sqlSUV =
                    @"CREATE TABLE IF NOT EXISTS SUV (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    es4x4 BOOLEAN,
                    torque INTEGER,
                    FOREIGN KEY (id) REFERENCES Vehiculo(id)
               )";
                SQLiteCommand commandSUV = new SQLiteCommand(sqlSUV, conexion);
                commandSUV.ExecuteNonQuery();

                Console.WriteLine("Tablas creadas correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear las tablas: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public void InsertarVehiculo(IVehiculo vehiculo)
        {
            try
            {
                conexion.Open();

                // Insertar en la tabla Vehiculo utilizando parámetros
                string sqlVehiculo =
                    "INSERT INTO Vehiculo (marca, modelo, color, anio, placa, tipo, velocidadmax) "
                    + "VALUES (@Marca, @Modelo, @Color, @Anio, @Placa, @Tipo, @VelocidadMax)";
                SQLiteCommand commandVehiculo = new SQLiteCommand(sqlVehiculo, conexion);
                commandVehiculo.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                commandVehiculo.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                commandVehiculo.Parameters.AddWithValue("@Color", vehiculo.Color);
                commandVehiculo.Parameters.AddWithValue("@Anio", vehiculo.Anio);
                commandVehiculo.Parameters.AddWithValue("@Placa", vehiculo.Placa);
                commandVehiculo.Parameters.AddWithValue("@Tipo", vehiculo.Tipo);
                commandVehiculo.Parameters.AddWithValue("@VelocidadMax", vehiculo.VelocidadMax);
                commandVehiculo.ExecuteNonQuery();

                long vehiculoId = conexion.LastInsertRowId;
                // Verificar el tipo del vehículo y realizar la inserción correspondiente
                if (vehiculo is PickUp)
                {
                    PickUp pickUp = vehiculo as PickUp;
                    string sqlPickUp =
                        "INSERT INTO PickUp (id, es4x4, cabina, torque, carga, diesel) "
                        + "VALUES (@Id, @Es4x4, @Cabina, @Torque, @Carga, @Diesel)";
                    SQLiteCommand commandPickUp = new SQLiteCommand(sqlPickUp, conexion);
                    commandPickUp.Parameters.AddWithValue("@Id", vehiculoId);
                    commandPickUp.Parameters.AddWithValue("@Es4x4", pickUp.Es4x4);
                    commandPickUp.Parameters.AddWithValue("@Cabina", pickUp.Cabina);
                    commandPickUp.Parameters.AddWithValue("@Torque", pickUp.Torque);
                    commandPickUp.Parameters.AddWithValue("@Carga", pickUp.Carga);
                    commandPickUp.Parameters.AddWithValue("@Diesel", pickUp.Diesel);
                    commandPickUp.ExecuteNonQuery();
                }
                else if (vehiculo is Sedan)
                {
                    Sedan sedan = vehiculo as Sedan;
                    string sqlSedan =
                        "INSERT INTO Sedan (id, puertas, capacidad_baul) "
                        + "VALUES (@Id, @Puertas, @CapacidadBaul)";
                    SQLiteCommand commandSedan = new SQLiteCommand(sqlSedan, conexion);
                    commandSedan.Parameters.AddWithValue("@Id", vehiculoId);
                    commandSedan.Parameters.AddWithValue("@Puertas", sedan.Puertas);
                    commandSedan.Parameters.AddWithValue("@CapacidadBaul", sedan.CapacidadBaul);
                    commandSedan.ExecuteNonQuery();
                }
                else if (vehiculo is SUV)
                {
                    SUV suv = vehiculo as SUV;
                    string sqlSUV =
                        "INSERT INTO SUV (id, es4x4, torque) "
                        + "VALUES (@Id, @Es4x4, @Torque)";
                    SQLiteCommand commandSUV = new SQLiteCommand(sqlSUV, conexion);
                    commandSUV.Parameters.AddWithValue("@Id", vehiculoId);
                    commandSUV.Parameters.AddWithValue("@Es4x4", suv.Es4x4);
                    commandSUV.Parameters.AddWithValue("@Torque", suv.Torque);
                    commandSUV.ExecuteNonQuery();
                }

                Console.WriteLine("Vehiculo insertado correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el Vehiculo: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public List<PickUp> ObtenerPickUps()
        {
            List<PickUp> pickUps = new List<PickUp>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT Vehiculo.*, PickUp.* FROM Vehiculo INNER JOIN PickUp ON Vehiculo.id = PickUp.id";
                using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int vehiculoId = reader.GetInt32(reader.GetOrdinal("id"));
                            string marca = reader.GetString(reader.GetOrdinal("marca"));
                            string modelo = reader.GetString(reader.GetOrdinal("modelo"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            int anio = reader.GetInt32(reader.GetOrdinal("anio"));
                            string placa = reader.GetString(reader.GetOrdinal("placa"));
                            string tipo = reader.GetString(reader.GetOrdinal("tipo"));
                            int velocidadMax = reader.GetInt32(reader.GetOrdinal("velocidadmax"));
                            bool diesel = reader.GetBoolean(reader.GetOrdinal("diesel"));
                            bool es4x4 = reader.GetBoolean(reader.GetOrdinal("es4x4"));
                            string cabina = reader.GetString(reader.GetOrdinal("cabina"));
                            int torque = reader.GetInt32(reader.GetOrdinal("torque"));
                            int carga = reader.GetInt32(reader.GetOrdinal("carga"));

                            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMax);
                            PickUp pickUp = new PickUp(vehiculo, es4x4, cabina, torque, carga, diesel);
                            pickUp.Id = vehiculoId; // Agregar el Id al vehículo PickUp
                            pickUps.Add(pickUp);
                        }
                    }
                }
            }
            return pickUps;
        }


        public List<Sedan> ObtenerSedans()
        {
            List<Sedan> sedans = new List<Sedan>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT Vehiculo.*, Sedan.* FROM Vehiculo INNER JOIN Sedan ON Vehiculo.id = Sedan.id";
                using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            int vehiculoId = reader.GetInt32(reader.GetOrdinal("id"));
                            string marca = reader.GetString(reader.GetOrdinal("marca"));
                            string modelo = reader.GetString(reader.GetOrdinal("modelo"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            int anio = reader.GetInt32(reader.GetOrdinal("anio"));
                            string placa = reader.GetString(reader.GetOrdinal("placa"));
                            string tipo = reader.GetString(reader.GetOrdinal("tipo"));
                            int velocidadMax = reader.GetInt32(reader.GetOrdinal("velocidadmax"));
                            int puertas = reader.GetInt32(reader.GetOrdinal("puertas"));
                            int capacidadBaul = reader.GetInt32(reader.GetOrdinal("capacidad_baul"));

                            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMax);
                            Sedan sedan = new Sedan(vehiculo, puertas, capacidadBaul);
                            sedan.Id = vehiculoId;
                            sedans.Add(sedan);
                        }
                    }
                }
            }
            return sedans;
        }

        public List<SUV> ObtenerSUVs()
        {
            List<SUV> suvs = new List<SUV>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT Vehiculo.*, SUV.* FROM Vehiculo INNER JOIN SUV ON Vehiculo.id = SUV.id";
                using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int vehiculoId = reader.GetInt32(reader.GetOrdinal("id"));
                            string marca = reader.GetString(reader.GetOrdinal("marca"));
                            string modelo = reader.GetString(reader.GetOrdinal("modelo"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            int anio = reader.GetInt32(reader.GetOrdinal("anio"));
                            string placa = reader.GetString(reader.GetOrdinal("placa"));
                            string tipo = reader.GetString(reader.GetOrdinal("tipo"));
                            int velocidadMax = reader.GetInt32(reader.GetOrdinal("velocidadmax"));
                            bool es4x4 = reader.GetBoolean(reader.GetOrdinal("es4x4"));
                            int torque = reader.GetInt32(reader.GetOrdinal("torque"));

                            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMax);
                            SUV suv = new SUV(vehiculo, es4x4, torque);
                            suv.Id = vehiculoId;
                            suvs.Add(suv);
                        }
                    }
                }
            }
            return suvs;
        }

        public PickUp ObtenerPickUpPorId(string id)
        {
            PickUp pickUp = null;
            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT Vehiculo.*, PickUp.* FROM Vehiculo INNER JOIN PickUp ON Vehiculo.id = PickUp.id WHERE Vehiculo.id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int vehiculoId = reader.GetInt32(reader.GetOrdinal("id"));
                            string marca = reader.GetString(reader.GetOrdinal("marca"));
                            string modelo = reader.GetString(reader.GetOrdinal("modelo"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            int anio = reader.GetInt32(reader.GetOrdinal("anio"));
                            string placa = reader.GetString(reader.GetOrdinal("placa"));
                            string tipo = reader.GetString(reader.GetOrdinal("tipo"));
                            int velocidadMax = reader.GetInt32(reader.GetOrdinal("velocidadmax"));
                            bool diesel = reader.GetBoolean(reader.GetOrdinal("diesel"));
                            bool es4x4 = reader.GetBoolean(reader.GetOrdinal("es4x4"));
                            string cabina = reader.GetString(reader.GetOrdinal("cabina"));
                            int torque = reader.GetInt32(reader.GetOrdinal("torque"));
                            int carga = reader.GetInt32(reader.GetOrdinal("carga"));

                            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMax);
                            pickUp = new PickUp(vehiculo, es4x4, cabina, torque, carga, diesel);
                            pickUp.Id = vehiculoId; // Agregar el Id al vehículo PickUp
                        }
                    }
                }
            }
            return pickUp;
        }
        public Sedan ObtenerSedanPorId(string id)
        {
            Sedan sedan = null;
            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT Vehiculo.*, Sedan.* FROM Vehiculo INNER JOIN Sedan ON Vehiculo.id = Sedan.id WHERE Vehiculo.id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int vehiculoId = reader.GetInt32(reader.GetOrdinal("id"));
                            string marca = reader.GetString(reader.GetOrdinal("marca"));
                            string modelo = reader.GetString(reader.GetOrdinal("modelo"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            int anio = reader.GetInt32(reader.GetOrdinal("anio"));
                            string placa = reader.GetString(reader.GetOrdinal("placa"));
                            string tipo = reader.GetString(reader.GetOrdinal("tipo"));
                            int velocidadMax = reader.GetInt32(reader.GetOrdinal("velocidadmax"));
                            int puertas = reader.GetInt32(reader.GetOrdinal("puertas"));
                            int capacidadBaul = reader.GetInt32(reader.GetOrdinal("capacidad_baul"));

                            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMax);
                            sedan = new Sedan(vehiculo, puertas, capacidadBaul);
                            sedan.Id = vehiculoId; // Agregar el Id al vehículo Sedan
                        }
                    }
                }
            }
            return sedan;
        }

        public SUV ObtenerSUVPorId(string id)
        {
            SUV suv = null;
            using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT Vehiculo.*, SUV.* FROM Vehiculo INNER JOIN SUV ON Vehiculo.id = SUV.id WHERE Vehiculo.id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int vehiculoId = reader.GetInt32(reader.GetOrdinal("id"));
                            string marca = reader.GetString(reader.GetOrdinal("marca"));
                            string modelo = reader.GetString(reader.GetOrdinal("modelo"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            int anio = reader.GetInt32(reader.GetOrdinal("anio"));
                            string placa = reader.GetString(reader.GetOrdinal("placa"));
                            string tipo = reader.GetString(reader.GetOrdinal("tipo"));
                            int velocidadMax = reader.GetInt32(reader.GetOrdinal("velocidadmax"));
                            bool es4x4 = reader.GetBoolean(reader.GetOrdinal("es4x4"));
                            int torque = reader.GetInt32(reader.GetOrdinal("torque"));

                            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMax);
                            suv = new SUV(vehiculo, es4x4, torque);
                            suv.Id = vehiculoId; // Agregar el Id al vehículo SUV
                        }
                    }
                }
            }
            return suv;
        }


    }
}
