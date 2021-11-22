using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ABMPersonasApi.Models;
namespace ABMPersonasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                SELECT ci,nombre, email, telefono, 
                DATE_FORMAT(fechanac, '%d -%m -%Y') as fechanac
                FROM clientes;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ABMPersonasAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{ci}")]
        public JsonResult Get(int ci)
        {
            string query = @"
                SELECT * FROM clientes 
                WHERE ci=@ci;
            ";

            try
            {

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ABMPersonasAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }

                return new JsonResult(table);
            }catch(MySqlException excep)
            {
                return new JsonResult(false);

            }
        }

        [HttpPost]
        public JsonResult POST(ClienteModel cliente)
        {
            string query = @"
                INSERT INTO clientes(ci, nombre, email, telefono, fechanac)
                VALUES				 
                (@ci, @nombre, @email, @telefono, @fechanac)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ABMPersonasAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ci", cliente.ci);
                    myCommand.Parameters.AddWithValue("@nombre", cliente.nombre);
                    myCommand.Parameters.AddWithValue("@email", cliente.email);
                    myCommand.Parameters.AddWithValue("@telefono", cliente.telefono);
                    myCommand.Parameters.AddWithValue("@fechanac", cliente.fechanac);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Se ha agregado el usuario correctamente!");
        }

        [HttpPut]
        public JsonResult Put(ClienteModel cliente)
        {
            string query = @"
                UPDATE clientes SET 
                ci=@ci,
                nombre=@nombre,
                email=@fechanac,
                telefono=@fechanac,
                fechanac=@fechanac
                WHERE id=@id;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ABMPersonasAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@id", cliente.id);
                    myCommand.Parameters.AddWithValue("@ci", cliente.ci);
                    myCommand.Parameters.AddWithValue("@nombre", cliente.nombre);
                    myCommand.Parameters.AddWithValue("@email", cliente.email);
                    myCommand.Parameters.AddWithValue("@telefono", cliente.telefono);
                    myCommand.Parameters.AddWithValue("@fechanac", cliente.fechanac);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Usuario Modificado Correctamente!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from clientes 
                where id=@id;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ABMPersonasAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            Console.WriteLine(id);

            return new JsonResult("Usuario Eliminado correctamente!");
        }

    }
}
