Public Class DTOSpvIn
    Inherits DTOBaseGuid

    'Public Shadows Property Guid As Guid
    'Public Shadows Property Guid As Guid

    Property Emp As DTOEmp
    Property Id As Integer
    Property Fch As Date
    Property Expedicio As String
    Property Bultos As Integer
    Property Kg As Integer
    Property M3 As Decimal
    Property Obs As String
    Property Spvs As List(Of DTOSpv)

    Property SpvCount As Integer
    Property UsrNum As Integer
    Property User As DTOUser

    Shared Function Factory() As DTOSpvIn
        Dim retval As New DTOSpvIn
        retval.Guid = Guid.NewGuid
        Return retval
    End Function

    Public Sub New()
        MyBase.New()
        _Spvs = New List(Of DTOSpv)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Spvs = New List(Of DTOSpv)
    End Sub

    Shared Function Factory(oUser As DTOUser, DtFch As Date) As DTOSpvIn
        Dim retval As New DTOSpvIn
        With retval
            .User = oUser
            .Emp = oUser.Emp
            .Fch = DtFch
        End With
        Return retval
    End Function

    Shared Function DeleteQuery(src As List(Of DTOSpvIn)) As String
        Dim retval As String = ""
        Select Case src.Count
            Case 0
                retval = "No hi han entrades sel·leccionades"
            Case 1
                retval = String.Format("retrocedim la entrada {0}?", src.First.Id)
            Case Else
                retval = "retrocedim les entrades sel·leccionades?"
        End Select
        Return retval
    End Function
End Class