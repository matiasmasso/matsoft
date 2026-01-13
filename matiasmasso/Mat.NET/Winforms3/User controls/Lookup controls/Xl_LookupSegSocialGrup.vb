Public Class Xl_LookupSegSocialGrup
    Inherits Xl_LookupTextboxButton

    Private _SegSocialGrup As DTOSegSocialGrup

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oSegSocialGrup As DTOSegSocialGrup)
        _SegSocialGrup = oSegSocialGrup
        Refresca()
    End Sub

    Public ReadOnly Property SegSocialGrup() As DTOSegSocialGrup
        Get
            Return _SegSocialGrup
        End Get
    End Property


    Public Sub Clear()
        _SegSocialGrup = Nothing
        Refresca()
    End Sub

    Private Sub Xl_LookupSegSocialGrup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_SegSocialGrup(_SegSocialGrup)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupSegSocialGrup_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_SegSocialGrups(_SegSocialGrup, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onSegSocialGrupSelected
        oFrm.Show()
    End Sub

    Private Sub onSegSocialGrupSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _SegSocialGrup = e.Argument
        RaiseEvent AfterUpdate(Me, e)
        Refresca()
    End Sub

    Private Sub Refresca()
        If _SegSocialGrup Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = _SegSocialGrup.Nom
        End If
    End Sub

End Class

