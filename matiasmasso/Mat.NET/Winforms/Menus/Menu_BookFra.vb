Public Class Menu_BookFra
    Inherits Menu_Base

    Private _BookFras As List(Of DTOBookFra)
    Private _BookFra As DTOBookFra
    Private _ShowProgress As ProgressBarHandler

    Public Sub New(ByVal oBookFras As List(Of DTOBookFra), Optional ShowProgress As ProgressBarHandler = Nothing)
        MyBase.New()
        _BookFras = oBookFras
        _ShowProgress = ShowProgress
        If _BookFras IsNot Nothing Then
            If _BookFras.Count > 0 Then
                _BookFra = _BookFras.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oBookFra As DTOBookFra)
        MyBase.New()
        _BookFra = oBookFra
        _BookFras = New List(Of DTOBookFra)
        If _BookFra IsNot Nothing Then
            _BookFras.Add(_BookFra)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_SendToSii_Produccion)
        'MyBase.AddMenuItem(MenuItem_SendToSii_Pruebas)
        MyBase.AddMenuItem(MenuItem_SiiLog)
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _BookFras.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_SendToSii_Produccion() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar a Hisenda"
        If _BookFras.First.SiiLog IsNot Nothing Then
        End If
        AddHandler oMenuItem.Click, AddressOf Do_SendToSii_Produccion
        Return oMenuItem
    End Function

    Private Function MenuItem_SendToSii_Pruebas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar a Hisenda (proves)"
        oMenuItem.ForeColor = Color.Red
        If _BookFra.SiiLog IsNot Nothing Then
            If _BookFra.SiiLog.Result = DTOSiiLog.Results.Correcto And _BookFra.SiiLog.Entorno = DTO.Defaults.Entornos.Produccion Then
                oMenuItem.Enabled = False
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_SendToSii_Pruebas
        Return oMenuItem
    End Function

    Private Function MenuItem_SiiLog() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SiiLog
        oMenuItem.Text = "consulta Hisenda"
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BookFra(_BookFra)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = _BookFras.First.Cca.Exercici
        Dim oExcel = Await FEB2.Bookfras.Excel(exs, oExercici, Today)
        If exs.Count = 0 Then
            UIHelper.SaveExcelDialog(oExcel, , "Desar Llibre Factures Rebudes " & oExercici.Year)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_SendToSii_Pruebas(ByVal sender As Object, ByVal e As System.EventArgs)
        Do_SendToSii(DTO.Defaults.Entornos.Pruebas)
    End Sub
    Private Sub Do_SendToSii_Produccion(ByVal sender As Object, ByVal e As System.EventArgs)
        Do_SendToSii(DTO.Defaults.Entornos.Produccion)
    End Sub

    Private Async Sub Do_SendToSii(entorno As DTO.Defaults.Entornos)
        Dim exs As New List(Of Exception)
        Dim oTaskResult = Await FEB2.Bookfras.SendToSii(Current.Session.Emp, entorno, _BookFras, _ShowProgress, exs)
        If oTaskResult.Success Then
            Dim iOk As Integer = _BookFras.Where(Function(x) x.SiiLog.Result = DTOSiiLog.Results.Correcto).Count
            Dim sb As New Text.StringBuilder
            If _BookFras.Count = iOk Then
                If iOk = 1 Then
                    sb.AppendLine("factura enviada correctament")
                Else
                    sb.AppendLine(String.Format("{0} factures enviades correctament", iOk))
                End If
            Else
                sb.AppendLine(String.Format("enviades correctament {0} de {1} factures", iOk, _BookFras.Count))
            End If
            Dim oCsvInvoice = _BookFras.FirstOrDefault(Function(x) x.SiiLog.Csv.isNotEmpty())
            If oCsvInvoice IsNot Nothing Then
                sb.AppendLine("csv: " & oCsvInvoice.SiiLog.Csv)
                sb.AppendLine("data: " & Format(oCsvInvoice.SiiLog.Fch, "dd/MM/yy HH:mm"))
            End If
            MsgBox(sb.ToString())
            RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_SiiLog()
        Dim exs As New List(Of Exception)
        Dim oBookFra As DTOBookFra = _BookFras.First
        If FEB2.BookFra.Load(exs, oBookFra) Then
            Dim oX509Cert = Await FEB2.Cert.X509Certificate2(Current.Session.Emp.Org, exs)
            If exs.Count = 0 Then
                Dim oLog = AeatSii.FacturasRecibidas.QuerySiiLog(DTO.Defaults.Entornos.Produccion, oX509Cert, Current.Session.Emp.Org, oBookFra, exs)
                If oLog Is Nothing Then
                    UIHelper.WarnError(exs)
                Else
                    oBookFra.SiiLog = oLog
                    If Await FEB2.BookFra.LogSii(exs, oBookFra) Then
                        MsgBox("factura actualitzada correctament" & vbCrLf & "resultat: " & oLog.Result.ToString & vbCrLf & "csv: " & oLog.Csv & vbCrLf & "data: " & Format(oLog.Fch, "dd/MM/yy HH:mm") & vbCrLf & "errors: " & oLog.ErrMsg)
                    Else
                        UIHelper.WarnError("No s'ha pogut actualitzar la factura" & vbCrLf & "resultat: " & oLog.Result.ToString & vbCrLf & "csv: " & oLog.Csv & vbCrLf & "data: " & Format(oLog.Fch, "dd/MM/yy HH:mm") & vbCrLf & "errors: " & oLog.ErrMsg)
                    End If
                End If
            Else
                UIHelper.WarnError(exs, "Error al descarregar el certificat")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class

