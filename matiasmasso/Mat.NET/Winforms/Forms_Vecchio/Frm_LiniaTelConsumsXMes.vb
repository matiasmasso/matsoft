

Public Class Frm_LiniaTelConsumsXMes
    Private _LiniaTelefon As DTOLiniaTelefon = Nothing
    Private _Consums As List(Of DTOLiniaTelefon.Consum)
    Private _DocFile As DTODocFile
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Fch
    End Enum

    Public Sub New(Optional oLinia As DTOLiniaTelefon = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _LiniaTelefon = oLinia
    End Sub

    Private Async Sub Frm_LiniaTelConsumsXMes_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Consums = Await FEB2.LiniaTelefonConsums.All(exs, _LiniaTelefon)
        If exs.Count = 0 Then
            SetFch()
            LoadView()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub LoadView()
        mAllowEvents = False
        Dim oListViewItem As ListViewItem = Nothing
        ListView1.Items.Clear()

        For Each oConsum In _Consums.Where(Function(x) x.YearMonth.Equals(CurrentYearMonth))
            oListViewItem = New ListViewItem(oConsum.Linia.Alias, 0)
            oListViewItem.Tag = oConsum
            ListView1.Items.Add(oListViewItem)
        Next
        SetContextMenu()
        mAllowEvents = True
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
    Private Function CurrentYearMonth() As DTOYearMonth
        Dim retval = New DTOYearMonth(CurrentYea, CurrentMes)
        Return retval
    End Function



    Private Function CurrentItm() As DTOLiniaTelefon.Consum
        Dim retval As DTOLiniaTelefon.Consum = Nothing
        If ListView1.SelectedItems.Count > 0 Then
            Dim oListViewItem As ListViewItem = ListView1.SelectedItems(0)
            retval = oListViewItem.Tag
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm = CurrentItm()
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
        Dim exs As New List(Of Exception)
        Dim oDocFiles As New List(Of DTODocFile)
        If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
            For Each oDocFile As DTODocFile In oDocFiles
                LoadDocFile(oDocFile)
            Next
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadDocFile(oDocFile As DTODocFile)
        If _LiniaTelefon Is Nothing Then
            Dim oFrm As New Frm_LiniesTelefon(, DTO.Defaults.SelectionModes.Selection)
            AddHandler oFrm.itemSelected, AddressOf onDroppedLiniaTelSelected
            _DocFile = oDocFile
            oFrm.Show()
        Else
            UpdateConsum(_LiniaTelefon, oDocFile)
        End If
    End Sub


    Private Sub onDroppedLiniaTelSelected(sender As Object, e As MatEventArgs)
        Dim oLineaTelefon As DTOLiniaTelefon = e.Argument
        UpdateConsum(oLineaTelefon, _DocFile)
    End Sub

    Private Async Sub UpdateConsum(oLinia As DTOLiniaTelefon, oDocFile As DTODocFile)
        Dim oConsum = DTOLiniaTelefon.Consum.Factory(oLinia, CurrentYearMonth, oDocFile)

        Dim exs As New List(Of Exception)
        If Await FEB2.LiniaTelefonConsum.Update(oConsum, exs) Then
            Await RefreshRequest(Me, MatEventArgs.Empty)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oConsum))
        Else
            UIHelper.WarnError(exs)
        End If

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
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim exs As New List(Of Exception)
                Dim oDocFile As DTODocFile = LegacyHelper.DocfileHelper.Factory(.FileName, exs)
                LoadDocFile(oDocFile)
            End If
        End With
    End Sub

    Private Async Sub Zoom()
        Dim oConsum = CurrentItm()
        Dim oDocFile As DTODocFile = oConsum.DocFile
        Dim exs As New List(Of Exception)
        If Not Await UIHelper.ShowStreamAsync(exs, oDocFile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Delete()
        Dim oConsum = CurrentItm()
        Dim rc As MsgBoxResult = MsgBox("eliminem " & oConsum.Linia.Alias & " del " & DTOLang.CAT.Mes(oConsum.YearMonth.Month) & " " & oConsum.YearMonth.Year & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.LiniaTelefonConsum.Delete(oConsum, exs) Then
                Await RefreshRequest(Nothing, EventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Function RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs) As Task
        Dim exs As New List(Of Exception)
        _Consums = Await FEB2.LiniaTelefonConsums.All(exs, _LiniaTelefon)
        If exs.Count = 0 Then
            LoadView()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        NumericUpDownYea.ValueChanged, _
         ListBoxMesos.SelectedIndexChanged

        If mAllowEvents Then
            LoadView()
        End If
    End Sub


End Class
