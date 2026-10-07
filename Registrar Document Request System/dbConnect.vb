Imports MySql.Data.MySqlClient
Module DBConnection
    Public cn As New MySqlConnection
    Public cmd As MySqlCommand
    Public dr As MySqlDataReader 'retrieve
    Public sql As String 'sql command
    Public Function connection() As Boolean
        Try
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            cn.ConnectionString = "server=localhost;userid=root;password=;database=registrar_db;"
            cn.Open()
            Return True
        Catch ex As MySqlException
            MsgBox("Database connection failed: " & ex.Message, vbCritical, "Connection Error")
            Return False
        Catch ex As Exception
            MsgBox("An unexpected error occurred: " & ex.Message, vbCritical, "Error")
            Return False
        End Try
    End Function
End Module
Module ConfirmHelper
    ' Returns True only if the user clicks Yes. "No" is the default button
    ' so pressing Enter by accident will not save anything.
    Public Function ConfirmAction(message As String, title As String) As Boolean
        Return MsgBox(message, vbYesNo + vbQuestion + vbDefaultButton2, title) = MsgBoxResult.Yes
    End Function
End Module
Module RequestRules
    ' Runs the automatic cancellation rules. Call it whenever a page that lists requests opens.
    Public Sub ApplyAutoRules()
        Try
            Call connection()

            ' RULE 1: not paid + still Pending after 7 days -> Cancelled
            sql = "UPDATE tblrequest SET Status = 'Cancelled' " &
                  "WHERE (Status = 'Pending' OR Status IS NULL OR Status = '') " &
                  "AND (PaymentStatus IS NULL OR PaymentStatus <> 'Paid') " &
                  "AND IFNULL(AmountPaid, 0) = 0 " &
                  "AND RequestDate <= DATE_SUB(CURDATE(), INTERVAL 7 DAY)"
            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()

            ' RULE 2: PAID requests are never auto-cancelled, even after 1 month.
            ' Safety net only: an UNPAID request stuck at Ready for Release for 1 month is cancelled.
            sql = "UPDATE tblrequest SET Status = 'Cancelled', ReadyForReleaseDate = NULL " &
                  "WHERE Status = 'Ready for Release' " &
                  "AND ReadyForReleaseDate IS NOT NULL " &
                  "AND ReadyForReleaseDate <= DATE_SUB(NOW(), INTERVAL 1 MONTH) " &
                  "AND (PaymentStatus IS NULL OR PaymentStatus <> 'Paid') " &
                  "AND IFNULL(AmountPaid, 0) = 0"
            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()

            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub
End Module
