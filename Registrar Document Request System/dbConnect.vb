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
    Public Function ConfirmAction(message As String, title As String) As Boolean
        Return MsgBox(message, vbYesNo + vbQuestion + vbDefaultButton2, title) = MsgBoxResult.Yes
    End Function
End Module

Module RequestRules
    Public Sub ApplyAutoRules()
        Try
            Call connection()

            sql = "UPDATE tblrequest SET Status = 'Cancelled' " &
                  "WHERE (Status = 'Pending' OR Status IS NULL OR Status = '') " &
                  "AND (PaymentStatus IS NULL OR PaymentStatus <> 'Paid') " &
                  "AND IFNULL(AmountPaid, 0) = 0 " &
                  "AND RequestDate <= DATE_SUB(CURDATE(), INTERVAL 7 DAY)"
            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()

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

Module SchoolYear
    Public IsAllTime As Boolean = True
    Public SchoolYearID As Integer = 0
    Public SchoolYearName As String = "All Time"
    Public StartDate As Date
    Public EndDate As Date

    Public ReadOnly Property DisplayName As String
        Get
            Return If(IsAllTime, "All Time", SchoolYearName)
        End Get
    End Property

    Public Sub SelectAllTime()
        IsAllTime = True
        SchoolYearID = 0
        SchoolYearName = "All Time"
    End Sub

    Public Sub SelectYear(id As Integer, name As String, startD As Date, endD As Date)
        IsAllTime = False
        SchoolYearID = id
        SchoolYearName = name
        StartDate = startD.Date
        EndDate = endD.Date
    End Sub

    Public Function AndRequestDate(Optional col As String = "r.RequestDate") As String
        If IsAllTime Then Return ""
        Dim ci As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
        Return " AND " & col & " >= '" & StartDate.ToString("yyyy-MM-dd", ci) & "'" &
               " AND " & col & " < '" & EndDate.AddDays(1).ToString("yyyy-MM-dd", ci) & "' "
    End Function
End Module

Module ActivityLogger
    Public Sub LogActivity(actionType As String, details As String)
        Try
            If details Is Nothing Then details = ""
            If details.Length > 500 Then details = details.Substring(0, 500)
            Using c As New MySqlConnection("server=localhost;userid=root;password=;database=registrar_db;")
                c.Open()
                Using k As New MySqlCommand("INSERT INTO tblactivitylogs (UserID, Username, FullName, Role, ActionType, Details) " &
                                            "VALUES (@uid, @un, @fn, @role, @act, @det)", c)
                    k.Parameters.AddWithValue("@uid", CurrentUser.UserID)
                    k.Parameters.AddWithValue("@un", CurrentUser.Username)
                    k.Parameters.AddWithValue("@fn", CurrentUser.FullName)
                    k.Parameters.AddWithValue("@role", CurrentUser.Role)
                    k.Parameters.AddWithValue("@act", actionType)
                    k.Parameters.AddWithValue("@det", details)
                    k.ExecuteNonQuery()
                End Using
            End Using
        Catch

        End Try
    End Sub
End Module