Public Class Frm_EdiversaFile
    Private _File As DTOEdiversaFile
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Txt
    End Enum

    Public Sub New(oFile As DTOEdiversaFile)
        MyBase.New
        InitializeComponent()
        _File = oFile
    End Sub

    Private Async Sub Frm_EdiversaFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _File.IsLoaded = False
        If FEB2.EdiversaFile.Load(_File, exs) Then
            LoadProperties()
            LoadCodiGrid()
            Await LoadExceptions()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadExceptions() As Task
        Dim exs As New List(Of Exception)
        Dim oEdiversaExceptions = Await FEB2.EdiversaExceptions.All(_File, exs)
        If exs.Count = 0 Then
            Await Xl_EdiversaExceptions1.Load(oEdiversaExceptions)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub LoadProperties()
        With _File
            TextBoxTag.Text = .Tag

            If .Sender IsNot Nothing Then
                If .Sender.Ean IsNot Nothing Then
                    TextBoxSenderEan.Text = .Sender.Ean.Value
                    If .Sender.Contact IsNot Nothing Then
                        TextBoxSenderNom.Text = .Sender.Contact.Nom
                    End If
                End If
            End If

            If .Receiver IsNot Nothing Then
                If .Receiver.Ean IsNot Nothing Then
                    TextBoxReceiverEan.Text = .Receiver.Ean.Value
                    If .Receiver.Contact IsNot Nothing Then
                        TextBoxReceiverNom.Text = .Receiver.Contact.Nom
                    End If
                End If
            End If

            If .Amount IsNot Nothing Then
                TextBoxAmt.Text = DTOAmt.CurFormatted(.Amount)
            End If
            TextBoxDocNum.Text = .Docnum
            TextBoxFch.Text = .Fch.ToShortDateString

            TextBoxFilename.Text = .FileName
            TextBoxFchCreated.Text = Format(.FchCreated, "dd/MM/yy HH:mm")
            TextBoxIOCod.Text = IIf(.IOCod = DTOEdiversaFile.IOcods.Inbox, "Entrada", "Sortida")
            Select Case .Result
                Case DTOEdiversaFile.Results.Deleted
                    TextBoxResult.Text = "Eliminat"
                Case DTOEdiversaFile.Results.Processed
                    TextBoxResult.Text = "Processat"
                Case DTOEdiversaFile.Results.Pending
                    TextBoxResult.Text = "Pendent"
            End Select
        End With
    End Sub


    Private Sub LoadCodiGrid()

        Dim oTb As New DataTable
        oTb.Columns.Add(New DataColumn("Txt", System.Type.GetType("System.String")))

        For Each oSegment As DTOEdiversaSegment In _File.Segments
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow(Cols.Txt) = oSegment.ToString()
        Next

        _AllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False


            With .Columns(Cols.Txt)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim sb As New System.Text.StringBuilder
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not IsDBNull(oRow.Cells(Cols.Txt).Value) Then
                Dim sVal As String = oRow.Cells(Cols.Txt).Value
                If sVal > "" Then
                    sb.AppendLine(sVal)
                End If
            End If
        Next
        Dim sText As String = sb.ToString
        _File.Stream = sText
        Dim exs As New List(Of Exception)
        If Await FEB2.EdiversaFile.Update(exs, _File) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_File))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar el fitxer")
        End If
    End Sub

    Private Async Sub RestoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oFile = Await FEB2.EdiversaFile.Restore(exs, GlobalVariables.Emp, _File)
        If exs.Count = 0 Then
            _File = oFile
            If Await FEB2.EdiversaFile.Update(exs, _File) Then
                Await refresca()
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_File))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ExportaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportaToolStripMenuItem.Click
        Dim oDlg As New SaveFileDialog()
        With oDlg
            .Title = "Desar fitxer Edi"
            .Filter = "fitxers de text|*.txt|tots els documents (*.*)|*.*"
            .AddExtension = True
            .DefaultExt = ".txt"
            If .ShowDialog = DialogResult.OK Then
                If IO.File.Exists(.FileName) Then
                    IO.File.WriteAllText(.FileName, _File.Stream)
                Else
                    IO.File.WriteAllText(.FileName, _File.Stream)
                End If
            End If
        End With
    End Sub

    Private Sub ImportaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportaToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Desar fitxer Edi"
            .Filter = "fitxers de text|*.txt|tots els documents (*.*)|*.*"
            If .ShowDialog = DialogResult.OK Then
                If IO.File.Exists(.FileName) Then
                    _File.stream = IO.File.ReadAllText(.FileName)
                    _File.loadSegments()
                    LoadCodiGrid()
                    ButtonOk.Enabled = True
                Else
                    UIHelper.WarnError("no existeix el fitxer")
                End If
            End If
        End With
    End Sub
End Class