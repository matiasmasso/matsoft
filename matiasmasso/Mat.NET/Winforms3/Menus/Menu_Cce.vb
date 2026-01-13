

Imports System.Net

Public Class Menu_Cce
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Private _Cce As DTOCce

    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)


    Public Sub New(ByVal oCce As DTOCce)
        MyBase.New()
        _Cce = oCce
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Extracte(),
        MenuItem_PgcCta(),
        MenuItem_Web(),
        MenuItem_CopyLink(),
        MenuItem_SavePdfs()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================



    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extracte"
        'oMenuItem.Image = My.Resources.
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_PgcCta() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pla comptable"
        'oMenuItem.Image = My.Resources.
        AddHandler oMenuItem.Click, AddressOf Do_PgcCta
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "web"
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
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
        Dim oFrm As New Frm_Extracte(Nothing, _Cce.Cta, _Cce.Exercici)
        oFrm.Show()
    End Sub

    Private Async Sub Do_SavePdfs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDlg As New FolderBrowserDialog
        With oDlg
            .ShowNewFolderButton = True
            If .ShowDialog = DialogResult.OK Then
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
                Dim path As String = String.Format("{0}\justificants compte {1} {2}", .SelectedPath, _Cce.Cta.Id, _Cce.Cta.Nom.Tradueix(Current.Session.Lang))
                System.IO.Directory.CreateDirectory(path)
                Dim oCcbs = Await FEB.Ccbs.All(exs, GlobalVariables.Emp, _Cce.Exercici.Year, _Cce.Cta)
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

    Private Sub Do_PgcCta(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PgcCta(_Cce.Cta)
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(FEB.Cce.Url(_Cce), True)
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Process.Start(FEB.Cce.Url(_Cce, True))
    End Sub


End Class
