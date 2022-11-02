﻿using Dapper;
using GenCryptography.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCryptography.Data.Repositories.Implementation
{
    public class UserEncryptionRepository : IUserEncryptionRepository
    {
        private readonly string _connectionString;

        public UserEncryptionRepository(string connectionString)
        {
            _connectionString = connectionString;
            CreateTable();
        }

        private int CreateTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = $@"IF 
	                                    (NOT EXISTS (SELECT TABLE_CATALOG FROM INFORMATION_SCHEMA.TABLES  
                                                     WHERE TABLE_SCHEMA = 'auth' 
                                                     AND  TABLE_NAME = 'userencryption')) 
                                    AND
	                                    (EXISTS (SELECT TABLE_CATALOG FROM INFORMATION_SCHEMA.TABLES  
                                                     WHERE TABLE_SCHEMA = 'auth' 
                                                     AND  TABLE_NAME = 'users'))
                                    BEGIN
                                        CREATE TABLE auth.userencryption (
		                                    UserEncryptionId BIGINT IDENTITY (1, 1) PRIMARY KEY,
		                                    UserId BIGINT NOT NULL FOREIGN KEY REFERENCES auth.users(UserId) ON DELETE CASCADE,
		                                    EncryptionKey VARBINARY (MAX) NOT NULL,
		                                    UNIQUE (UserId),
		                                    UNIQUE (EncryptionKey)
	                                    )
                                    END";

                    var result = connection.Execute(sql);

                    connection.Close();

                    return result;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private int DropTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = $@"DROP TABLE IF EXISTS auth.userencryption";

                    var result = connection.Execute(sql);

                    connection.Close();

                    return result;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public byte[] InsertOrUpdate(long userId, byte[] encryptionKey)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = $@"IF EXISTS (SELECT UserEncryptionId FROM auth.userencryption WHERE UserId = @UserId)
                                    BEGIN
		                                UPDATE 
			                                auth.userencryption
		                                SET	
			                                EncryptionKey = @EncryptionKey
                                        OUTPUT inserted.EncryptionKey
		                                WHERE
			                                UserId = @UserId
                                    END
                                    ELSE
                                    BEGIN
                                        INSERT INTO auth.userencryption (UserId, EncryptionKey)
									    OUTPUT inserted.EncryptionKey 
	                                    VALUES (@UserId, @EncryptionKey)
                                    END";

                    var result = connection.ExecuteScalar<byte[]>(sql, new
                    {
                        UserId = userId,
                        EncryptionKey = encryptionKey
                    });

                    connection.Close();

                    return result;
                }
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
