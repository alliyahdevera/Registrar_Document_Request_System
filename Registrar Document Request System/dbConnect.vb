Imports MySql.Data.MySqlClient
Module DBConnection
    Public connStr As String = "server=localhost;userid=root;password=YOUR_PASSWORD;database=registrar_db;"
    Public Function GetConnection() As MySqlConnection
        Return New MySqlConnection(connStr)
    End Function
End Module