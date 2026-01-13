

Public Class Frm_Banc_Impago
    Private _Banc As DTOBanc
    Private _Impagats As List(Of DTOImpagat)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Ref
        Eur
        Vto
        Txt
        Nom
    End Enum

    Public Sub New(oImpagats As List(Of DTOImpagat))
        MyBase.New
        Me.InitializeComponent()
        _Impagats = oImpagats
    End Sub

    Private Sub Frm_Banc_Impago_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Impagats.Count > 0 Then
            _Banc = _Impagats(0).Csb.Csa.Banc
            LoadBanc()
            LoadGrid()
            If Not LoadDespeses(exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
        _AllowEvents = True

    End Sub

    Private Function LoadDespeses(exs As List(Of Exception)) As Boolean
        Dim oSum As DTOAmt = Xl_AmtNominal.Amt.Clone
        Dim Descomptat As Boolean = _Impagats.First.Csb.Csa.Descomptat
        Dim DcComisio As Decimal = 0
        Dim DcIVA As Decimal = 0
        Dim DcMail As Decimal = 0
        Dim SngIVApercent As Decimal = DTOTax.Closest(DTOTax.Codis.Iva_Standard, DateTimePicker1.Value).Tipus
        If Descomptat And _Banc.ImpagComisio > 0 Then
            DcComisio = oSum.Percent(_Banc.ImpagComisio).Eur
        End If

        FEB2.Banc.Load(_Banc, exs)

        If Descomptat Then
            If _Banc.ImpagMinim Then
                If _Banc.ImpagMinim > DcComisio Then
                    DcComisio = _Banc.ImpagMinim
                End If
            End If
        Else
            DcComisio = _Banc.ComisioImpagoGestioCobro
        End If

        If _Banc.ImpagMail Then
            If _Banc.ImpagMailxRebut Then
                DcMail = _Impagats.Count * _Banc.ImpagMail
            Else
                DcMail = _Banc.ImpagMail
            End If
        End If

        Dim DcIvaBase As Decimal = 0
        If _Banc.Iban.Digits.Substring(0, 2) = GlobalVariables.Emp.DefaultCountry.ISO Then
            DcIvaBase = DcComisio
        End If

        If _Banc.ImpagMailIva Then
            If _Banc.ImpagMailIva Then
                DcIvaBase = DcIvaBase + DcMail
            End If
        End If

        DcIVA = Math.Round(DcIvaBase * SngIVApercent / 100, 2, MidpointRounding.AwayFromZero)
        Xl_AmtComisions.Amt = DTOAmt.Factory(DcComisio)
        Xl_AmtIVA.Amt = DTOAmt.Factory(DcIVA)
        Xl_AmtCorreu.Amt = DTOAmt.Factory(DcMail)
        refrescaDespeses()
        Return exs.Count = 0
    End Function

    Private Sub refrescaDespeses()
        Dim oAmt As DTOAmt = Xl_AmtNominal.Amt.Clone
        oAmt.Add(Xl_AmtComisions.Amt)
        oAmt.Add(Xl_AmtIVA.Amt)
        oAmt.Add(Xl_AmtCorreu.Amt)
        Xl_AmtTotal.Amt = oAmt
    End Sub

    Private Sub LoadBanc()
        If _Banc.ImpagComisio <> 0 Then TextBoxTipus.Text = _Banc.ImpagComisio & "%"
        If _Banc.ImpagMinim <> 0 Then TextBoxMinim.Text = Format(_Banc.ImpagMinim, "#,##0.00")
        If _Banc.ImpagMail <> 0 Then TextBoxCondsMail.Text = Format(_Banc.ImpagMail, "#,##0.00")
        CheckBoxIvaMail.Checked = _Banc.ImpagMailIVA
        CheckBoxMailxRebut.Checked = _Banc.impagMailxRebut
        PictureBoxBancLogo.Image = LegacyHelper.ImageHelper.Converter(_Banc.Logo)
        GroupBoxCondicions.Text = "Condicions de data " & _Banc.ImpagFchCondicions
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

        Dim oRow As DataRow
        Dim oSum = DTOAmt.Empty
        For Each oImpagat As DTOImpagat In _Impagats
            oRow = oTb.NewRow
            oRow(Cols.Ref) = IIf(oImpagat.RefBanc > "", oImpagat.RefBanc, "")
            oRow(Cols.Eur) = oImpagat.Csb.Amt.Formatted
            oRow(Cols.Vto) = oImpagat.Csb.Vto
            oRow(Cols.Txt) = oImpagat.Csb.Txt
            oRow(Cols.Nom) = oImpagat.Csb.Contact.Nom
            oTb.Rows.Add(oRow)
            oSum.Add(oImpagat.Csb.Amt)
        Next

        Xl_AmtNominal.Amt = oSum

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

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oCca As DTOCca = Nothing
        UIHelper.ToggleProggressBar(Panel1, True)
        oCca = Await Update(oCca, exs)
        UIHelper.ToggleProggressBar(Panel1, False)
        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oCca))
            If CheckBoxWarnReps.Checked Then
                Await WarnReps(exs)
                If exs.Count = 0 Then
                    Me.Close()
                Else
                    UIHelper.WarnError(exs, "error al avisar als representants")
                End If
            End If
        Else
            UIHelper.WarnError(exs, "error al desar l'impagat")
        End If
    End Sub

    Private Shadows Async Function Update(oCca As DTOCca, exs As List(Of Exception)) As Task(Of DTOCca)
        Dim oAmtNominal As DTOAmt = Xl_AmtNominal.Amt.Clone
        Dim oAmtDespeses As DTOAmt = Xl_AmtComisions.Amt.Clone
        oAmtDespeses.Add(Xl_AmtCorreu.Amt.Clone)
        Dim oAmtTot As DTOAmt = Xl_AmtTotal.Amt.Clone
        Dim oAmtIVA As DTOAmt = Xl_AmtIVA.Amt.Clone
        Dim DtFch As Date = DateTimePicker1.Value

        For i As Integer = 0 To _Impagats.Count - 1
            _Impagats(i).RefBanc = DataGridView1.Rows(i).Cells(Cols.Ref).Value
        Next

        oCca = Await FEB2.Impagats.Update(_Impagats, _Banc, DtFch, oAmtNominal, oAmtDespeses, oAmtTot, oAmtIVA, oCca, Current.Session.User, exs)

        Return oCca
    End Function


    Private Async Function WarnReps(exs As List(Of Exception)) As Task
        For Each oImpagat As DTOImpagat In _Impagats
            Await FEB2.Impagat.WarnReps(exs, Current.Session.User, oImpagat)
        Next
    End Function



    Private Sub Xl_Amt_AfterUpdate(sender As Object, e As System.EventArgs) Handles _
        Xl_AmtComisions.AfterUpdate,
         Xl_AmtIVA.AfterUpdate,
          Xl_AmtCorreu.AfterUpdate

        If _AllowEvents Then
            refrescaDespeses()
        End If

    End Sub


End Class
