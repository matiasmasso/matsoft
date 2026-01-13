

Public Class Menu_Importacio
    Private _Importacio As DTOImportacio
    Private _Importacions As List(Of DTOImportacio)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oImportacions As List(Of DTOImportacio))
        MyBase.New()
        _Importacions = oImportacions
        If _Importacions.Count > 0 Then
            _Importacio = _Importacions.First
        End If
    End Sub

    Public Sub New(ByVal oImportacio As DTOImportacio)
        MyBase.New()
        _Importacio = oImportacio
        _Importacions = New List(Of DTOImportacio)
        _Importacions.Add(_Importacio)
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_PrevisioSkus(),
        MenuItem_EntradaOk(),
        MenuItem_NewEntrada(),
        MenuItem_NewImportacio(),
        MenuItem_NewFra(),
        MenuItem_ExcelRemeses(),
        MenuItem_MailOrdreDeCarrega(),
        MenuItem_ExcelGoods(),
        MenuItem_ImportValidation(),
        MenuItem_DiscrepancyReport(),
        MenuItem_Advanced(),
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

    Private Function MenuItem_PrevisioSkus() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Qué es lo que arriba?"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_PrevisioSkus
        Return oMenuItem
    End Function

    Private Function MenuItem_EntradaOk() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Entrar tot Ok"
        oMenuItem.Image = My.Resources.vb
        AddHandler oMenuItem.Click, AddressOf Do_EntrarTotOk
        Return oMenuItem
    End Function


    Private Function MenuItem_NewEntrada() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "albará d'entrada"
        oMenuItem.Image = My.Resources.notepad
        AddHandler oMenuItem.Click, AddressOf Do_NewEntrada
        Return oMenuItem
    End Function

    Private Function MenuItem_NewFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "entrada factura"
        oMenuItem.Visible = False 'tret perque per aqui no ho assigna a la remesa. Cal arrossegar el PDF a la remesa corresponent del llistat de remeses de importacio per posar en marxa el proces
        oMenuItem.Image = My.Resources.NewDoc
        AddHandler oMenuItem.Click, AddressOf Do_NewFra
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelRemeses() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel remeses"
        AddHandler oMenuItem.Click, AddressOf Do_ExcelRemeses
        Return oMenuItem
    End Function

    Private Function MenuItem_MailOrdreDeCarrega() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email Ordre de Càrrega"
        AddHandler oMenuItem.Click, AddressOf Do_MailOrdreDeCarrega
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelGoods() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel mercancía"
        AddHandler oMenuItem.Click, AddressOf Do_ExcelGoods
        Return oMenuItem
    End Function

    Private Function MenuItem_ImportValidation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar XML validació"
        AddHandler oMenuItem.Click, AddressOf Do_ImportValidation
        Return oMenuItem
    End Function


    Private Function MenuItem_NewImportacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If _Importacio.proveidor Is Nothing Then
            oMenuItem.Text = "Nova importacio"
        Else
            oMenuItem.Text = "Nova importacio de " & _Importacio.proveidor.NomComercialOrDefault()
        End If
        AddHandler oMenuItem.Click, AddressOf Do_NewImportacio
        Return oMenuItem
    End Function

    Private Function MenuItem_DiscrepancyReport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Discrepancy report"
        AddHandler oMenuItem.Click, AddressOf Do_DiscrepancyReport
        Return oMenuItem
    End Function

    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançats"
        oMenuItem.DropDownItems.Add(MenuItem_RevertPrevisions)
        oMenuItem.DropDownItems.Add(MenuItem_Unconfirm)
        oMenuItem.DropDownItems.Add(MenuItem_FakeConfirmation)
        Return oMenuItem
    End Function

    Private Function MenuItem_FakeConfirmation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer simulacre confirmació magatzem"
        AddHandler oMenuItem.Click, AddressOf Do_FakeConfirmation
        Return oMenuItem
    End Function

    Private Function MenuItem_RevertPrevisions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir previsions"
        AddHandler oMenuItem.Click, AddressOf Do_RevertPrevisions
        Return oMenuItem
    End Function

    Private Function MenuItem_Unconfirm() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir entrada"
        AddHandler oMenuItem.Click, AddressOf Do_Unconfirm
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
        Dim oFrm As New Frm_Importacio(_Importacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_PrevisioSkus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.Importacio.Load(exs, _Importacio) Then
            Dim oFrm As New Frm_Importacio(_Importacio, Frm_Importacio.Tabs.Previsio)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_EntrarTotOk() '93
        Dim exs As New List(Of Exception)
        If FEB.Importacio.Load(exs, _Importacio) Then
            Dim title = String.Format("Entrada remesa {0}", _Importacio.id)
            Dim msg As String = "Confirmem que ha entrat tota la mercancia facturada d'aquesta remesa i fem la entrada sense esperar el fitxer de confirmació?"
            Dim rc = MsgBox(msg, MsgBoxStyle.OkCancel, title)
            If rc = MsgBoxResult.Ok Then
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
                Dim oConfirmation = DTOImportacio.Confirmation.Factory(exs, _Importacio, Current.Session.User)
                If Await FEB.Importacio.Confirm(exs, oConfirmation, AddressOf ShowProgress) Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs())
                    RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                    MsgBox(String.Format("remesa {0} entrada correctament", _Importacio.id))
                Else
                    RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem remesa " & _Importacio.id & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then

            Dim exs As New List(Of Exception)
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
            If Await FEB.Importacio.Delete(_Importacio, exs) Then
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                MsgBox("remesa " & _Importacio.id & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                MsgBox("error al eliminar el document de la remesa" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If

        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub Do_NewEntrada(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = _Importacio.proveidor
        If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oContact, DTOAlbBloqueig.Codis.ALB, exs) Then
            Dim oFrm As New Frm_AlbNew2(_Importacio)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Wz_Proveidor_NewFra(_Importacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_NewImportacio(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If _Importacio.proveidor IsNot Nothing Then FEB.Proveidor.Load(_Importacio.proveidor, exs)
        Dim oImportacio = DTOImportacio.Factory(GlobalVariables.Emp, _Importacio.proveidor)
        Dim oFrm As New Frm_Importacio(oImportacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ExcelRemeses(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSheet = DTOImportacio.Excel(_Importacions, Current.Session.Lang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_MailOrdreDeCarrega()
        Dim exs As New List(Of Exception)
        Dim oMailMessage = Await FEB.Importacio.MailMessageOrdenDeCarga(exs, Current.Session.Emp, _Importacio)
        If exs.Count = 0 Then
            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Do_ExcelGoods(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB.Importacio.ExcelGoods(exs, _Importacio, Current.Session.Lang)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ImportValidation()
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog()
        With oDlg
            .Title = "Importar fitxer XML de Vivace amb la confirmació de la entrada"
            .Filter = "fitxers Xml|*.xml|tots els fitxers|*.*"
            If .ShowDialog Then
                Dim oStream = .OpenFile()
                Using sr As New System.IO.StreamReader(oStream)
                    Dim fileContent = sr.ReadToEnd()
                    Dim doc = XDocument.Parse(fileContent)
                    Dim oConfirmation = DTOImportacio.Confirmation.Factory(exs, doc, Current.Session.User)
                    If Await FEB.Importacio.Confirm(exs, oConfirmation, AddressOf ShowProgress) Then
                        MsgBox("Remesa confirmada")
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End Using
            End If
        End With
    End Sub

    Private Sub ShowProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        'to impement
    End Sub

    Private Async Sub Do_RevertPrevisions()
        Dim exs As New List(Of Exception)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
        If Await FEB.Importacio.RevertPrevisions(exs, _Importacio) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
        Else
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_FakeConfirmation()
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog()
        With oDlg
            .Title = "Fake Confirmation Remesa " & _Importacio.id
            .Filter = "Fitxers XML|*.xml|Tots els fitxers|*.*"
            .FileName = String.Format("Simulacre confirmacio remesa {0}", _Importacio.id)
            If .ShowDialog = DialogResult.OK Then
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
                If FEB.Importacio.Load(exs, _Importacio) Then
                    Dim XmlSrc As String = FEB.ImportPrevisions.AvisCamionXml(exs, _Importacio.previsions, fakeConfirmation:=True)
                    If exs.Count = 0 Then
                        If FileSystemHelper.SaveTextToFile(XmlSrc, .FileName, exs) Then
                            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                        Else
                            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                        UIHelper.WarnError(exs)
                    End If
                Else
                    RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                    UIHelper.WarnError(exs)
                End If

            End If
        End With
    End Sub

    Private Async Sub Do_Unconfirm()
        Dim exs As New List(Of Exception)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
        If Await FEB.Importacio.UnConfirm(exs, _Importacio) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
        Else
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_DiscrepancyReport()
        Dim exs As New List(Of Exception)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
        Dim oInvoicesReceived = Await FEB.Importacio.InvoicesReceived(exs, _Importacio)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
        If exs.Count = 0 Then
            If oInvoicesReceived.Count = 0 Then
                UIHelper.WarnError("No s'han registrat discrepancies en aquesta remesa")
            Else
                Dim oSheet = Await FEB.Importacio.DiscrepancyReport(exs, oInvoicesReceived, _Importacio)
                If exs.Count = 0 Then
                    If Not UIHelper.ShowExcel(oSheet, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    '===================================================================


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
