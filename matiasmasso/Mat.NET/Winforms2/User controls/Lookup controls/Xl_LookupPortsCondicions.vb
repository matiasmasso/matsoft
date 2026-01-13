Public Class Xl_LookupPortsCondicions
    Inherits Xl_LookupTextboxButton

    Private _PortsCondicio As DTOPortsCondicio

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows ReadOnly Property PortsCondicio() As DTOPortsCondicio
        Get
            Return _PortsCondicio
        End Get
    End Property

    Public Shadows Sub Load(oPortsCondicio As DTOPortsCondicio)
        _PortsCondicio = oPortsCondicio
        refresca()
    End Sub

    Public Sub Clear()
        _PortsCondicio = Nothing
        refresca()
    End Sub

    Private Sub Xl_LookupPortsCondicio_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        Dim oFrm As New Frm_PortsCondicions(_PortsCondicio, DTO.Defaults.SelectionModes.selection)
        AddHandler oFrm.itemSelected, AddressOf onPortsCondicioSelected
        oFrm.Show()
    End Sub

    Private Sub onPortsCondicioSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PortsCondicio = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _PortsCondicio Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _PortsCondicio.Nom
            Dim oMenu_PortsCondicio As New Menu_PortsCondicio(_PortsCondicio)
            AddHandler oMenu_PortsCondicio.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_PortsCondicio.Range)
        End If
    End Sub


End Class

