

Public Class Menu_RepLiq
    Private _RepLiqs As List(Of DTORepLiq)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRepLiq As DTORepLiq)
        MyBase.New()
        _RepLiqs = New List(Of DTORepLiq)
        _RepLiqs.Add(oRepLiq)
    End Sub

    Public Sub New(ByVal oRepLiqs As List(Of DTORepLiq))
        MyBase.New()
        _RepLiqs = oRepLiqs
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Pdf(), _
        MenuItem_Advanced(), _
        MenuItem_Del()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = (_RepLiqs.Count = 1)
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avançats"
        oMenuItem.DropDownItems.Add(MenuItem_Cca)
        oMenuItem.DropDownItems.Add(MenuItem_regeneraPdf)
        Return oMenuItem
    End Function

    Private Function MenuItem_Cca() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "assentament"
        'oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = (_RepLiqs.Count = 1)
        AddHandler oMenuItem.Click, AddressOf Do_Cca
        Return oMenuItem
    End Function

    Private Function MenuItem_regeneraPdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "regenera Pdf"
        'oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = (_RepLiqs.Count = 1)
        AddHandler oMenuItem.Click, AddressOf reDo_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepLiq(_RepLiqs(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("retrocedim " & _RepLiqs.Count & " liquidacións seleccionades?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL_RepLiqs.Delete(_RepLiqs, exs) Then
                MsgBox(_RepLiqs.Count & " liquidacións retrocedides", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_Cca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRepLiq As DTORepLiq = _RepLiqs(0)
        Dim oCca As DTOCca = oRepLiq.Cca
        Dim oFrm As New Frm_Cca(oCca)
        oFrm.Show()
    End Sub

    Private Sub reDo_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRepLiq As DTORepLiq = _RepLiqs(0)
        Dim oCca As DTOCca = oRepLiq.Cca
        Dim oPdfRepLiq As New PdfRepLiq(oRepLiq)

        Dim exs As New List(Of exception)
        Dim oDocFile As DTODocFile = Nothing
        If BLL_DocFile.LoadFromStream(oPdfRepLiq.Stream, oDocFile, exs) Then
            oCca.DocFile = oDocFile
            oCca.DocFile.PendingOp = DTODocFile.PendingOps.Update
            If BLL.BLLCca.Update(oCca, exs) Then
                RaiseEvent AfterUpdate(oRepLiq, EventArgs.Empty)
                MsgBox("pdf regenerat", MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs, "error al desar el pdf regenerat")
            End If
        Else
            UIHelper.WarnError(exs, "error al redactar i signar el pdf de la factura")
        End If
    End Sub

    Private Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = _RepLiqs(0).Cca
        If oCca IsNot Nothing Then
            UIHelper.ShowStream(oCca.DocFile)
        Else
            MsgBox("aquesta liquidació no está lligada amb cap assentament comptable", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
