Public Class Xl_LookupRep
    Inherits Xl_LookupTextboxButton

    Private _Rep As DTORep

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Rep() As DTORep
        Get
            Return _Rep
        End Get
        Set(ByVal value As DTORep)
            _Rep = value
            If _Rep Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Rep.NicknameOrNom()
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Rep = Nothing
    End Sub

    Private Sub Xl_LookupRep_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Reps(_Rep, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onRepSelected
        oFrm.Show()
    End Sub

    Private Sub onRepSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Rep = e.Argument
        MyBase.Text = _Rep.NickName
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
