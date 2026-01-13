Public Class Menu_Intrastat
    Inherits Menu_Base

    Private _Intrastats As List(Of DTOIntrastat)
    Private _Intrastat As DTOIntrastat

    Public Sub New(ByVal oIntrastats As List(Of DTOIntrastat))
        MyBase.New()
        _Intrastats = oIntrastats
        If _Intrastats IsNot Nothing Then
            If _Intrastats.Count > 0 Then
                _Intrastat = _Intrastats.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oIntrastat As DTOIntrastat)
        MyBase.New()
        _Intrastat = oIntrastat
        _Intrastats = New List(Of DTOIntrastat)
        If _Intrastat IsNot Nothing Then
            _Intrastats.Add(_Intrastat)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_SaveFile())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Intrastats.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_SaveFile() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "desar Fitxer"
        oMenuItem.Enabled = _Intrastats.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SaveFile
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
        'Dim oFrm As New Frm_Intrastat_Old(_Intrastat)
        Dim oFrm As New Frm_Intrastat(_Intrastat)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Public Sub Do_SaveFile()
        SaveFile(_Intrastat)
    End Sub

    Shared Sub SaveFile(oIntrastat As DTOIntrastat)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .InitialDirectory = "C:\AEAT"
            .Title = "Desar fitxer Intrastat"
            .Filter = "fitxers Txt|*.txt|tots els arxius|*.*"
            .FilterIndex = 0
            .FileName = DTOIntrastat.DefaultFileName(oIntrastat)
            .AddExtension = True
            .DefaultExt = ".txt"

            If .ShowDialog = DialogResult.OK Then
                Dim exs As New List(Of Exception)
                If FEB2.Intrastat.Load(oIntrastat, exs) Then
                    Dim sr As IO.StreamWriter
                    Try
                        Dim src As String = DTOIntrastat.FileStringBuilder(oIntrastat)
                        sr = New IO.StreamWriter(.FileName, False, System.Text.Encoding.Default)
                        sr.Write(src)
                        sr.Flush()
                        sr.Close()
                    Catch ex As Exception
                        UIHelper.WarnError(ex)
                    End Try
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With

    End Sub


    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Intrastat.Delete(_Intrastats.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


