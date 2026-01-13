Public Class Frm_EdiversaRemadv
    Private mEdiFile As EdiFile_REMADV_D_96A_UN_EAN003
    Private _Sender As DTOContact

    Private Enum Cols
        Ico
        Pnd
        Num
        Fch
        Eur
    End Enum

    Public Sub New(oEdiFile As EdiFile_REMADV_D_96A_UN_EAN003)
        MyBase.New()
        Me.InitializeComponent()
        mEdiFile = oEdiFile

        Me.Text = mEdiFile.Fch.ToShortDateString & "-Remesa de pagaments " & mEdiFile.DocumentNumber & " de " & mEdiFile.SenderEanOrNom

        LoadHeader()

        _Sender = BLLContact.FromGLN(New DTOEan(mEdiFile.Sender.Value))
        For Each oLine As EdiLineItem In mEdiFile.LineItems
            Dim DcImport As Decimal = oLine.ImportNet
            Dim oDocSegment As EdiSegment_DOC = oLine.Segments(0)
            Dim oTipo As EdiSegment_DOC.Tipos = oDocSegment.Tipo
            If oTipo = EdiSegment_DOC.Tipos.Nota_de_Cargo Then
                DcImport = -DcImport
            End If
            Dim oGuid As Guid = BLLPnd.FromFra(_Sender, oLine.DocumentNumber, DcImport).Guid
            If oGuid = Nothing Then
                ButtonOk.Enabled = False
            Else
                oLine.CustomObject = oGuid
            End If
        Next

        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3
            .AutoGenerateColumns = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False
            .DataSource = mEdiFile.LineItems

            .Columns.Clear()
            Dim oCol As New DataGridViewImageColumn()
            .Columns.Add(oCol)
            .Columns.Add("Pnd", "Pnd")
            .Columns.Add("Num", "document")
            .Columns.Add("fch", "data")
            .Columns.Add("Eur", "import")

            With .Columns(Cols.Ico)
                .Width = 20
            End With
            With .Columns(Cols.Pnd)
                .Visible = False
                .DataPropertyName = "CustomObject"
            End With
            With .Columns(Cols.Num)
                .HeaderText = "document"
                .DataPropertyName = "DocumentNumber"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .DataPropertyName = "fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .DataPropertyName = "ImportNet"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

        End With
    End Sub

    Private Sub LoadHeader()
        Dim oContact As DTOContact = BLLContact.FromGLN(mEdiFile.Sender)
        BLLContact.Load(oContact)
        Xl_Contact_Logo1.Load(oContact)
        LabelFch.Text = mEdiFile.Fch.ToShortDateString
        LabelVto.Text = mEdiFile.Vto.ToShortDateString
        LabelEur.Text = Format(mEdiFile.Import, "#,###.00 €")
    End Sub



    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oGrid As DataGridView = CType(sender, DataGridView)
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                Dim oPndGuid As Guid = oRow.Cells(Cols.Pnd).Value
                If oPndGuid = Nothing Then
                    e.Value = My.Resources.warn
                Else
                    e.Value = My.Resources.Ok
                End If
        End Select
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oPnds As New List(Of DTOPnd)
        For Each oLine As EdiLineItem In mEdiFile.LineItems
            Dim oGuid As Guid = oLine.CustomObject
            Dim oPnd As DTOPnd = BLLPnd.Find(oGuid)
            oPnds.Add(oPnd)
        Next
        Dim oFrm As New Frm_Cobrament(_Sender, oPnds)
        oFrm.Show()
    End Sub
End Class