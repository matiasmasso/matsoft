Public Class Xl_LookupEvento
    Inherits Xl_LookupTextboxButton

    Private _EventoValue As DTOEvento

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property EventoValue() As DTOEvento
        Get
            Return _EventoValue
        End Get
        Set(ByVal value As DTOEvento)
            _EventoValue = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.EventoValue = Nothing
    End Sub

    Private Sub Xl_LookupEvento_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Events(_EventoValue, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onEventoSelected
        oFrm.Show()
    End Sub

    Private Sub onEventoSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _EventoValue = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _EventoValue Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _EventoValue.Title.Tradueix(Current.Session.Lang)
            Dim oMenu_Evento As New Menu_Noticia(_EventoValue)
            AddHandler oMenu_Evento.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Evento.Range)
        End If
    End Sub

End Class
