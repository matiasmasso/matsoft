Public Class Xl_LookupVisaCard
    Inherits Xl_LookupTextboxButton

    Private _VisaCard As DTOVisaCard

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property VisaCard() As DTOVisaCard
        Get
            Return _VisaCard
        End Get
        Set(ByVal value As DTOVisaCard)
            _VisaCard = value
            If _VisaCard Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _VisaCard.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.VisaCard = Nothing
    End Sub

    Private Sub Xl_LookupVisaCard_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_VisaCards(_VisaCard, BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onVisaCardSelected
        oFrm.Show()
    End Sub

    Private Sub onVisaCardSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _VisaCard = e.Argument
        MyBase.Text = _VisaCard.Nom & " " & Microsoft.VisualBasic.Right(_VisaCard.Digits, 4)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class