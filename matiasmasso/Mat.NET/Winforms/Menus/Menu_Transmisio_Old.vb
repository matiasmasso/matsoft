

Public Class Menu_Transmisio_Old
    Private mTransmisio As Transmisio

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oTransmisio As Transmisio)
        MyBase.New()
        mTransmisio = oTransmisio
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Excel(),
        MenuItem_RollBack(),
        MenuItem_GetXML(),
        MenuItem_GetAlbs(),
        MenuItem_SendOutlook(),
        MenuItem_SendExchange()
        })
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

    Private Function MenuItem_RollBack() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir"
        oMenuItem.Image = My.Resources.del
        'oMenuItem.Enabled = mTransmisions(0).AllowDelete
        AddHandler oMenuItem.Click, AddressOf Do_RollBack
        Return oMenuItem
    End Function

    Private Function MenuItem_GetXML() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer de dades"
        oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_GetXML
        Return oMenuItem
    End Function

    Private Function MenuItem_GetAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "albarans"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_GetAlbs
        Return oMenuItem
    End Function


    Private Function MenuItem_SendOutlook() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar per Outlook"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_SendOutlook
        Return oMenuItem
    End Function

    Private Function MenuItem_SendExchange() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar per Exchange"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_SendExchange
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================


    Private Sub Do_Zoom(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oTransmisio As New DTOTransmisio(mTransmisio.Guid)
        Dim oFrm As New Frm_Transmisio(oTransmisio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_RollBack(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("retrocedim transmisió " & mTransmisio.Id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If TransmisioLoader.Delete(mTransmisio, exs) Then
                RaiseEvent AfterUpdate(Me, New System.EventArgs)
                MsgBox("transmisió retrocedida", MsgBoxStyle.Information, "MAT.NET")
            Else
                UIHelper.WarnError(exs, "error al retrocedir la transmisio " & mTransmisio.Id)
            End If
        Else
            MsgBox("operació cancelada", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_GetXML(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oTransmisio As DTOTransmisio = BLL.BLLTransmisio.Find(mTransmisio.Guid)
        Dim oDlg As New System.Windows.Forms.SaveFileDialog
        With oDlg
            .FileName = "transmisio " & mTransmisio.Id
            .DefaultExt = "xml"
            .Filter = "xml files (*.xml)|*.xml"
            .FilterIndex = 1
            If .ShowDialog() = DialogResult.OK Then
                mTransmisio.MakeFileXml(.FileName)
                'My.Computer.FileSystem.CopyFile(mTransmisio.PathToDades, .FileName)
            End If
        End With
    End Sub


    Private Sub Do_GetAlbs(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oAlbs As Albs = ECI.SortAlbs(mTransmisio.Albs)
        'root.ShowPdf(mAlbs.PdfStream(BlProforma))
        root.ShowPdf(oAlbs.PdfStream())
    End Sub

    Private Sub Do_SendOutlook(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oTransmisio As DTOTransmisio = BLLTransmisio.Find(mTransmisio.Guid)
        With mTransmisio
            If Not MatOutlook.NewMessage(BLLTransmisio.SendTo, "", , BLLTransmisio.SendSubject(oTransmisio), , BLLTransmisio.SendUrlBody(oTransmisio), mTransmisio.SendAttachments(), exs) Then
                UIHelper.WarnError(exs, "error al redactar missatge")
            End If
        End With
    End Sub

    Private Sub Do_SendExchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sErr As String = ""
        Dim oTransmisio As DTOTransmisio = BLLTransmisio.Find(mTransmisio.Guid)
        If mTransmisio.Send() Then
            MsgBox("Transmisió " & mTransmisio.Id & " enviada al magatzem", MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("Operació no realitzada:" & vbCrLf & sErr, MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_Excel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim SQL As String = "SELECT ALB.alb, ALB.nom, " _
        & "(CASE WHEN CASHCOD =" & CInt(DTO.DTOCustomer.CashCodes.Reembols).ToString & " THEN (ALB.Pts + ALB.Pt2) ELSE 0 END) AS REEMBOLSO, " _
        & "(CASE WHEN PORTSCOD = " & CInt(DTO.DTOCustomer.PortsCodes.Reculliran).ToString & " THEN '(recogerán)' ELSE TRP.abr END) AS TRANSPORT " _
        & "FROM ALB LEFT OUTER JOIN " _
        & "TRP ON ALB.TrpGuid = TRP.Guid " _
        & "WHERE ALB.Emp =" & mTransmisio.Mgz.Emp.Id & " AND " _
        & "ALB.yea_transm =" & mTransmisio.Yea & " AND " _
        & "ALB.transm =" & mTransmisio.Id & " " _
        & "ORDER BY ALB.alb"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        MatExcel.GetExcelFromDataset(oDs).Visible = True
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
