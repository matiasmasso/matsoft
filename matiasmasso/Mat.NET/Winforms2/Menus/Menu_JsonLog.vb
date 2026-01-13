Public Class Menu_JsonLog
    Inherits Menu_Base

    Private _JsonLogs As List(Of DTOJsonLog)
    Private _JsonLog As DTOJsonLog

    Public Sub New(ByVal oJsonLogs As List(Of DTOJsonLog))
        MyBase.New()
        _JsonLogs = oJsonLogs
        If _JsonLogs IsNot Nothing Then
            If _JsonLogs.Count > 0 Then
                _JsonLog = _JsonLogs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oJsonLog As DTOJsonLog)
        MyBase.New()
        _JsonLog = oJsonLog
        _JsonLogs = New List(Of DTOJsonLog)
        If _JsonLog IsNot Nothing Then
            _JsonLogs.Add(_JsonLog)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        If _JsonLog.ResultTarget IsNot Nothing Then
            If _JsonLog.Schema.Equals(DTOJsonSchema.Wellknown(DTOJsonSchema.Wellknowns.Purchaseorder)) Then
                Dim oPdcMenuItem As New ToolStripMenuItem("comanda")
                MyBase.AddMenuItem(oPdcMenuItem)
                Dim oPurchaseOrder As New DTOPurchaseOrder(_JsonLog.ResultTarget.Guid)
                Dim oMenuPdc As New Menu_Pdc({oPurchaseOrder}.ToList())
                oPdcMenuItem.DropDownItems.AddRange(oMenuPdc.Range)
            End If
        End If
        MyBase.AddMenuItem(MenuItem_Procesa())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _JsonLogs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Procesa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Procesa"
        oMenuItem.Enabled = _JsonLogs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Procesa
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
        Dim oFrm As New Frm_JsonLog(_JsonLog)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Procesa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        Dim url As String = "http://localhost:55836/api/jsonlog/mailbox"
        FEB.JsonLog.Load(exs, _JsonLog)
        If exs.Count = 0 Then
            Dim jsonInput = _JsonLog.Json
            Dim stringContent As New System.Net.Http.StringContent(jsonInput, System.Text.Encoding.UTF8, "application/json")

            Try
                Using client As New System.Net.Http.HttpClient
                    Using response As System.Net.Http.HttpResponseMessage = Await client.PostAsync(url, stringContent)
                        MyBase.ToggleProgressBarRequest(False)
                        If response.IsSuccessStatusCode Then
                            MyBase.RefreshRequest(Me, New MatEventArgs(_JsonLog))
                        Else
                            Dim errMsg = Await response.Content.ReadAsStringAsync()
                            exs.Add(New Exception(errMsg))
                        End If
                    End Using
                End Using

            Catch ex As Exception
                exs.Add(ex)
            End Try

        End If
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.JsonLog.Delete(exs, _JsonLogs.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

