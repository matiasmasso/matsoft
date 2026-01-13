Public Class Xl_LookupBookFra

    Inherits Xl_LookupTextboxButton

    Private _BookFra As DTOBookFra

    Public Event RequestToLookUp(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property BookFra() As DTOBookFra
        Get
            Return _BookFra
        End Get
        Set(ByVal value As DTOBookFra)
            _BookFra = value
            If _BookFra Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = DTOBookFra.Caption(_BookFra)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.BookFra = Nothing
    End Sub

    Private Sub onBookFraSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _BookFra = e.Argument
        MyBase.Text = DTOBookFra.Caption(_BookFra)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class

