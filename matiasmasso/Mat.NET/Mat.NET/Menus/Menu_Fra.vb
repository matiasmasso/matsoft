
Imports Microsoft.Office.Interop

Public Class Menu_Fra

    Private mFras As Fras

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Progress(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oFra As Fra)
        MyBase.New()
        mFras = New Fras
        mFras.Add(oFra)
    End Sub

    Public Sub New(ByVal oFras As Fras)
        MyBase.New()
        mFras = oFras
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Del(), _
        MenuItem_Print(), _
        MenuItem_eFra(), _
        MenuItem_eMailAll(), _
        MenuItem_Pdf(), _
        MenuItem_CopyLink(), _
        MenuItem_Advance() _
        })
        'MenuItem_eMail(), _
    End Function


        '==========================================================================
        '                            MENU ITEMS BUILDER
        '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = mFras.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function

    Private Function MenuItem_Print() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Imprimir"
        oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_Print
        Return oMenuItem
    End Function

    Private Function MenuItem_eFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "enviar e-factura"
        For Each oFra As Fra In mFras
            If oFra.Client IsNot Nothing Then
                If Not oFra.Client.EfrasEnabled Then
                    oMenuItem.Enabled = False
                    Exit For
                End If
            End If
        Next
        oMenuItem.Image = My.Resources.MailSobreGroc
        AddHandler oMenuItem.Click, AddressOf Do_eFra
        Return oMenuItem
    End Function

    Private Function MenuItem_eMail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "e-mail"
        oMenuItem.Image = My.Resources.printer
        oMenuItem.Enabled = mFras.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_eMail
        Return oMenuItem
    End Function

    Private Function MenuItem_eMailAll() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "e-mail a autoritzats"
        oMenuItem.Image = My.Resources.printer
        oMenuItem.Enabled = mFras.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_eMailAll
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        oMenuItem.Enabled = mFras.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Advance() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avançades"
        oMenuItem.DropDownItems.Add(SubMenuItem_PdfRegenerate)
        Return oMenuItem
    End Function

    Private Function SubMenuItem_PdfRegenerate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_PdfRegenerate
        oMenuItem.Text = "regenera pdf"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Html() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        oMenuItem.Enabled = mFras.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Html
        oMenuItem.Text = "regenera pdf"
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Fra(mFras(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & mFras.ToString() & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim i As Integer
            Dim exs as New List(Of exception)
            RaiseEvent Progress(Nothing, New ProgressEventArgs(0, mFras.Count, ProgressEventArgs.Codis.Start))
            For Each Itm As Fra In mFras
                i += 1
                RaiseEvent Progress(Nothing, New ProgressEventArgs(i, mFras.Count, ProgressEventArgs.Codis.Increment, "eliminant fra." & Itm.Id))
                If Not Itm.Delete( exs) Then Exit For
            Next


            Dim sText As String
            Select Case i
                Case 0
                    sText = "no s'ha eliminat cap factura"
                Case 1
                    sText = i & " factura eliminada"
                Case Else
                    sText = i & " factures eliminades"
            End Select

            If exs.Count > 0 Then
                sText = "Error: " & sText & vbCrLf & BLL.Defaults.ExsToMultiline(exs)
                MsgBox(sText, MsgBoxStyle.Exclamation)
            Else
                MsgBox(sText, MsgBoxStyle.Information, "M+O")
            End If

            RaiseEvent Progress(Nothing, New ProgressEventArgs(i, mFras.Count, ProgressEventArgs.Codis.Stop))
            RaiseEvent AfterUpdate(Me, New System.EventArgs)
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_Print(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintFras(mFras)
    End Sub

    Private Sub Do_eFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFra As Fra
        For Each oFra In mFras
            root.Email_Efra(oFra, Now)
        Next
    End Sub

    Private Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sGuid As String = mFras(0).Guid.ToString
        If mFras.Count = 1 Then
            Dim oFra As Fra = mFras(0)
            Dim oDocFile As DTODocFile = BLL.BLLDocFile.FromHash(oFra.Cca.DocFile.Hash, True)
            UIHelper.ShowStream(oDocFile)
        Else
            root.ShowPdf(mFras.PdfStream(True))
        End If
    End Sub

    Private Sub Do_PdfRegenerate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)

        For Each oFra As Fra In mFras

            Dim oPdf As New PdfAlb(oFra)

            Dim oDocFile As DTODocFile = Nothing
            If BLL_DocFile.LoadFromStream(oPdf.Stream(True), oDocFile, exs) Then
                oDocFile.PendingOp = DTODocFile.PendingOps.Update

                Dim oCca As Cca = oFra.Cca
                oCca.SetItm()
                oCca.DocFile = oDocFile

                If Not oCca.Update(exs) Then
                    MsgBox("error al regenerar pdf " & oFra.FullConcept & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If

        Next

        RefreshRequest(sender, e)
        MsgBox("pdf de factures regenerats", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub Do_Html(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sGuid As String = mFras(0).Guid.ToString
        If mFras.Count = 1 Then
            UIHelper.ShowStream(mFras(0).Cca.DocFile)
        Else
            'UIHelper.ShowHtml(mFras.PdfStream(True))
        End If
    End Sub


    Private Sub Do_eMail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sSubject As String = mFras.ToString.ToUpper
        Dim sDisplayName As String = "M+O " & sSubject & ".pdf"
        Dim sFileName As String = mFras(0).FileName

        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            If mFras.SameClient Then
                If Not mFras(0).Client.Email Is Nothing Then
                    .Recipients.Add(mFras(0).Client.Email)
                End If
            End If
            .Subject = sSubject
            .Attachments.Add(sFileName, , , sDisplayName)
            .Display()
        End With
    End Sub

    Private Sub Do_eMailAll(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim s As String = ""
        Dim oEmails As Emails = mFras(0).Client.eFrasMailboxRecipients
        If oEmails.Count = 0 Then
            If mFras(0).Client.Emails.Count = 0 Then
                s = "No consta cap adreça de e-mail registrada en aquest client"
            Else
                s = "Aquest client té adreçes de e-mail registrades, pero cap consta com autoritzada per enviar factures."
            End If
            MsgBox(s, MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            s = "Confirmar envío de la factura " & mFras(0).Id & " als següents destinataris de " & mFras(0).Client.Nom & ":" & vbCrLf
            For Each oEmail As Email In oEmails
                s = s & oEmail.Adr & vbCrLf
            Next
            Dim rc As MsgBoxResult = MsgBox(s, MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim oFchEmailed As Date = Now
                Dim BlSuccess As Boolean = True
                For Each oFra As Fra In mFras
                    If Not root.Email_Efra(oFra, oFchEmailed) Then BlSuccess = False
                Next
                If BlSuccess Then MsgBox("factures enviades correctament", MsgBoxStyle.Information, "MAT.NET")
            Else
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = BLL.BLLDocFile.DownloadUrl(mFras(0).Cca.DocFile, True)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
