

Imports FEB
Imports System.Net

Public Class Menu_Ccd
    Private _Ccds As List(Of DTOCcd)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oCcd As DTOCcd)
        MyBase.New()
        _Ccds = New List(Of DTOCcd)
        _Ccds.Add(oCcd)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Extracte(),
        MenuItem_Contact(),
        MenuItem_Excel(),
        MenuItem_SavePdfs()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extracte"
        If _Ccds.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function



    Private Function MenuItem_Contact() As ToolStripMenuItem
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        If _Ccds.Count = 1 Then
            If _Ccds.First.Contact IsNot Nothing Then
                oMenuItem.Text = "Contacte..."
                Dim oContactMenu = FEB.ContactMenu.FindSync(exs, _Ccds.First.Contact)
                oMenuItem.DropDownItems.AddRange(New Menu_Contact(_Ccds.First.Contact, oContactMenu).Range)
            Else
                oMenuItem.Visible = False
            End If
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "excel"
        oMenuItem.Image = My.Resources.Excel
        If _Ccds.Count <= 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function

    Private Function MenuItem_SavePdfs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desar tots els justificants"
        oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_SavePdfs
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================


    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCcd As DTOCcd = _Ccds(0)
        Dim oFrm As New Frm_Extracte(oCcd.Contact, oCcd.Cta, oCcd.Exercici)
        oFrm.Show()
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDs As New DataSet
        Dim oTable As New DataTable
        oTable.Columns.Add("CLX", System.Type.GetType("System.String"))
        Dim oRow As DataRow
        oDs.Tables.Add(oTable)
        For Each oCcd As DTOCcd In _Ccds
            oRow = oTable.NewRow
            oRow(0) = oCcd.Contact.FullNom
            oTable.Rows.Add(oRow)
        Next
        Dim oSheet = MatHelper.Excel.Sheet.Factory(oDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_SavePdfs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCcd = _Ccds.First
        Dim oDlg As New FolderBrowserDialog
        With oDlg
            .ShowNewFolderButton = True
            If .ShowDialog = DialogResult.OK Then
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
                Dim path As String = String.Format("{0}\justificants compte {1} {2} {3}", .SelectedPath, oCcd.Cta.Id, oCcd.Cta.Nom.Tradueix(Current.Session.Lang), oCcd.Contact.Nom)
                System.IO.Directory.CreateDirectory(path)
                Dim oCcbs = Await FEB.Ccbs.All(exs, oCcd.Exercici, oCcd.Cta, oCcd.Contact)
                If exs.Count = 0 Then
                    For Each oCcb In oCcbs
                        If oCcb.Cca.DocFile IsNot Nothing Then
                            Dim url = FEB.DocFile.DownloadUrl(oCcb.Cca.DocFile, True)
                            Dim fileName As String = String.Format("{0}\{1:0000}.{2:00000}.pdf", path, oCcb.Cca.Fch.Year, oCcb.Cca.Id)
                            Using client As New WebClient()
                                client.DownloadFile(url, fileName)
                            End Using
                        End If
                    Next
                    RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                Else
                    RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                    UIHelper.WarnError(exs)
                End If
            End If
        End With

    End Sub
End Class
