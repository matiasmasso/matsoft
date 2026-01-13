Public Class Menu_JsonSchema

    Inherits Menu_Base

    Private _JsonSchemas As List(Of DTOJsonSchema)
    Private _JsonSchema As DTOJsonSchema

    Public Sub New(ByVal oJsonSchemas As List(Of DTOJsonSchema))
        MyBase.New()
        _JsonSchemas = oJsonSchemas
        If _JsonSchemas IsNot Nothing Then
            If _JsonSchemas.Count > 0 Then
                _JsonSchema = _JsonSchemas.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oJsonSchema As DTOJsonSchema)
        MyBase.New()
        _JsonSchema = oJsonSchema
        _JsonSchemas = New List(Of DTOJsonSchema)
        If _JsonSchema IsNot Nothing Then
            _JsonSchemas.Add(_JsonSchema)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _JsonSchemas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_JsonSchema(_JsonSchema)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.JsonSchema.Delete(_JsonSchemas.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

