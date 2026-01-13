Public Class Xl_LookupSermepaConfig

    Inherits Xl_LookupTextboxButton

    Private _SermepaConfig As DTOSermepaConfig

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property SermepaConfig() As DTOSermepaConfig
        Get
            Return _SermepaConfig
        End Get
        Set(ByVal value As DTOSermepaConfig)
            _SermepaConfig = value
            If _SermepaConfig Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _SermepaConfig.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.SermepaConfig = Nothing
    End Sub

    Private Sub Xl_LookupSermepaConfig_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_SermepaConfigs(_SermepaConfig, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onSermepaConfigSelected
        oFrm.Show()
    End Sub

    Private Sub onSermepaConfigSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _SermepaConfig = e.Argument
        MyBase.Text = _SermepaConfig.Nom
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Xl_LookupSermepaConfig_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_SermepaConfig(_SermepaConfig)
        AddHandler oFrm.AfterUpdate, AddressOf onAfterUpdate
        oFrm.Show()
    End Sub

    Private Sub OnAfterUpdate(sender As Object, e As MatEventArgs)
        _SermepaConfig = e.Argument
        If _SermepaConfig Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = _SermepaConfig.Nom
        End If
    End Sub
End Class
