Public Class Menu_ElCorteInglesAlineamientoDisponibilidad
    Inherits Menu_Base

    Private _Logs As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
    Private _Log As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad

    Public Sub New(ByVal oLogs As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad))
        MyBase.New()
        _Logs = oLogs
        If _Logs IsNot Nothing Then
            If _Logs.Count > 0 Then
                _Log = _Logs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oLog As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        MyBase.New()
        _Log = oLog
        _Logs = New List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        If _Log IsNot Nothing Then
            _Logs.Add(_Log)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Save())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Logs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Save() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desar"
        oMenuItem.Image = My.Resources.download_16
        AddHandler oMenuItem.Click, AddressOf Do_Savefile
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_AlineamientoDeStocks(_Log)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Savefile()
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "Desar Fitxer de Alineacion de disponibilidad de El Corte Ingles"
            .FileName = "STOCK.TXT"
            .Filter = "fitxers de text|*.txt|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                MyBase.ToggleProgressBarRequest(True)

                Dim oLog = Await FEB.ElCorteIngles.AlineamientoDeDisponibilidad(exs, _Log.Guid, Current.Session.User)
                MyBase.ToggleProgressBarRequest(False)
                If exs.Count = 0 Then
                    If Not MatHelperStd.FileSystemHelper.SaveTextToFile(oLog.Text, .FileName, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End With
    End Sub
End Class


