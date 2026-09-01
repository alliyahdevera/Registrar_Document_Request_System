Imports MySql.Data.MySqlClient
Module DBConnection
    Public connStr As String = "server=localhost;userid=root;password=;database=registrar_db;"
    Public Function GetConnection() As MySqlConnection
        Return New MySqlConnection(connStr)
    End Function
    Public dr As MySqlDataReader
End Module