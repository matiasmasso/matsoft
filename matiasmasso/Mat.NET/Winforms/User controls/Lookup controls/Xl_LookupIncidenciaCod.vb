Public Class Xl_LookupIncidenciaCod
    Inherits Xl_LookupTextboxButton

    Private _Cod As DTOIncidenciaCod.cods
    Private _IncidenciaCod As DTOIncidenciaCod
    Private _DefaultText As String

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)


    Public Shadows Sub Load(oCod As DTOIncidenciaCod.cods, oValue As DTOIncidenciaCod, sDefaultText As String)
        _Cod = oCod
        _DefaultText = sDefaultText
        Me.IncidenciaCod = oValue
    End Sub
    Public Shadows Property IncidenciaCod() As DTOIncidenciaCod
        Get
            Return _IncidenciaCod
        End Get
        Set(ByVal value As DTOIncidenciaCod)
            _IncidenciaCod = value
            refresca()
        End Set
    End Property

    Private Sub refresca()
        If _IncidenciaCod Is Nothing Then
            MyBase.Text = _DefaultText
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _IncidenciaCod.Nom.Tradueix(Current.Session.Lang)
            Dim oMenu As New Menu_IncidenciaCod(_IncidenciaCod)
            MyBase.SetContextMenuRange(oMenu.Range)
        End If
    End Sub

    Public Sub Clear()
        Me.IncidenciaCod = Nothing
    End Sub

    Private Sub Xl_LookupIncidenciaCod_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_IncidenciesCods(_Cod, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onIncidenciaCodSelected
        oFrm.Show()
    End Sub

    Private Sub onIncidenciaCodSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        Me.IncidenciaCod = e.Argument
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Xl_LookupIncidenciaCod_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_IncidenciaCod(_IncidenciaCod)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class


