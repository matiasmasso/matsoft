

Public Class Frm_Banc_AutorizVtos
    Private mBanc As Banc
    Private mDs As DataSet
    Private mAllowEvents As Boolean

    Private Enum Cols
        Chk
        Pnd
        Vto
        Eur
        Prv
        Nom
        Fra
        Fch
        Obs
    End Enum

    Public WriteOnly Property Banc() As Banc
        Set(ByVal value As Banc)
            mBanc = value
            PictureBoxBancLogo.Image = mBanc.Img48
            DateTimePickerVto.Value = SuggestFch()
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim sFch As String = Format(DateTimePickerVto.Value, "yyyyMMdd")
        Dim SQL As String = "SELECT CAST (0 AS BIT) AS CHK, " _
        & "PND.Id, PND.vto, PND.eur, CLX.cli, CLX.clx, PND.fra, PND.fch, PND.fpg " _
        & "FROM PND INNER JOIN " _
        & "CLX ON PND.ContactGuid = CLX.Guid " _
        & "WHERE PND.ad LIKE 'A' AND " _
        & "PND.Status =" & Pnd.StatusCod.pendent & " And " _
        & "PND.cfp =" & DTOCustomer.FormasDePagament.DomiciliacioBancaria & " " _
        & "ORDER BY PND.vto, CLX.clx, PND.fch, PND.fra"
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)
        ChkVto()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.Pnd)
                .Visible = False
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Prv)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "Observacions"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With


    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim DtVto As Date = DateTimePickerVto.Value
        Dim BlWarn As Boolean
        Dim oPnds As New Pnds
        Dim oPnd As Pnd
        Dim oMail As New Mail(mBanc.Emp, Today)
        Dim oPdf As New PdfBancAutoritzacioVtos(mBanc, DateTimePickerVto.Value, oPnds, oMail)

        Dim exs as New List(Of exception)
        With oMail
            .Cod = DTO.DTOCorrespondencia.Cods.Enviat
            .Contacts.Add(mBanc)
            .Subject = "AUTORITZACIO VENCIMENTS " & DtVto.ToShortDateString

            Dim oDocFile As DTODocFile = Nothing
            If BLL_DocFile.LoadFromStream(oPdf.Stream, oDocFile, exs) Then
                .DocFile = oDocFile
                .DocFile.PendingOp = DTODocFile.PendingOps.Update
            End If

            If .Update(exs) Then
                Dim oRow As DataGridViewRow
                For Each oRow In DataGridView1.Rows
                    If oRow.Cells(Cols.Chk).Value Then
                        oPnd = New Pnd(oRow.Cells(Cols.Pnd).Value)
                        If oPnd.Vto <> DtVto And Not BlWarn Then
                            Dim rc As MsgBoxResult = MsgBox("S'han autoritzat venciments que no coincideixen amb la data " & Format(DtVto, "dd/MM/yy"), MsgBoxStyle.OkCancel, "MAT.NET")
                            If rc <> MsgBoxResult.Ok Then Exit Sub
                            BlWarn = True
                        End If
                        oPnd.Fpg = "autoritzat a " & mBanc.Abr & " (n/ref." & oMail.Id & ")"
                        exs = New List(Of Exception)
                        If Not oPnd.Update(exs) Then
                            MsgBox("error al desar PND" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                        End If
                        oPnds.Add(oPnd)
                    End If
                Next
                'root.ShowFileDocument(.FileDocument)
                UIHelper.ShowStream(.DocFile)
                Me.Close()
            Else
                MsgBox("error al desar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Function SuggestFch() As Date
        Dim DtFch As Date
        Select Case Today.Day
            Case 31
                DtFch = Today
            Case Is < 15
                DtFch = Today.AddDays(-Today.Day + 15)
            Case Else
                DtFch = Today.AddMonths(1).AddDays(-Today.Day - 1)
        End Select
        Return DtFch
    End Function

    Private Sub ChkVto()
        Dim DtVto As Date = DateTimePickerVto.Value
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = (oRow.Cells(Cols.Vto).Value = DtVto)
        Next
    End Sub

    Private Sub DateTimePickerVto_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerVto.ValueChanged
        If mAllowEvents Then
            ChkVto()
        End If
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If oRow.Cells(Cols.Vto).Value = DateTimePickerVto.Value Then
            oRow.DefaultCellStyle.BackColor = Color.LightSkyBlue
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Chk
                ButtonOk.Enabled = True
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

End Class