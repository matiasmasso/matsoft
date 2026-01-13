Public Class Menu_CodiMercancia

    Inherits Menu_Base

    Private _CodiMercancias As List(Of DTOCodiMercancia)
    Private _CodiMercancia As DTOCodiMercancia

    Public Sub New(ByVal oCodiMercancias As List(Of DTOCodiMercancia))
        MyBase.New()
        _CodiMercancias = oCodiMercancias
        If _CodiMercancias IsNot Nothing Then
            If _CodiMercancias.Count > 0 Then
                _CodiMercancia = _CodiMercancias.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oCodiMercancia As DTOCodiMercancia)
        MyBase.New()
        _CodiMercancia = oCodiMercancia
        _CodiMercancias = New List(Of DTOCodiMercancia)
        If _CodiMercancia IsNot Nothing Then
            _CodiMercancias.Add(_CodiMercancia)
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
        oMenuItem.Enabled = _CodiMercancias.Count = 1
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
        Dim oFrm As New Frm_CodiMercancia(_CodiMercancia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.CodiMercancia.Delete(_CodiMercancias.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


