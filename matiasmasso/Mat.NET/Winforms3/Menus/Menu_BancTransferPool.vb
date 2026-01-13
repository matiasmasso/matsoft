Public Class Menu_BancTransferPool

    Inherits Menu_Base

    Private _BancTransferPools As List(Of DTOBancTransferPool)
    Private _BancTransferPool As DTOBancTransferPool


    Public Sub New(ByVal oBancTransferPools As List(Of DTOBancTransferPool))
        MyBase.New()
        _BancTransferPools = oBancTransferPools
        If _BancTransferPools IsNot Nothing Then
            If _BancTransferPools.Count > 0 Then
                _BancTransferPool = _BancTransferPools.First
            End If
        End If
    End Sub

    Public Sub New(ByVal oBancTransferPool As DTOBancTransferPool)
        MyBase.New()
        _BancTransferPool = oBancTransferPool
        _BancTransferPools = New List(Of DTOBancTransferPool)
        If _BancTransferPool IsNot Nothing Then
            _BancTransferPools.Add(_BancTransferPool)
        End If
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_SepaCreditTransferFile(),
        MenuItem_RemittanceAdvice(),
        MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _BancTransferPools.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_SepaCreditTransferFile() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer Sepa Credit Transfer (Q34-14)"
        oMenuItem.Enabled = _BancTransferPools.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SepaCreditTransferFile
        Return oMenuItem
    End Function

    Private Function MenuItem_RemittanceAdvice() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email de notificació"
        oMenuItem.Enabled = _BancTransferPools.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_RemittanceAdvice
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Enabled = _BancTransferPools.Count = 1
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BancTransferPool(_BancTransferPool)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_SepaCreditTransferFile()
        Dim exs As New List(Of Exception)
        If FEB.BancTransferPool.Load(_BancTransferPool, exs) Then
            Dim sFilename As String = _BancTransferPools.First.DefaultFilename()
            Dim XMLSource As String = Await FEB.SepaCreditTransfer.XML(Current.Session.Emp, _BancTransferPools.First, exs)
            If exs.Count = 0 Then
                UIHelper.SaveXmlFileDialog(XMLSource, sFilename)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_RemittanceAdvice()
        Dim exs As New List(Of Exception)
        If Not Await MatOutlook.RemittanceAdvice(_BancTransferPool.Beneficiaris.First, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.BancTransferPool.Delete(_BancTransferPool, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar la transferencia")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


