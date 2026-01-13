Public Class Xl_LookupPremiumLine

    Inherits Xl_LookupTextboxButton

    Private _PremiumLine As DTOPremiumLine

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property PremiumLine() As DTOPremiumLine
        Get
            Return _PremiumLine
        End Get
        Set(ByVal value As DTOPremiumLine)
            _PremiumLine = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.PremiumLine = Nothing
    End Sub

    Private Sub Xl_LookupPremiumLine_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_PremiumLines(_PremiumLine, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.ItemSelected, AddressOf onPremiumLineSelected
        oFrm.Show()
    End Sub

    Private Sub onPremiumLineSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PremiumLine = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _PremiumLine Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _PremiumLine.Nom
            Dim oMenu_PremiumLine As New Menu_PremiumLine(_PremiumLine)
            AddHandler oMenu_PremiumLine.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_PremiumLine.Range)
        End If
    End Sub


End Class



