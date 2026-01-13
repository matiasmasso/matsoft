Public Class Xl_MatDateTimePicker
    Inherits DateTimePicker

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private _isDroppedDown As Boolean = False

    Public Property IsDroppedDown() As Boolean
        Get
            Return _isDroppedDown
        End Get
        Set(value As Boolean)
            _isDroppedDown = value
        End Set
    End Property

    Private Sub MyDateTimePicker_CloseUp(sender As Object, e As System.EventArgs) Handles Me.CloseUp
        _isDroppedDown = False
    End Sub

    Private Sub MyDateTimePicker_DropDown(sender As Object, e As System.EventArgs) Handles Me.DropDown
        _isDroppedDown = True
    End Sub

End Class
