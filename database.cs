using System;
using System.Net.Http;
using System.Threading.Tasks;
using Npgsql;
using DependencyInjection.Models;

namespace DependencyInjection
{
    public interface idatabase
    {
        int Create();
        // Task<string> create();
        // Task<string> update();
        // Task<string> delete();
        // Task<String> connection();
        
    }

    public class database : idatabase
    {
        NpgsqlConnection _connection;
        
        public database(NpgsqlConnection connection)
        {
            _connection=connection;
            connection.Open();
            
        }
        public int Create(Member member)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO user (id,username,password,email,fullname,popularity) VALUES (@id, @username, @pass, @email, @fullName, @popularity ) RETURNING id";
            
            command.Parameters.AddWithValue("@id", member.id);
            command.Parameters.AddWithValue("@username", member.username);
            command.Parameters.AddWithValue("@pass", member.password);
            command.Parameters.AddWithValue("@email", member.email);
            command.Parameters.AddWithValue("@fullName", member.full_name);
            command.Parameters.AddWithValue("@popularity", member.popularity);

            command.Prepare();

            var result = command.ExecuteScalar();
            
            return int(member);
        }

        public int Create()
        {
            throw new NotImplementedException();
        }
    }


}