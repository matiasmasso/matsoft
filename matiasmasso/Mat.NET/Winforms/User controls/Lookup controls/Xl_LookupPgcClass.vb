Public Class Xl_LookupPgcClass

    Inherits Xl_LookupTextboxButton

    Private _PgcClass As DTOPgcClass

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property PgcClass() As DTOPgcClass
        Get
            Return _PgcClass
        End Get
        Set(ByVal value As DTOPgcClass)
            _PgcClass = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.PgcClass = Nothing
    End Sub

    Private Sub Xl_LookupPgcClass_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _PgcClass IsNot Nothing Then
            If Not FEB2.PgcClass.Load(_PgcClass, exs) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If
        Dim oFrm As New Frm_PgcClasses(_PgcClass, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onPgcClassSelected
        oFrm.Show()
    End Sub

    Private Sub onPgcClassSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PgcClass = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _PgcClass Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _PgcClass.Nom.Tradueix(MyBase.Lang)
            Dim oMenu_PgcClass As New Menu_PgcClass(_PgcClass)
            AddHandler oMenu_PgcClass.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_PgcClass.Range)
        End If
    End Sub

End Class

