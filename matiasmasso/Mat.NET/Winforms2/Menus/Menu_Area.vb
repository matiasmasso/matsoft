Public Class Menu_Area
    Private _Area As DTOArea

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oArea As DTOArea)
        MyBase.New()
        _Area = oArea
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Dim exs As New List(Of Exception)
        Dim retval As ToolStripMenuItem() = Nothing
        If TypeOf _Area Is DTOCountry Then
            Dim oMenu As New Menu_Country(_Area)
            AddHandler oMenu.AfterUpdate, AddressOf refreshrequest
            retval = oMenu.Range
        ElseIf TypeOf _Area Is DTOZona Then
            Dim oMenu As New Menu_Zona(_Area)
            AddHandler oMenu.AfterUpdate, AddressOf refreshrequest
            retval = oMenu.Range
        ElseIf TypeOf _Area Is DTOLocation Then
            Dim oMenu As New Menu_Location(_Area)
            AddHandler oMenu.AfterUpdate, AddressOf refreshrequest
            retval = oMenu.Range
        ElseIf TypeOf _Area Is DTOZip Then
            Dim oMenu As New Menu_Zip(_Area)
            AddHandler oMenu.AfterUpdate, AddressOf refreshrequest
            retval = oMenu.Range
        ElseIf TypeOf _Area Is DTOContact Then
            Dim oContactMenu = FEB.ContactMenu.FindSync(exs, _Area)
            Dim oMenu As New Menu_Contact(_Area, oContactMenu)
            AddHandler oMenu.AfterUpdate, AddressOf refreshrequest
            retval = oMenu.Range
        End If
        Return retval
    End Function

    Private Sub refreshrequest(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
