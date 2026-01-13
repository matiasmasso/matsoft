Public Class Menu_Transmisio
    Inherits Menu_Base

    Private _Transmisions As List(Of DTOTransmisio)
    Private _Transmisio As DTOTransmisio


    Public Sub New(ByVal oTransmisions As IEnumerable(Of DTOTransmisio))
        MyBase.New()
        _Transmisions = oTransmisions.ToList
        If _Transmisions IsNot Nothing Then
            If _Transmisions.Count > 0 Then
                _Transmisio = _Transmisions.First
            End If
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Excel())
        MyBase.AddMenuItem(MenuItem_RollBack())
        MyBase.AddMenuItem(MenuItem_GetXML())
        MyBase.AddMenuItem(MenuItem_GetAlbs())
        MyBase.AddMenuItem(MenuItem_EmailBody())
        MyBase.AddMenuItem(MenuItem_SendExchange())
        MyBase.AddMenuItem(MenuItem_SendOutlook())
        MyBase.AddMenuItem(MenuItem_Facturar())
        MyBase.AddMenuItem(MenuItem_ExcelEci())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Enabled = _Transmisions.Count = 1
        oMenuItem.Text = "Zoom"
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_RollBack() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir"
        oMenuItem.Enabled = _Transmisions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_RollBack
        Return oMenuItem
    End Function

    Private Function MenuItem_GetXML() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer de dades"
        oMenuItem.Image = My.Resources.save_16
        oMenuItem.Enabled = _Transmisions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_GetXML
        Return oMenuItem
    End Function

    Private Function MenuItem_GetAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "albarans"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = _Transmisions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_GetAlbs
        Return oMenuItem
    End Function

    Private Function MenuItem_EmailBody() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "missatge"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = _Transmisions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_EmailBody
        Return oMenuItem
    End Function

    Private Function MenuItem_SendOutlook() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar per Outlook"
        oMenuItem.Image = My.Resources.MailSobreObert
        oMenuItem.Enabled = _Transmisions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SendOutlook
        Return oMenuItem
    End Function

    Private Function MenuItem_SendExchange() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar per Exchange"
        oMenuItem.Image = My.Resources.MailSobreObert
        oMenuItem.Enabled = _Transmisions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SendExchange
        Return oMenuItem
    End Function
    Private Function MenuItem_Facturar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Facturar"
        oMenuItem.Image = My.Resources.Gears
        AddHandler oMenuItem.Click, AddressOf Do_Facturar
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        oMenuItem.Image = My.Resources.Excel
        oMenuItem.Enabled = _Transmisions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function



    Private Function MenuItem_ExcelEci() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel El Corte Ingles"
        AddHandler oMenuItem.Click, AddressOf Do_ExcelECI
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================


    Private Sub Do_Zoom(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Transmisio(_Transmisions.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_RollBack(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("retrocedim transmisió " & _Transmisio.Id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then

            Dim exs As New List(Of Exception)
            If Await FEB.Transmisio.Delete(_Transmisio, exs) Then
                RefreshRequest(Me, MatEventArgs.Empty)
                MsgBox("transmisió retrocedida", MsgBoxStyle.Information, "MAT.NET")
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("operació cancelada", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Async Sub Do_GetXML(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)

        MyBase.ToggleProgressBarRequest(True)
        Dim oByteArray = Await FEB.Transmisio.XmlFile(_Transmisio, exs)
        MyBase.ToggleProgressBarRequest(False)

        If exs.Count = 0 Then
            Dim text As String = System.Text.ASCIIEncoding.UTF8.GetString(oByteArray)
            Dim oDlg As New SaveFileDialog
            With oDlg
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                .Filter = "fitxers Xml (*.xml)|*.xml|tots els fitxers (*.*)|*.*"
                .FileName = FEB.Transmisio.FileNameDades(_Transmisio)
                .Title = "Desar fitxer de dades transmisió"
                If .ShowDialog() = DialogResult.OK Then
                    Dim objWriter As New System.IO.StreamWriter(.FileName, append:=False)
                    objWriter.Write(text)
                    objWriter.Close()
                End If
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_GetAlbs(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Dim url As String = FEB.Transmisio.UrlAlbarans(_Transmisio)
        'UIHelper.ShowHtml(url)

        Dim exs As New List(Of Exception)

        MyBase.ToggleProgressBarRequest(True)
        Dim oByteArray = Await FEB.Transmisio.PdfDeliveries(_Transmisio, exs)
        MyBase.ToggleProgressBarRequest(False)

        If exs.Count = 0 Then
            Dim oDlg As New SaveFileDialog
            With oDlg
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                .Filter = "documents Pdf (*.pdf)|*.pdf|tots els fitxers (*.*)|*.*"
                .FileName = FEB.Transmisio.FileNameDocs(_Transmisio)
                .Title = "Desar pdf de albarans de la transmisió"
                If .ShowDialog() = DialogResult.OK Then
                    System.IO.File.WriteAllBytes(.FileName, oByteArray)
                End If
            End With
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_EmailBody()
        Dim url As String = FEB.Transmisio.SendUrlBody(_Transmisio)
        UIHelper.ShowHtml(url)
    End Sub

    Private Async Sub Do_SendOutlook(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)

        MyBase.ToggleProgressBarRequest(True)
        Dim oAttachment = Await FEB.Transmisio.XmlAttachment(_Transmisio, exs)
        MyBase.ToggleProgressBarRequest(False)

        If exs.Count = 0 Then
            Dim sendTo = Await FEB.Transmisio.SendTo(exs)
            If exs.Count = 0 Then
                With _Transmisio
                    Dim oMailMessage = DTOMailMessage.Factory(sendTo)
                    With oMailMessage
                        .Subject = FEB.Transmisio.SendSubject(_Transmisio)
                        .BodyUrl = FEB.Transmisio.SendUrlBody(_Transmisio)
                        .Attachments.Add(oAttachment)
                    End With

                    If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                End With
            Else
                UIHelper.WarnError(exs, "Error al llegir l'adreça del destinatari")
            End If
        Else
            UIHelper.WarnError(exs, "Error al generar el fitxer de dades")
        End If
    End Sub

    Private Async Sub Do_SendExchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        If Await FEB.Transmisio.Send(exs, _Transmisio) Then
            MyBase.ToggleProgressBarRequest(False)
            MsgBox("Transmisió " & _Transmisio.id & " enviada al magatzem", MsgBoxStyle.Information, "MAT.NET")
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Facturar()
        Dim oFrm As New Frm_Facturacio(_Transmisions)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Excel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        Dim oBook As MatHelper.Excel.Book = Await FEB.Transmisio.Excel(_Transmisio, exs)
        MyBase.ToggleProgressBarRequest(False)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oBook, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ExcelECI(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        For i As Integer = 0 To _Transmisions.Count - 1
            _Transmisions(i) = Await FEB.Transmisio.Find(_Transmisions(i).Guid, exs)
        Next
        Dim oDeliveries = _Transmisions.SelectMany(Function(x) x.deliveries).ToList()
        For i = 0 To oDeliveries.Count - 1
            oDeliveries(i) = Await FEB.Delivery.Find(oDeliveries(i).Guid, exs)
            For Each item In oDeliveries(i).items
                item.delivery = oDeliveries(i)
            Next
        Next
        MyBase.ToggleProgressBarRequest(False)
        Dim oFrm As New Frm_Eci_PlantillaModif(oDeliveries)
        oFrm.Show()
    End Sub



End Class
