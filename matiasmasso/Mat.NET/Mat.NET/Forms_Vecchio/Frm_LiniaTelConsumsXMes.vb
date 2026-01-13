

Public Class Frm_LiniaTelConsumsXMes
    Private mLiniaTelefon As LiniaTelefon = Nothing
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Fch
    End Enum

    Public Sub New(Optional oLinia As LiniaTelefon = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mLiniaTelefon = oLinia
        SetFch()
        LoadView()
        SetContextMenu()
    End Sub

    Private Sub SetFch()
        Dim LastMonth As Date = Today.AddMonths(-1)
        NumericUpDownYea.Value = LastMonth.Year
        ListBoxMesos.SelectedIndex = LastMonth.Month - 1
    End Sub

    Private Function CurrentYea() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Function CurrentMes() As Integer
        Dim retVal As Integer = ListBoxMesos.SelectedIndex + 1
        Return retVal
    End Function


    Private Sub LoadView()
        mAllowEvents = False
        Dim oListViewItem As ListViewItem = Nothing
        ListView1.Items.Clear()

        Dim oConsums As LiniaTelConsums = LiniaTelConsums.FromFch(CurrentYea, CurrentMes, BLL.BLLSession.Current.User.Rol)
        For Each oConsum As LiniaTelConsum In oConsums
            Dim blshow As Boolean
            If mLiniaTelefon Is Nothing Then
                blshow = True
            Else
                blshow = oConsum.LiniaTelefon.Guid.Equals(mLiniaTelefon.Guid)
            End If
            If blshow Then
                oListViewItem = New ListViewItem(ItemText(oConsum), 0)
                oListViewItem.Tag = oConsum.Guid.ToString
                ListView1.Items.Add(oListViewItem)
            End If
        Next
        mAllowEvents = True
    End Sub

    Private Function ItemText(ByVal oItem As LiniaTelConsum) As String
        Dim sText As String = ""
        If oItem IsNot Nothing Then
            Select Case BLL.BLLSession.Current.User.Rol.Id
                Case Rol.Ids.SuperUser, Rol.Ids.Admin, Rol.Ids.SalesManager, Rol.Ids.Accounts
                    sText = oItem.LiniaTelefon.Alias
                    'Case Rol.Ids.Accounts
                    'sText = oItem.LiniaTelefon.Num
            End Select
        End If
        Return sText
    End Function

    Private Function CurrentItm() As LiniaTelConsum
        Dim retval As LiniaTelConsum = Nothing
        If ListView1.SelectedItems.Count > 0 Then
            Dim oListViewItem As ListViewItem = ListView1.SelectedItems(0)
            Dim sGuid As String = oListViewItem.Tag
            Dim oGuid As New Guid(sGuid)
            retval = New LiniaTelConsum(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As LiniaTelConsum = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.del, AddressOf Delete)
            oContextMenuStrip.Items.Add(oMenuItem)

        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        ListView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Zoom()
    End Sub

    Private Sub ListView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragDrop
        Dim exs as new list(Of Exception)

        For Each sFilename As String In root.FilesDroppedArray(e)
            Dim sExtension As String = System.IO.Path.GetExtension(sFilename)
            If sExtension = ".pdf" Then
                LoadBigFile(sFilename)
            Else
                exs.Add(New Exception(sFilename & "format incorrecte. Nomes s'admeten pdfs"))
            End If
        Next

        If exs.Count > 0 Then
            MsgBox( BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub

    Private Sub LoadBigFile(sFilename As String)
        Dim oBigFile As New maxisrvr.BigFileNew()
        root.LoadBigFilePdfFile(oBigFile, sFilename)

        If mLiniaTelefon Is Nothing Then
            Dim oFrm As New Frm_LiniesTelefon(BLL.Defaults.SelectionModes.Selection)
            AddHandler oFrm.AfterSelect, AddressOf onDroppedLiniaTelSelected
            oFrm.BigFile = oBigFile
            oFrm.Show()
        Else
            UpdateConsum(mLiniaTelefon, oBigFile)
        End If
    End Sub

    Private Sub onDroppedLiniaTelSelected(sender As Object, e As System.EventArgs)
        Dim oFrm As Frm_LiniesTelefon = sender
        UpdateConsum(oFrm.LiniaTelefon, oFrm.BigFile)
    End Sub

    Private Sub UpdateConsum(oLinia As LiniaTelefon, oBigfile As maxisrvr.BigFileNew)
        Dim oConsum As New LiniaTelConsum(oLinia)
        With oConsum
            .Yea = CurrentYea()
            .Mes = CurrentMes()
            .Bigfile = oBigfile
            .Update()
        End With

        Dim oListViewItem As New ListViewItem(oConsum.LiniaTelefon.Alias, 0)
        oListViewItem.Tag = oConsum.Guid.ToString
        ListView1.Items.Add(oListViewItem)
    End Sub

    Private Sub ListView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragOver
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Afegir llistat de consum telefonic"
            .Filter = "documents pdf (*.pdf)|*.pdf|(tots els fitxers)|*.*"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                LoadBigFile(.FileName)
            End If
        End With
    End Sub

    Private Sub Zoom()
        Dim oConsum As LiniaTelConsum = CurrentItm()
        root.ShowPdf(oConsum.Bigfile.Stream, oConsum.LiniaTelefon.Num & ".pdf")
    End Sub

    Private Sub Delete()
        Dim oItm As LiniaTelConsum = CurrentItm()
        Dim rc As MsgBoxResult = MsgBox("eliminem " & ItemText(oItm) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            oItm.Delete()
            RefreshRequest(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadView()
    End Sub


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        NumericUpDownYea.ValueChanged, _
         ListBoxMesos.SelectedIndexChanged

        If mAllowEvents Then
            LoadView()
        End If
    End Sub
End Class
