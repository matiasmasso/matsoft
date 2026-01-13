Public Class Xl_LookupBank

    Inherits Xl_LookupTextboxButton

    Private _Bank As DTOBank

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Bank() As DTOBank
        Get
            Return _Bank
        End Get
        Set(ByVal value As DTOBank)
            _Bank = value
            If _Bank Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Bank.RaoSocial
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Bank = Nothing
    End Sub

    Private Sub Xl_LookupBank_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Banks(_Bank, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.OnItemSelected, AddressOf onBankSelected
        oFrm.Show()
    End Sub

    Private Sub onBankSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Bank = e.Argument
        MyBase.Text = _Bank.RaoSocial
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class

