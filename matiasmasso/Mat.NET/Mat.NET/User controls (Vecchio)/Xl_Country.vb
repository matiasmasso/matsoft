Public Class Xl_Country
    Inherits Xl_LookupTextboxButton

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Property Country As DTOCountry
        Get
            Return MyBase.Value
        End Get
        Set(value As DTOCountry)
            MyBase.Value = value
            If value Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = BLL.BLLSession.Current.Lang.Tradueix(value.Nom_Esp, value.Nom_Cat, value.Nom_Eng)
            End If
        End Set
    End Property

    Private Sub Xl_Country_onLookUpRequest(sender As Object, e As EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Countries(BLL_App.Country("ES"), bll.dEFAULTS.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Me.Country = e.Argument
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
