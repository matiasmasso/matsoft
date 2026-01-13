Public Class Xl_Rols_CheckList
    Private _Rols As List(Of DTORol)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oRols As List(Of DTORol))
        _Rols = oRols
        For Each oRol As DTORol In _Rols
            CheckedListBox1.Items.Add(oRol.Id.ToString)
        Next
        _AllowEvents = True
    End Sub

    Public Property CheckedValues As List(Of DTORol)
        Get
            Dim retval As New List(Of DTORol)
            For Each index As Integer In CheckedListBox1.CheckedIndices
                retval.Add(_Rols(index))
            Next
            Return retval
        End Get
        Set(value As List(Of DTORol))
            'TODO
        End Set
    End Property

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        RaiseEvent AfterUpdate(Me, New MatEventArgs(e.CurrentValue))
    End Sub
End Class
