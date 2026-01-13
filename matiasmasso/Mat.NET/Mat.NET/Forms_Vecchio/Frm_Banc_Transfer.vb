

Public Class Frm_Banc_Transfer
    Private mBancTransfer As BancTransfer
    Private mAllowEvents As Boolean = False

    Private Enum Cols
        Eur
        Nom
        CliGuid
        CtaGuid
        PgcCta
        CtaDsc
        Iban
        Text
    End Enum

    Public Sub New(ByVal oBancTransfer As BancTransfer)
        MyBase.New()
        Me.InitializeComponent()
        mBancTransfer = oBancTransfer
        Refresca()
        ButtonDel.Enabled = mBancTransfer.Exists
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        DateTimePicker1.Value = mBancTransfer.Fch
        Xl_Contact1.Contact = mBancTransfer.Banc
        TextBoxConcepte.Text = mBancTransfer.Concepte
        Dim oTb As New DataTable
        Dim oRow As DataRow = Nothing
        With oTb.Columns
            .Add("EUR", System.Type.GetType("System.Decimal"))
            .Add("NOM", System.Type.GetType("System.String"))
            .Add("CliGuid", System.Type.GetType("System.Guid"))
            .Add("CtaGuid", System.Type.GetType("System.Guid"))
            .Add("PGCCTA", System.Type.GetType("System.String"))
            .Add("CTADSC", System.Type.GetType("System.String"))
            .Add("IBAN", System.Type.GetType("System.String"))
        End With

        For Each oitem As BankTransferItm In mBancTransfer.Itms
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With oitem
                oRow(Cols.Eur) = .Ccb.Amt.Eur
                oRow(Cols.Nom) = .Ccb.Contact.Nom
                oRow(Cols.CliGuid) = .Ccb.Contact.Guid
                oRow(Cols.CtaGuid) = .Ccb.Cta.Guid
                oRow(Cols.PgcCta) = .Ccb.Cta.Id
                oRow(Cols.CtaDsc) = .Ccb.Cta.FullNom
                oRow(Cols.Iban) = .IbanDigits
            End With
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "beneficiari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.CliGuid)
                .Visible = False
            End With
            With .Columns(Cols.CtaGuid)
                .Visible = False
            End With
            With .Columns(Cols.PgcCta)
                .Visible = False
            End With
            With .Columns(Cols.CtaDsc)
                .HeaderText = "comptabilització"
                .Width = 200
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Iban)
                .HeaderText = "Iban"
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With

        DisplayTotal()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oTransfer As BancTransfer = GetTransferFromGrid()

        Dim exs as New List(Of exception)
        If oTransfer.Update( exs) Then
            Me.Close()
        Else
            MsgBox("error al desar la transferencia" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub

    Private Function GetTransferFromGrid() As BancTransfer
        Dim oPlan As PgcPlan = PgcPlan.FromYear(DateTimePicker1.Value.Year)
        Dim oBanc As New Banc(Xl_Contact1.Contact.Guid)

        With mBancTransfer
            .Fch = DateTimePicker1.Value
            .Banc = oBanc
            .Concepte = TextBoxConcepte.Text
            .Itms.Clear()
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If Not oRow.IsNewRow Then
                    Dim oContact As New Contact(CType(oRow.Cells(Cols.CliGuid).Value, Guid))
                    Dim oCta As New PgcCta(CType(oRow.Cells(Cols.CtaGuid).Value, Guid))
                    Dim oIban As DTOIban = BLL.BLLIban.FromDigits(oRow.Cells(Cols.Iban).Value.ToString)
                    Dim oAmt As New Amt(CDec(oRow.Cells(Cols.Eur).Value))
                    Dim oCcb As New Ccb(oCta, oContact, oAmt, DTOCcb.DhEnum.Debe)
                    Dim oItem As New BankTransferItm(oCcb, oIban.Digits.ToString)
                    .Itms.Add(oItem)
                End If
            Next
        End With
        Return mBancTransfer
    End Function

    Private Sub DisplayTotal()
        Dim DcTot As Decimal = 0
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            DcTot += CDec(oRow.Cells(Cols.Eur).Value)
        Next
        TextBoxTot.Text = Format(DcTot, "#,##0.00 €;-#,##0.00 €;#")
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
            Select Case e.ColumnIndex
                Case Cols.Eur
                    DisplayTotal()
            End Select
        End If
    End Sub


    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la transferencia?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mBancTransfer.Delete( exs) Then
                Me.Close()
            Else
                MsgBox( BLL.Defaults.ExsToMultiline(exs))
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If mAllowEvents Then
            DisplayTotal()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub PictureBoxSaveFile_Click(sender As Object, e As System.EventArgs) Handles PictureBoxSaveFile.Click
        Dim oBancTransfer As BancTransfer = GetTransferFromGrid()
        Dim oAeb341 As AEB341 = Nothing
        Dim exs as New List(Of exception)
        If BLL_BankTransfer.AEB341(oBancTransfer, oAeb341, exs) Then
            Dim oDlg As New Windows.Forms.SaveFileDialog
            With oDlg
                .FileName = "AEB341_nominas_" & mBancTransfer.Cca.yea & "_" & mBancTransfer.Cca.Id & ".txt"
                .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                .FilterIndex = 1
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    oAeb341.Save(.FileName)
                End If
            End With
        Else
            UIHelper.WarnError( exs, "els següents comptes no han passat la validació:")
        End If
    End Sub
End Class