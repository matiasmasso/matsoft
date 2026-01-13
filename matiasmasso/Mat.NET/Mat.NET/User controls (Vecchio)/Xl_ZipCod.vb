Public Class Xl_ZipCod
    Private _Country As DTOCountry

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oCountry As DTOCountry, sZipCod As String)
        _Country = oCountry
        Me.ZipCod = sZipCod
    End Sub

    Public WriteOnly Property Country As DTOCountry
        Set(value As DTOCountry)
            _Country = value
        End Set
    End Property

    Public Property ZipCod As String
        Get
            Return TextBoxZipCod.Text
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                TextBoxZipCod.Text = value.Trim
                refresca()
            End If
        End Set
    End Property


    Private Sub TextBoxZipCod_TextChanged(sender As Object, e As EventArgs) Handles TextBoxZipCod.TextChanged
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.ZipCod))
        refresca()
    End Sub

    Private Sub refresca()
        Dim oZip As DTOZip = Nothing
        If _Country IsNot Nothing Then
            oZip = BLL_App.Zip(_Country, TextBoxZipCod.Text)
            If oZip Is Nothing Then
                TextBoxLocation.Clear()
            Else
                TextBoxLocation.Text = oZip.Location.Nom
            End If
        End If

    End Sub
End Class
