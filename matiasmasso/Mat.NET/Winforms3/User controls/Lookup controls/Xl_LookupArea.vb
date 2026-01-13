Public Class Xl_LookupArea

    Inherits Xl_LookupTextboxButton

    Private _Area As DTOArea
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows ReadOnly Property Area() As DTOArea
        Get
            Return _Area
        End Get
    End Property

    Public Shadows Sub Load(oArea As DTOArea)
        _Area = oArea
        refresca()
    End Sub

    Public Sub Clear()
        Load(Nothing)
    End Sub

    Private Sub onAreaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Area = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Area Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = DTOArea.FullNomSegmented(_Area, Current.Session.User.Lang)
            Dim oMenu_Area As New Menu_Area(_Area)
            AddHandler oMenu_Area.AfterUpdate, AddressOf refresca
            If oMenu_Area.Range IsNot Nothing Then
                MyBase.SetContextMenuRange(oMenu_Area.Range)
            End If
        End If
    End Sub

End Class