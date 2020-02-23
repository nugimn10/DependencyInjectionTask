using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Npgsql;
using System.Linq;
using System.Collections.Generic;

using DependencyInjection.Models;

namespace DependencyInjection
{
    public interface IDatabase
    {
        
        int CreateMember(Member member);
        List<Member> ReadPost();
        Member readByID(int id);
        void updateMember(int id, [FromBody]JsonPatchDocument<Member> data);
        void delMember(int id);
        
    }

    public class Database : IDatabase
    {
        NpgsqlConnection _connection;
        
        public database(NpgsqlConnection connection)
        {
            _connection=connection;
            _connection.Open();
            
        }
        public int CreateMember(Member member)
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
            
            return (int)member;
        }

        public List<Member> readMember( )
        {
            var command = _connection.CreateCommand();
            command.CommandText= "SELECT * FROM member";
            var result = command.ExecuteReader();
            var member = new List<Member>();
            while (result.Read())
                Member.Add(new Member()
                {
                    id = (int)result[0],
                    username = (string)result[1],
                    password = (string)result[2],
                    email = (string)result[3],
                    full_name = (string)result[4],
                    popularity = (string)result[5],
                });
            _connection.Close();
            return Member;

        }

        public Member readByID(int id){
            var command = _connection.CreateCommand();

            command.CommandText = $"SELECT * FROM member WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            var result = command.ExecuteReader();
            result.Read();

            var memberId = new Member(){
                    id = (int)result[0],
                    username = (string)result[1],
                    password = (string)result[2],
                    email = (string)result[3],
                    full_name = (string)result[4],
                    popularity = (string)result[5],
            };
            _connection.Close();
            return posts;
            Id;
        }

            public void updatePost(int id, [FromBody]JsonPatchDocument<Member> data){
            var command = _connection.CreateCommand();
            var memberId = readByID(id);
            _connection.Open();

            data.ApplyTo(memberId);

            command.CommandText = $"UPDATE Posts SET(username,password,email,fullname,popularity) = (@username, @pass, @email, @fullName, @popularity ) WHERE id = {id}";
            command.Parameters.AddWithValue("@username", memberId.username);
            command.Parameters.AddWithValue("@pass", memberId.password);
            command.Parameters.AddWithValue("@email", memberId.email);
            command.Parameters.AddWithValue("@fullName", memberId.full_name);
            command.Parameters.AddWithValue("@popularity", memberId.popularity);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void delMember(int id){
            var command = _connection.CreateCommand();

            command.CommandText = $"DELETE FROM member WHERE id = {id}";

            var result = command.ExecuteNonQuery();
            _connection.Close();

        }

    }


}