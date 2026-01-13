Public Class Xl_LookupEscriptura

    Inherits Xl_LookupTextboxButton

    Private _Escriptura As DTOEscriptura

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Escriptura() As DTOEscriptura
        Get
            Return _Escriptura
        End Get
        Set(ByVal value As DTOEscriptura)
            _Escriptura = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Escriptura = Nothing
    End Sub

    Private Sub Xl_LookupEscriptura_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        ' Dim oFrm As New Frm_Escriptures(DTO.Defaults.SelectionModes.Selection)
        ' AddHandler oFrm.onItemSelected, AddressOf onEscripturaSelected
        ' oFrm.Show()
    End Sub

    Private Sub onEscripturaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Escriptura = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Escriptura Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = Format(_Escriptura.FchFrom, "dd/MM/yy") & ": " & _Escriptura.Nom
            Dim oMenu_Escriptura As New Menu_Escriptura(_Escriptura)
            AddHandler oMenu_Escriptura.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Escriptura.Range)
        End If
    End Sub


End Class

