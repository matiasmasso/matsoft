

Public Class Frm_Banc_Impago
    Private mBanc As Banc
    Private mImpagats As Impagats
    Private mSum As maxisrvr.Amt
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Ref
        Eur
        Vto
        Txt
        Nom
    End Enum

    Public WriteOnly Property Impagats() As Impagats
        Set(ByVal value As Impagats)
            mImpagats = value
            If mImpagats.Count > 0 Then
                mBanc = mImpagats(0).Csb.Csa.Banc
                LoadBanc()
                LoadGrid()
                LoadDespeses()
            End If
            mAllowEvents = True
        End Set
    End Property

    'Public WriteOnly Property Banc() As Banc
    '    Set(ByVal value As Banc)
    '        mBanc = value
    '        LoadBanc()
    '        LoadDespeses()
    '        mAllowEvents = True
    '    End Set
    'End Property

    'Public WriteOnly Property Csbs() As Csbs
    '    Set(ByVal value As Csbs)
    '        mCsbs = value
    '        LoadGrid()
    '        LoadDespeses()
    '    End Set
    'End Property

    Private Sub LoadDespeses()
        Dim DcComisio As Decimal = 0
        Dim DcIVA As Decimal = 0
        Dim DcMail As Decimal = 0
        Dim SngIVApercent As Decimal = MaxiSrvr.Iva.Standard(DateTimePicker1.Value).Tipus
        If mBanc.ImpagComisio > 0 Then
            DcComisio = mSum.Percent(mBanc.ImpagComisio).Eur
        End If

        If mBanc.ImpagMinim Then
            If mBanc.ImpagMinim > DcComisio Then
                DcComisio = mBanc.ImpagMinim
            End If
        End If

        If mBanc.ImpagMail Then
            If mBanc.ImpagMailxRebut Then
                DcMail = mImpagats.Count * mBanc.ImpagMail
            Else
                DcMail = mBanc.ImpagMail
            End If
        End If

        Dim DcIvaBase As Decimal = 0
        If mBanc.Adr.Zip.Location.Zona.Country.ISO = Country.Default.ISO Then
            DcIvaBase = DcComisio
        End If

        If mBanc.ImpagMailIVA Then
            If mBanc.ImpagMailIVA Then
                DcIvaBase = DcIvaBase + DcMail
            End If
        End If

        DcIVA = Math.Round(DcIvaBase * SngIVApercent / 100, 2)
        Xl_AmtNominal.Amt = mSum
        Xl_AmtComisions.Amt = New maxisrvr.Amt(DcComisio)
        Xl_AmtIVA.Amt = New maxisrvr.Amt(DcIVA)
        Xl_AmtCorreu.Amt = New maxisrvr.Amt(DcMail)
        refrescaDespeses()

    End Sub

    Private Sub refrescaDespeses()
        Dim oAmt As maxisrvr.Amt = Xl_AmtNominal.Amt
        oAmt.Add(Xl_AmtComisions.Amt)
        oAmt.Add(Xl_AmtIVA.Amt)
        oAmt.Add(Xl_AmtCorreu.Amt)
        Xl_AmtTotal.Amt = oAmt
    End Sub

    Private Sub LoadBanc()
        If mBanc.ImpagComisio <> 0 Then TextBoxTipus.Text = mBanc.ImpagComisio & "%"
        If mBanc.ImpagMinim <> 0 Then TextBoxMinim.Text = Format(mBanc.ImpagMinim, "#,##0.00")
        If mBanc.ImpagMail <> 0 Then TextBoxCondsMail.Text = Format(mBanc.ImpagMail, "#,##0.00")
        CheckBoxIvaMail.Checked = mBanc.ImpagMailIVA
        CheckBoxMailxRebut.Checked = mBanc.ImpagMailxRebut
        PictureBoxBancLogo.Image = mBanc.Img48
        GroupBoxCondicions.Text = "Condicions de data " & mBanc.ImpagFchCondicions
        DateTimePicker1.Value = Today
    End Sub

    Private Sub LoadGrid()
        Dim oTb As New DataTable
        With oTb
            .Columns.Add("REF", System.Type.GetType("System.String"))
            .Columns.Add("EUR", System.Type.GetType("System.Decimal"))
            .Columns.Add("VTO", System.Type.GetType("System.DateTime"))
            .Columns.Add("TXT", System.Type.GetType("System.String"))
            .Columns.Add("NOM", System.Type.GetType("System.String"))
        End With

        Dim oImpagat As Impagat
        Dim oRow As DataRow
        mSum = New maxisrvr.Amt
        For Each oImpagat In mImpagats
            oRow = oTb.NewRow
            oRow(Cols.Ref) = IIf(oImpagat.RefBanc > "", oImpagat.RefBanc, "")
            oRow(Cols.Eur) = oImpagat.Csb.Amt.Formatted
            oRow(Cols.Vto) = oImpagat.Csb.Vto
            oRow(Cols.Txt) = oImpagat.Csb.txt
            oRow(Cols.Nom) = oImpagat.Csb.Client.Nom
            oTb.Rows.Add(oRow)
            mSum.Add(oImpagat.Csb.Amt)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            With .Columns(Cols.Ref)
                .HeaderText = "referencia"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "lliurat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        Dim oCca As Cca = Nothing
        If Update(oCca, exs) Then
            RaiseEvent AfterUpdate(oCca, EventArgs.Empty)
            WarnReps()
            DisplayMsg(oCca)
            Me.Close()
        Else
            MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub

    Private Shadows Function Update(ByRef oCca As Cca, ByRef exs as list(Of Exception)) As Boolean
        Dim oAmtNominal As MaxiSrvr.Amt = Xl_AmtNominal.Amt
        Dim oAmtDespeses As MaxiSrvr.Amt = Xl_AmtComisions.Amt
        oAmtDespeses.Add(Xl_AmtCorreu.Amt)
        Dim oAmtTot As MaxiSrvr.Amt = Xl_AmtTotal.Amt
        Dim oAmtIVA As MaxiSrvr.Amt = Xl_AmtIVA.Amt
        Dim DtFch As Date = DateTimePicker1.Value

        For i As Integer = 0 To mImpagats.Count - 1
            mImpagats(i).RefBanc = DataGridView1.Rows(i).Cells(Cols.Ref).Value
        Next

        Dim retval As Boolean = mImpagats.Update(mBanc, DtFch, oAmtNominal, oAmtDespeses, oAmtTot, oAmtIVA, oCca, exs)
        Return retval
    End Function


    Private Sub WarnReps()
        For Each oImpagat As Impagat In mImpagats
            oImpagat.WarnImpagat()
        Next
    End Sub


    Private Sub DisplayMsg(ByVal oCca As Cca)
        MsgBox("Arxivar a compte 6265 (" & oCca.Id & ")", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub Xl_Amt_AfterUpdate(sender As Object, e As System.EventArgs) Handles _
        Xl_AmtComisions.AfterUpdate, _
         Xl_AmtIVA.AfterUpdate, _
          Xl_AmtCorreu.AfterUpdate

        If mAllowEvents Then
            refrescaDespeses()
        End If

    End Sub
End Class
