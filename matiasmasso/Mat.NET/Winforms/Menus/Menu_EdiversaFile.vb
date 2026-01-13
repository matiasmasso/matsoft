Public Class Menu_EdiversaFile
    Inherits Menu_Base

    Private _EdiversaFiles As List(Of DTOEdiversaFile)
    Private _EdiversaFile As DTOEdiversaFile

    Public Sub New(ByVal oEdiversaFiles As List(Of DTOEdiversaFile))
        MyBase.New()
        _EdiversaFiles = oEdiversaFiles
        If _EdiversaFiles IsNot Nothing Then
            If _EdiversaFiles.Count > 0 Then
                _EdiversaFile = _EdiversaFiles.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oEdiversaFile As DTOEdiversaFile)
        MyBase.New()
        _EdiversaFile = oEdiversaFile
        _EdiversaFiles = New List(Of DTOEdiversaFile)
        If _EdiversaFile IsNot Nothing Then
            _EdiversaFiles.Add(_EdiversaFile)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_SaveFile())
        MyBase.AddMenuItem(MenuItem_ReturnToProcess())
        MyBase.AddMenuItem(MenuItem_Process())
        MyBase.AddMenuItem(MenuItem_Restore())
        MyBase.AddMenuItem(MenuItem_Descarta())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _EdiversaFiles.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_SaveFile() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desa"
        oMenuItem.Image = My.Resources.disk
        AddHandler oMenuItem.Click, AddressOf Do_SaveFile
        Return oMenuItem
    End Function

    Private Function MenuItem_Restore() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Restaura"
        AddHandler oMenuItem.Click, AddressOf Do_Restore
        Return oMenuItem
    End Function

    Private Function MenuItem_ReturnToProcess() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Torna a pendent de processar"
        AddHandler oMenuItem.Click, AddressOf Do_ReturnToProcess
        Return oMenuItem
    End Function

    Private Function MenuItem_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importa"
        AddHandler oMenuItem.Click, AddressOf Do_Import
        Return oMenuItem
    End Function

    Private Function MenuItem_Process() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Processa"
        AddHandler oMenuItem.Click, AddressOf Do_Process
        Return oMenuItem
    End Function

    Private Function MenuItem_Descarta() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Descartar"
        AddHandler oMenuItem.Click, AddressOf Do_Descarta
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar definitivament"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case _EdiversaFile.Tag
            Case DTOEdiversaFile.Tags.ORDRSP_D_96A_UN_EAN005
                Dim oFrm As New Frm_EdiversaFile(_EdiversaFile)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Else
                Dim oFrm As New Frm_EdiversaFile(_EdiversaFile)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub Do_Savefile()
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "Desar fitxer pla de Ediversa"
            .Filter = "Fitxers de text (*.txt)|*.txt|tots els fitxers|*.*"
            .DefaultExt = ".txt"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog = DialogResult.OK Then
                If FEB2.EdiversaFile.Load(_EdiversaFile, exs) Then
                    _EdiversaFile.LoadSegments()
                    Dim sr As IO.StreamWriter = Nothing
                    Try
                        sr = IO.File.CreateText(.FileName)

                        For Each oSegment As DTOEdiversaSegment In _EdiversaFile.Segments
                            sr.WriteLine(oSegment.ToString())
                        Next
                        sr.Close()
                    Catch ex As Exception
                        exs.Add(ex)
                        UIHelper.WarnError(exs)
                    End Try
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Async Sub Do_Restore(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)

        If Await FEB2.EdiversaFiles.Restore(exs, Current.Session.Emp, _EdiversaFiles) Then
            For Each oFile In _EdiversaFiles
                Await FEB2.EdiversaFile.Update(exs, oFile)
            Next
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaFiles))
        End If

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)

        End If
    End Sub
    Private Async Sub Do_Import()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importa y procesa fitxer Edi"
            If .ShowDialog = DialogResult.OK Then
                Dim oEdiversaFile = New DTOEdiversaFile
                With oEdiversaFile
                    .FileName = oDlg.FileName
                    .FchCreated = Now
                    .Stream = My.Computer.FileSystem.ReadAllText(oDlg.FileName)
                End With
                Dim exs As New List(Of Exception)
                If Await FEB2.EdiversaFileSystem.SaveInboxFile(oEdiversaFile, exs) Then
                    MsgBox("Ok")
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Async Sub Do_Process()
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        If Await FEB2.EdiversaFileSystem.SaveInboxFiles(_EdiversaFiles, exs) Then
            MyBase.ToggleProgressBarRequest(False)
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaFiles))
        Else
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaFiles))
            MyBase.ToggleProgressBarRequest(False)
            Dim sb As New System.Text.StringBuilder
            For Each ex As Exception In exs
                sb.AppendLine(ex.Message)
            Next

            Dim data_object As New DataObject
            data_object.SetData(DataFormats.Text, True, sb.ToString())
            Clipboard.SetDataObject(data_object, True)

            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ReturnToProcess()
        Dim exs As New List(Of Exception)

        For Each oFile In _EdiversaFiles
            If FEB2.EdiversaFile.Load(oFile, exs) Then
                oFile.LoadSegments()
                oFile.Result = DTOEdiversaFile.Results.Pending
                oFile.ResultBaseGuid = Nothing
                If Not Await FEB2.EdiversaFile.Update(exs, oFile) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Next
        MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaFiles))

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_Descarta(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Descartem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.EdiversaFiles.Descarta(exs, _EdiversaFiles) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al descartar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.EdiversaFiles.Delete(_EdiversaFiles, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


