Module CurrentUser
    Public UserID As Integer
    Public Username As String
    Public FullName As String
    Public Role As String

    ' Single source of truth for "is this user an Administrator?" - every form
    ' checks this instead of comparing CurrentUser.Role to the literal string
    ' "Administrator" itself, so a typo can't silently break access control in
    ' just one spot.
    Public ReadOnly Property IsAdmin As Boolean
        Get
            Return Role = "Administrator"
        End Get
    End Property
End Module